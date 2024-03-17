package controllers;

import domain.*;
import javafx.application.Platform;
import javafx.beans.property.ReadOnlyObjectWrapper;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.stage.Stage;
import service.TriatlonObserverInterface;
import service.TriatlonServiceInterface;

import java.io.FileNotFoundException;
import java.sql.SQLException;
import java.util.*;

public class MainController implements TriatlonObserverInterface {

    @FXML
    private Label labelNotificat;
    @FXML
    private Button logOutButton;
    @FXML
    private TableColumn<ParticipantDTO, Long> columnPunctajTotalDreapta;
    @FXML
    private TableColumn<ParticipantDTO, String> columnPrenumeDreapta;
    @FXML
    private TableColumn<ParticipantDTO, String> columnNumeDreapta;
    @FXML
    private TableView<ParticipantDTO> tableViewParticipantiProbaActuala;
    @FXML
    private Button addPunctajButton;
    @FXML
    private TextField punctajDeAdaugatTextField;
    @FXML
    private TextField addButton;
    @FXML
    private TableColumn<ParticipantDTO, Long> columnPunctajTotal;
    @FXML
    private TableColumn<ParticipantDTO, String> columnPrenume;
    @FXML
    private TableColumn<ParticipantDTO, String> columnNume;
    @FXML
    private TableView<ParticipantDTO> tableViewParticipanti;
    @FXML
    private TextField arbitruTextField;
    private Arbitru arbitru;

    private final ObservableList<ParticipantDTO> participantDTOModel = FXCollections.observableArrayList();

    private final ObservableList<ParticipantDTO> participantDTOProbaActualaModel = FXCollections.observableArrayList();
    private TriatlonServiceInterface service;

    private Proba probaActuala;
    private Proba setProba(Proba proba) {
        this.probaActuala = proba;
        return proba;
    }
    public void setService(TriatlonServiceInterface srv) {
        this.service = srv;
    }

    public void setArbitru(Arbitru arbitru) {
        this.arbitru = arbitru;
    }

    public void initializeMain(Arbitru arbitru) {
        try {
            System.out.println("Starting initializeMain AGAIN");
            this.arbitru = arbitru;
            arbitruTextField.setText(arbitru.getNume() + " " + arbitru.getPrenume());
            initializeLeftTable();
            initializeRightTable();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initializeRightTable() throws Exception {
        System.out.println("Starting initializeRightTable");
        // List<ParticipantDTO> participantiDTOdreapta = new ArrayList<>();
        Proba probaActuala = getProba();

        System.out.println("Proba ID : " + probaActuala.getId());

        List<ParticipantDTO> participantiDTO = service.getParticipantiDeLaProba(probaActuala, this);

        /*Map<Participant,Long> mapParticipantPunctajDreapta = service.getParticipantiDeLaProba(probaActuala);

        var set1 = mapParticipantPunctajDreapta.entrySet();
        for (var entry : set1) {
            Participant part = entry.getKey();
            ParticipantDTO pDTO = new ParticipantDTO(part.getId(),part.getNume(),part.getPrenume(),entry.getValue());
            System.out.println(pDTO);
            participantiDTOdreapta.add(pDTO);
        }*/
        participantiDTO.sort(Comparator.comparing(ParticipantDTO::getPunctajTotal).reversed());
        //print to console all participants from participantiDTOdreapta

        participantDTOProbaActualaModel.setAll(participantiDTO);

        columnNumeDreapta.setCellValueFactory(data -> new SimpleStringProperty(data.getValue().getNume()));
        columnPrenumeDreapta.setCellValueFactory(data -> new SimpleStringProperty(data.getValue().getPrenume()));
        columnPunctajTotalDreapta.setCellValueFactory(data -> new ReadOnlyObjectWrapper<>(data.getValue().getPunctajTotal()));
        tableViewParticipantiProbaActuala.setItems(participantDTOProbaActualaModel);
    }

    private void initializeLeftTable() throws Exception {
        System.out.println("Starting initializeLeftTable ... ");
        List<ParticipantDTO> participantiDTO = service.getParticipantiCuPunctaj(this);
        System.out.println("Participanti left table:" + participantiDTO);
       /* Map<Participant,Long> mapParticipantPunctaj = service.getParticipantiCuPunctaj();
        var set = mapParticipantPunctaj.entrySet();
        for (var entry : set) {
            Participant part = entry.getKey();
            ParticipantDTO pDTO = new ParticipantDTO(part.getId(),part.getNume(),part.getPrenume(),entry.getValue());
            participantiDTO.add(pDTO);
        }*/

        //sort by name
        participantiDTO.sort(Comparator.comparing(ParticipantDTO::getNume));
        System.out.println("-------------------");
        System.out.println("Before populate left table");
        participantDTOModel.setAll(participantiDTO);
        System.out.println("After populate left table");

        columnNume.setCellValueFactory(data -> new SimpleStringProperty(data.getValue().getNume()));
        columnPrenume.setCellValueFactory(data -> new SimpleStringProperty(data.getValue().getPrenume()));
        columnPunctajTotal.setCellValueFactory(data -> new ReadOnlyObjectWrapper<>(data.getValue().getPunctajTotal()));
        tableViewParticipanti.setItems(participantDTOModel);

    }

    public Proba getProba() throws Exception {
        Proba probaActuala = null;
        for (Proba proba : service.findAllProbe()) {
            if (proba.getIdArbitru().equals(arbitru.getId())) {
                probaActuala = proba;
                probaActuala.setId(proba.getId());
            }
        }
        System.out.println("Proba actuala : " + probaActuala);
        //DE VERIFICAT DC CAND APAS PE BUTON DE ADD< PROBAACTUALA E NULL, IN RESPONSE NU PRIMESC NIMIC
        return probaActuala;
    }

    public void onAddButtonClick(ActionEvent actionEvent) throws SQLException, FileNotFoundException {
        try {
            ParticipantDTO participantDTO = tableViewParticipanti.getSelectionModel().getSelectedItem();
            Long punctajDeAdaugat = Long.valueOf(punctajDeAdaugatTextField.getText());

            Proba probaActuala = getProba();


            Participant participant = new Participant(participantDTO.getNume(), participantDTO.getPrenume());
            participant.setId(participantDTO.getId());

            Inscriere inscriere = new Inscriere(participant.getId(), probaActuala.getId(), punctajDeAdaugat);
            service.addInscriere(inscriere);
            punctajDeAdaugatTextField.clear();
            //initializeMain(arbitru);
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    public void onLogOutButtonClick(ActionEvent actionEvent) {
        try {
            Stage stage = (Stage) logOutButton.getScene().getWindow();
            System.out.println("LOGOUT PRESSED!!! Arbitru: " + arbitru.getNume() + " " + arbitru.getPrenume());
            service.logout(arbitru, this);
            stage.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public void updatePunctaj() {
        Platform.runLater(() -> {
            try {
                System.out.println("Update punctaj called");
                System.out.println("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                tableViewParticipanti.getItems().clear();
                tableViewParticipantiProbaActuala.getItems().clear();
                initializeMain(arbitru);
            } catch (Exception e) {
                e.printStackTrace();
            }
        });
    }
}
