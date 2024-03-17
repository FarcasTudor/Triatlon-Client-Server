package rpcprotocol;

import domain.*;
import service.TriatlonObserverInterface;
import service.TriatlonServiceInterface;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Map;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class TriatlonServiceRpcProxy implements TriatlonServiceInterface {

    private String host;

    private int port;
    private TriatlonObserverInterface client;
    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;
    private BlockingQueue<Response> qresponses;
    private volatile boolean finished;

    public TriatlonServiceRpcProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<Response>();
    }

    /*@Override
    public synchronized Arbitru getAccount(String username, String parola, TriatlonObserverInterface client) throws Exception {
        initializeConnection();
        Request req = new Request.Builder().type(RequestType.GET_ARBITRU).data(new ArrayList<>(Arrays.asList(username, parola))).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            closeConnection();
            throw new Exception(err);
        }
        else {
            this.client = client;
            return (Arbitru) response.data();
        }
    }*/

    @Override
    public synchronized Arbitru login(ArbitruDTO arbitru, TriatlonObserverInterface client) throws Exception {
        initializeConnection();
        Request req = new Request.Builder().type(RequestType.LOGIN).data(arbitru).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            closeConnection();
            throw new Exception(err);
        }
        else {
            if (response.type() == ResponseType.OK) {
                this.client = client;
            }
        }
        return (Arbitru) response.data();
    }

    @Override
    public void logout(Arbitru arbitru, TriatlonObserverInterface client) throws Exception {
        Request req = new Request.Builder().type(RequestType.LOGOUT).data(arbitru).build();
        sendRequest(req);
        Response response = readResponse();
        closeConnection();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            System.out.println(err);
        }
    }

    @Override
    public List<ParticipantDTO> getParticipantiCuPunctaj(TriatlonObserverInterface client) throws Exception {
        Request req = new Request.Builder().type(RequestType.GET_PARTICIPANTI_CU_PUNCTAJ).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            closeConnection();
            System.out.println(err);
        }
        return (List<ParticipantDTO>) response.data();
    }

    @Override
    public Iterable<Proba> findAllProbe() throws Exception {
        Request req = new Request.Builder().type(RequestType.GET_PROBE).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            closeConnection();
            System.out.println(err);
        }
        return (Iterable<Proba>) response.data();
    }

    @Override
    public void addInscriere(Inscriere inscriere) throws Exception {
        Request req = new Request.Builder().type(RequestType.ADD_INSCRIERE).data(inscriere).build();
        sendRequest(req);

        Response response = readResponse();

        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            closeConnection();
            System.out.println(err);
        }
    }

    @Override
    public List<ParticipantDTO> getParticipantiDeLaProba(Proba proba, TriatlonObserverInterface client) throws Exception {
        Request req = new Request.Builder().type(RequestType.GET_PARTICIPANTI_DE_LA_PROBA).data(proba).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            closeConnection();
            System.out.println(err);
        }
        return (List<ParticipantDTO>) response.data();
    }

    private void sendRequest(Request request) throws Exception {
        try {
            //System.out.println(request);
            output.writeObject(request);
            output.flush();
        } catch (IOException e) {
            throw new Exception("Error sending object "+e);
        }

    }

    private Response readResponse(){
        Response response=null;
        try{
            response = qresponses.take();
            //System.out.println(response);
        } catch (InterruptedException e) {
            e.printStackTrace();

        }
        return response;
    }

    private void startReader(){
        Thread tw=new Thread(new ReaderThread());
        tw.start();
    }

    private void initializeConnection() {
        try {
            connection=new Socket(host,port);
            output=new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input=new ObjectInputStream(connection.getInputStream());
            finished=false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
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

    private void handleUpdate(Response response){
        if(response.type() == ResponseType.UPDATE_PUNCTAJ){
            try{
                client.updatePunctaj();
            }catch (Exception e){
                e.printStackTrace();
            }
        }
    }

    private boolean isUpdate(Response response){
        return response.type() == ResponseType.UPDATE_PUNCTAJ;
    }

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    Object response=input.readObject();
                    System.out.println("response received " + response);
                    if(isUpdate((Response) response)){
                        handleUpdate((Response) response);
                    }
                    else {
                        try {
                            qresponses.put((Response) response);
                            System.out.println("Recieved: " + qresponses);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException | ClassNotFoundException e) {
                    System.out.println("Reading error "+e);
                }
            }
        }
    }
}
