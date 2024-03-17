package rpcprotocol;

import domain.*;
import service.TriatlonObserverInterface;
import service.TriatlonServiceInterface;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

public class TriatlonClientRpcReflectionWorker implements Runnable, TriatlonObserverInterface {

    private TriatlonServiceInterface server;

    private Socket connection;

    private ObjectInputStream input;

    private ObjectOutputStream output;

    private volatile boolean connected;

    public TriatlonClientRpcReflectionWorker(TriatlonServiceInterface server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try{
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            connected = true;
        }catch(IOException e){
            e.printStackTrace();
        }
    }

    @Override
    public void run() {
        while(connected){
            try {
                Object request=input.readObject();
                Response response=handleRequest((Request)request);
                if (response!=null){
                    sendResponse(response);
                }
            } catch (IOException | ClassNotFoundException e) {
                e.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error "+e);
        }
    }

    private Response handleRequest(Request request){
        Response response=null;

        try {
            if(request.type() == RequestType.LOGIN){
                System.out.println("Login request ...");
                ArbitruDTO arbitruDTO = (ArbitruDTO) request.data();
                Arbitru arbitru = server.login(arbitruDTO, this);
                response = new Response.Builder().type(ResponseType.OK).data(arbitru).build();
            }
            /*if (request.type() == RequestType.GET_ARBITRU) {
                System.out.println("Get arbitru request ...");
                ArrayList<String> requestData = (ArrayList<String>) request.data();
                String username = requestData.get(0);
                String parola = requestData.get(1);
                response = new Response.Builder().type(ResponseType.TAKE_ARBITRU).data(server.getAccount(username,parola, this)).build();
            }*/
            if (request.type() == RequestType.GET_PROBE) {
                System.out.println("Get rezervari request ...");
                response = new Response.Builder().type(ResponseType.TAKE_PROBE).data(server.findAllProbe()).build();
            }
            if (request.type() == RequestType.GET_PARTICIPANTI_CU_PUNCTAJ) {
                System.out.println("Get Participsnti cu punctaj request ...");
                response = new Response.Builder().type(ResponseType.TAKE_PARTICIPANTI_CU_PUNCTAJ).data(server.getParticipantiCuPunctaj(this)).build();
            }
            if (request.type() == RequestType.GET_PARTICIPANTI_DE_LA_PROBA) {
                List<ParticipantDTO> participanti =  server.getParticipantiDeLaProba((Proba) request.data(), this);
                System.out.println("Get Participanti de la Proba request ...");
                response = new Response.Builder().type(ResponseType.TAKE_PARTICIPANTI_DE_LA_PROBA).data(participanti).build();
            }
            if (request.type() == RequestType.ADD_INSCRIERE) {
                System.out.println("Add Inscriere request ...");
                server.addInscriere((Inscriere) request.data());
                response = new Response.Builder().type(ResponseType.OK).data((Inscriere) request.data()).build();
                // DE MODIFICAT IN LOC DE RESPONSETYPE UPDATEPUNCTAJ SA FIE INAPOI IN OK
            }
            if (request.type() == RequestType.LOGOUT) {
                System.out.println("Logout request...");
                server.logout((Arbitru) request.data(), this);
                response = new Response.Builder().type(ResponseType.OK).build();
                connected = false;
            }

        }catch (Exception e) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }

        return response;
    }

    private void sendResponse(Response response) throws IOException{
        System.out.println("sending response "+response);
        output.writeObject(response);
        output.flush();
    }

    @Override
    public void updatePunctaj() throws Exception {
        Response resp = new Response.Builder().type(ResponseType.UPDATE_PUNCTAJ).build();
        System.out.println("Sending update punctaj response "+resp);
        try {
            sendResponse(resp);
        } catch (IOException e) {
            throw new Exception("Sending error: "+e);
        }
    }
}
