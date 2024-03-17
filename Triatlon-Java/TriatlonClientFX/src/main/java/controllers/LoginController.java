package controllers;
import domain.*;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import service.TriatlonServiceInterface;

import java.io.IOException;

public class LoginController {

    private MainController ctrl;
    @FXML
    private PasswordField parola;
    @FXML
    private Button loginButton;
    @FXML
    private TextField username;
    private TriatlonServiceInterface service;
    private MainController mainController;
    private Parent mainRoot;
    public void setMainController(MainController mainController) {
        this.mainController = mainController;
    }

    public void setParent(Parent p) {
        this.mainRoot = p;
    }

    public void setService(TriatlonServiceInterface srv) {
        this.service = srv;
    }

    public void userLogIn(ActionEvent actionEvent) throws Exception {
        String username = this.username.getText();
        String password =  this.parola.getText();

        if(username.isEmpty() || password.isEmpty()) {
            Alert meesage = new Alert(Alert.AlertType.ERROR);
            meesage.setTitle("Eroare la LogIn");
            meesage.setHeaderText("Campurile nu pot fi goale!");
            meesage.setContentText("Va rugam sa completati toate campurile!");
            meesage.showAndWait();

        } else {
            ArbitruDTO arbitruDTO = new ArbitruDTO(username, password);
            try {
                Arbitru arbitru = service.login(arbitruDTO, mainController);
                if(arbitru == null) {
                    Alert meesage = new Alert(Alert.AlertType.ERROR);
                    meesage.setTitle("Eroare la LogIn");
                    meesage.setHeaderText("Cont inexistent!");
                    meesage.setContentText("Specificati alte date de logare!");
                    meesage.showAndWait();
                }
                else {
                    Stage s = (Stage) loginButton.getScene().getWindow();
                    s.close();
                    Stage stage = new Stage();
                    Scene scene = new Scene(mainRoot, 665, 400);
                    stage.setTitle(arbitru.getNume() + " " + arbitru.getPrenume());
                    stage.setScene(scene);
                    mainController.setArbitru(arbitru);
                    System.out.println(arbitru.getId());
                    mainController.initializeMain(arbitru);
                    stage.show();

                    this.username.setText("");
                    this.parola.setText("");
                }
            } catch (Exception e) {
                Alert meesage = new Alert(Alert.AlertType.ERROR);
                meesage.setTitle("Eroare la LogIn");
                meesage.setHeaderText("Cont inexistent!");
                meesage.setContentText(e.getMessage() + "\n");
                e.printStackTrace();
                meesage.showAndWait();
            }


        }
    }



}
