package repository.interfaces;

import domain.*;
import repository.Repository;

import java.util.List;
import java.util.Map;

public interface ParticipantRepository extends Repository<Long, Participant> {

    List<Participant>   findParticipantByName(String nume);

    public Map<Participant, Long> getParticipantiCuPunctaj();

}
