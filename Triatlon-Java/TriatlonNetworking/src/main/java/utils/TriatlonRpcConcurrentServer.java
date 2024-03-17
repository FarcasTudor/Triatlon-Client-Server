package utils;

import rpcprotocol.TriatlonClientRpcReflectionWorker;
import service.TriatlonServiceInterface;

import java.net.Socket;

public class TriatlonRpcConcurrentServer extends AbsConcurrentServer{

    private TriatlonServiceInterface triatlonServer;

    public TriatlonRpcConcurrentServer(int port, TriatlonServiceInterface triatlonServer) {
        super(port);
        this.triatlonServer = triatlonServer;
        System.out.println("Triatlon- TriatlonRpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        TriatlonClientRpcReflectionWorker worker=new TriatlonClientRpcReflectionWorker(triatlonServer, client);
        Thread tw=new Thread(worker);
        return tw;
    }

    @Override
    public void stop() {
        System.out.println("Stopping services ...");
    }
}
