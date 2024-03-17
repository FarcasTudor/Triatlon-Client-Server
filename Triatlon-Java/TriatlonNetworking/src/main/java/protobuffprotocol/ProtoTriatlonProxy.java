package protobuffprotocol;

import domain.*;
import service.TriatlonObserverInterface;
import service.TriatlonServiceInterface;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class ProtoTriatlonProxy implements TriatlonServiceInterface {

    private String host;
    private int port;
    private TriatlonObserverInterface client;
    private InputStream input;
    private OutputStream output;
    private Socket connection;

    private BlockingQueue<TriatlonProtobufs.TriatlonResponse> qresponses;
    private volatile boolean finished;
    public ProtoTriatlonProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses=new LinkedBlockingQueue<TriatlonProtobufs.TriatlonResponse>();
    }

    @Override
    public Arbitru login(ArbitruDTO arbitruDTO, TriatlonObserverInterface client) throws Exception {
        initializeConnection();
        System.out.println("Login request ... ");
        sendRequest(ProtoUtils.createLoginRequest(arbitruDTO));
        TriatlonProtobufs.TriatlonResponse response=readResponse();
        if (response.getType() == TriatlonProtobufs.TriatlonResponse.Type.OK){
            this.client=client;
            Arbitru arbitru = ProtoUtils.getArbitru(response);
            return arbitru;
        }
        if (response.getType()==TriatlonProtobufs.TriatlonResponse.Type.ERROR){
            String err=response.getError();
            closeConnection();
            throw new Exception(err);
        }
        return null;
    }

    private void closeConnection() {
        finished=true;
        try {

            input.close();
            output.close();
            connection.close();
            client=null;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void sendRequest(TriatlonProtobufs.TriatlonRequest request) {
        try {
            System.out.println("Sending request ..." + request);
            request.writeDelimitedTo(output);
            output.flush();
            System.out.println("Request sent.");
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }

    private TriatlonProtobufs.TriatlonResponse readResponse() throws Exception {
        TriatlonProtobufs.TriatlonResponse response=null;
        try {
            response=qresponses.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    @Override
    public void logout(Arbitru arbitru, TriatlonObserverInterface client) throws Exception {
        System.out.println("Logout request ...");
        System.out.println("Arbitru: "+arbitru);
        sendRequest(ProtoUtils.createLogoutRequest(arbitru));
        TriatlonProtobufs.TriatlonResponse response=readResponse();
        closeConnection();
        if (response.getType()==TriatlonProtobufs.TriatlonResponse.Type.ERROR){
            String err=response.getError();
            throw new Exception(err);
        }
    }

    @Override
    public List<ParticipantDTO> getParticipantiCuPunctaj(TriatlonObserverInterface client) throws Exception {
        sendRequest(ProtoUtils.createGetParticipantiCuPunctajRequest());
        TriatlonProtobufs.TriatlonResponse response = readResponse();
        if (response.getType() == TriatlonProtobufs.TriatlonResponse.Type.ERROR) {
            String err = response.getError();
            throw new Exception(err);
        }
        System.out.println("response received "+response);
        List<ParticipantDTO> participanti = ProtoUtils.createGetParticipantiCuPunctaj(response);
        return participanti;
    }

    @Override
    public Iterable<Proba> findAllProbe() throws Exception {
        sendRequest(ProtoUtils.createGetProbeRequest());
        TriatlonProtobufs.TriatlonResponse response = readResponse();
        if (response.getType() == TriatlonProtobufs.TriatlonResponse.Type.ERROR) {
            String err = response.getError();
            throw new Exception(err);
        }
        System.out.println("response received PROBE : "+response);
        System.out.println("response received PROBE lista 1 : "+response.getProbeList());
        // MERGE PRIMA DATA FIND_ALL_PROBE DAR DUPA PUSCA
        List<Proba> probe = ProtoUtils.createGetProbe(response);
        System.out.println("response received PROBE lista 2 : "+probe);

        return probe;
    }

    @Override
    public void addInscriere(Inscriere inscriere) throws Exception {
        sendRequest(ProtoUtils.createAddInscriereRequest(inscriere));
        TriatlonProtobufs.TriatlonResponse response = readResponse();
        if (response.getType() == TriatlonProtobufs.TriatlonResponse.Type.ERROR) {
            String err = response.getError();
            throw new Exception(err);
        }
    }

    @Override
    public List<ParticipantDTO> getParticipantiDeLaProba(Proba proba,TriatlonObserverInterface client) throws Exception {
        sendRequest(ProtoUtils.createGetParticipantiDeLaProbaRequest(proba));
        TriatlonProtobufs.TriatlonResponse response = readResponse();
        if (response.getType() == TriatlonProtobufs.TriatlonResponse.Type.ERROR) {
            String err = response.getError();
            throw new Exception(err);
        }
        List<ParticipantDTO> participanti = ProtoUtils.createGetParticipantiDeLaProba(response);
        return participanti;
    }

    private void initializeConnection() throws Exception{
        try {
            connection=new Socket(host,port);
            output=connection.getOutputStream();
            //output.flush();
            input=connection.getInputStream();     //new ObjectInputStream(connection.getInputStream());
            finished=false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    private void startReader(){
        Thread tw=new Thread(new ReaderThread());
        tw.start();
    }

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    TriatlonProtobufs.TriatlonResponse response=TriatlonProtobufs.TriatlonResponse.parseDelimitedFrom(input);
                    System.out.println("response received "+response);

                    if (response.getType() == TriatlonProtobufs.TriatlonResponse.Type.UPDATE_PUNCTAJ){
                        handleUpdate(response);
                    }else{
                        try {
                            qresponses.put(response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Reading error "+e);
                }
            }
        }
    }


    private void handleUpdate(TriatlonProtobufs.TriatlonResponse updateResponse){
        if (updateResponse.getType() == TriatlonProtobufs.TriatlonResponse.Type.UPDATE_PUNCTAJ) {
            try {
                System.out.println("INAINTE SA SE DEA UPDATE");
                client.updatePunctaj();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

    }
}
