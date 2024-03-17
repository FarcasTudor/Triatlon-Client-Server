package controllers;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import protobuffprotocol.ProtoTriatlonProxy;
import rpcprotocol.TriatlonServiceRpcProxy;
import service.TriatlonServiceInterface;

import java.io.FileReader;
import java.io.IOException;
import java.sql.SQLException;
import java.util.Properties;

public class HelloApplication extends Application {

    private static int defaultPort = 55555;
    private static String defaultServer = "localhost";
    private static Stage stg;

    @Override
    public void start(Stage stage) throws IOException, SQLException {
        Properties props = new Properties();
        /*try {
            props.load(new FileReader("D:\\ANUL 2\\SEM2\\MPP\\Java_Project\\LAB3\\bd.config"));
            System.out.println("Properties set. ");
            System.out.println(props);
        } catch (IOException e) {
            System.out.println("Cannot find bd.config "+e);
        }*/

        try {
            props.load(HelloApplication.class.getResourceAsStream("/triatlonclient.properties"));
            System.out.println("Client properties set. ");
            props.list(System.out);
        } catch (IOException e) {
            System.out.println("Cannot find triatlonclient.properties "+e);
            return;
        }
        String serverIP = props.getProperty("host", defaultServer);
        int serverPort = defaultPort;

        try {
            serverPort = Integer.parseInt(props.getProperty("port"));
        } catch (NumberFormatException ex) {
            System.err.println("Wrong port number "+ex.getMessage());
            System.out.println("Using default port: "+defaultPort);
        }
        System.out.println("Using server IP " + serverIP);
        System.out.println("Using server port " + serverPort);

        TriatlonServiceInterface service = new TriatlonServiceRpcProxy(serverIP, serverPort);
        // TriatlonServiceInterface service = new ProtoTriatlonProxy(serverIP, serverPort);

        stg = stage;
        stage.setResizable(false);
        FXMLLoader fxmlLoader = new FXMLLoader(HelloApplication.class.getResource("/login.fxml"));
        Parent root = fxmlLoader.load();
        LoginController loginController = fxmlLoader.getController();
        loginController.setService(service);

        FXMLLoader mainLoader = new FXMLLoader(HelloApplication.class.getResource("/main.fxml"));
        Parent mainRoot = mainLoader.load();
        MainController mainController = mainLoader.getController();
        mainController.setService(service);

        loginController.setMainController(mainController);
        loginController.setParent(mainRoot);

        stage.setTitle("Login");
        stage.setScene(new Scene(root, 600, 400));
        stage.show();
        /*Scene scene = new Scene(fxmlLoader.load(), 600 , 400);
        stage.setResizable(false);
        stage.setTitle("Hello!");
        stage.setScene(scene);
        LoginController logIn = fxmlLoader.getController();
        logIn.setService(service);
        stage.show();*/
    }


    public static void main(String[] args) {
        Application.launch();
    }
}