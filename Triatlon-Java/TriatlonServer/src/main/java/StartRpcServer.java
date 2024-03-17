

import repository.databases.*;
import repository.interfaces.ArbitruRepository;
import repository.interfaces.InscriereRepository;
import repository.interfaces.ParticipantRepository;
import repository.interfaces.ProbaRepository;
import service.TriatlonServiceInterface;
import utils.AbstractServer;
import utils.TriatlonRpcConcurrentServer;

import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.boot.MetadataSources;
import org.hibernate.boot.registry.StandardServiceRegistry;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;

import java.io.IOException;
import java.rmi.ServerException;
import java.util.Properties;

public class StartRpcServer {

    private static final int defaultPort = 55555;
    public static void main(String[] args){
        Properties serverProps = new Properties();
        try {
            serverProps.load(StartRpcServer.class.getResourceAsStream("/triatlonserver.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch(IOException e){
            System.err.println("Cannot find transportserver.properties " + e);
            return;
        }
        // ArbitruRepository arbitruDB = new ArbitruDB(serverProps);
        ArbitruRepository arbitruDB= new ArbitruHibernateDB(serverProps);
        InscriereRepository inscriereDB = new InscriereDB(serverProps);
        ParticipantRepository participantDB = new ParticipantDB(serverProps);
        ProbaRepository probaDB = new ProbaDB(serverProps);

        TriatlonServiceInterface service = new Service(arbitruDB, inscriereDB, participantDB, probaDB);
        int triatlonServerPort = defaultPort;
        try {
            triatlonServerPort = Integer.parseInt(serverProps.getProperty("port"));
        } catch (NumberFormatException nef) {
            System.err.println("Wrong Port Number " + nef.getMessage());
            System.err.println("Using default port " + defaultPort);
        }
        System.out.println("Starting server on port: " + triatlonServerPort);
        AbstractServer server = new TriatlonRpcConcurrentServer(triatlonServerPort, service);
        try {
            server.start();
        } catch (ServerException e) {
            System.err.println("Error starting the server" + e.getMessage());
        }
    }
}
