import domain.*;
import repository.interfaces.*;
import service.TriatlonObserverInterface;
import service.TriatlonServiceInterface;

import java.io.FileNotFoundException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class Service implements TriatlonServiceInterface {
    private final InscriereRepository inscriereRepository;

    private final ParticipantRepository participantRepository;

    private final ProbaRepository probaRepository;

    private final ArbitruRepository arbitruRepository;

    private Map<Long, TriatlonObserverInterface> loggedClients;


    public Service(ArbitruRepository arbitruRepository, InscriereRepository inscriereRepository, ParticipantRepository participantRepository, ProbaRepository probaRepository) {
        this.arbitruRepository = arbitruRepository;
        this.inscriereRepository = inscriereRepository;
        this.participantRepository = participantRepository;
        this.probaRepository = probaRepository;
        loggedClients = new ConcurrentHashMap<>();
    }

    /*public Arbitru getAccount(String username, String parola, TriatlonObserverInterface client) {
        return arbitruRepository.getAccount(username, parola);
    }*/

    public synchronized Arbitru login(ArbitruDTO arbitru, TriatlonObserverInterface client) throws Exception {
        Arbitru arbitru1 = arbitruRepository.getAccount(arbitru.getUsername(), arbitru.getParola());
        if (arbitru1 != null) {
            if (loggedClients.get(arbitru1.getId()) != null)
                throw new Exception("Arbitru already logged in.");
            loggedClients.put(arbitru1.getId(), client);
            System.out.println("Logged users " + loggedClients);
        } else
            throw new Exception("Authentication failed.");
        return arbitru1;
    }

    public synchronized void logout(Arbitru arbitru, TriatlonObserverInterface client) throws Exception {
        Arbitru arbitru1 = arbitruRepository.getAccount(arbitru.getUsername(), arbitru.getParola());
        if(loggedClients.get(arbitru1.getId()) != null)
            loggedClients.remove(arbitru1.getId());
        else
            throw new Exception("User not logged in");
    }

    public synchronized List<ParticipantDTO> getParticipantiCuPunctaj(TriatlonObserverInterface client) {
        Map<Participant, Long> participanti =  participantRepository.getParticipantiCuPunctaj();
        List<ParticipantDTO> listaParticipantiDTO = new ArrayList<>();
        var set = participanti.entrySet();
        for (var p : set) {
            Participant part = p.getKey();
            ParticipantDTO partDTO = new ParticipantDTO(part.getId(), part.getNume(), part.getPrenume(), p.getValue());
            listaParticipantiDTO.add(partDTO);
        }
        return listaParticipantiDTO;
    }


    public Iterable<Proba> findAllProbe() throws SQLException {
        return probaRepository.findAll();
    }

    public synchronized void addInscriere(Inscriere inscriere) throws FileNotFoundException {
        inscriereRepository.save(inscriere);
        notifyUpdatePunctaj();
    }

    private synchronized void notifyUpdatePunctaj() {
        ExecutorService executor = Executors.newFixedThreadPool(5);
        for(Long key : loggedClients.keySet()) {
            TriatlonObserverInterface client = loggedClients.get(key);
                executor.execute(() -> {
                    try {
                        client.updatePunctaj();
                    } catch (Exception e) {
                        System.err.println("Error notifying client " + e);
                    }
                });
        }
    }


    public synchronized List<ParticipantDTO> getParticipantiDeLaProba(Proba proba, TriatlonObserverInterface client) {
        Map<Participant, Long> participanti =  probaRepository.getParticipantiProba(proba);
        List<ParticipantDTO> listaParticDTOPerProba = new ArrayList<>();

        var setPerProba = participanti.entrySet();
        for (var p : setPerProba) {
            Participant part = p.getKey();
            ParticipantDTO partDTO = new ParticipantDTO(part.getId(), part.getNume(),part.getPrenume(), p.getValue());
            listaParticDTOPerProba.add(partDTO);
        }
        return listaParticDTOPerProba;
    }
}
