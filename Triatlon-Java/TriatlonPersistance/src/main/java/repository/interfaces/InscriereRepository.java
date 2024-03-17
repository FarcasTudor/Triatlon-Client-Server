package repository.interfaces;

import domain.*;
import repository.Repository;

import java.util.List;

public interface InscriereRepository extends Repository<Long, Inscriere> {
    List<Long> findParticipantsByProba(Long idProba);
    List<Inscriere> findInscriereByProbaAndParticipant(Long idProba, Long idParticipant);
}
