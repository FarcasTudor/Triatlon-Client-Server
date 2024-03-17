package service;

import domain.*;

import java.util.List;
import java.util.Map;

public interface TriatlonServiceInterface {

/*
    Arbitru getAccount(String username, String parola, TriatlonObserverInterface client) throws Exception;
*/
    Arbitru login(ArbitruDTO arbitru, TriatlonObserverInterface client) throws Exception;

    void logout(Arbitru arbitru, TriatlonObserverInterface client) throws Exception;
    List<ParticipantDTO> getParticipantiCuPunctaj(TriatlonObserverInterface client) throws Exception;
    Iterable<Proba> findAllProbe() throws Exception;
    void addInscriere(Inscriere inscriere) throws Exception;
    List<ParticipantDTO> getParticipantiDeLaProba(Proba proba, TriatlonObserverInterface client ) throws Exception;

}
