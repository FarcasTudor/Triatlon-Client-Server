package repository.interfaces;

import domain.*;
import repository.Repository;

import java.util.Map;

public interface ProbaRepository extends Repository<Long, Proba> {

    Map<Participant,Long> getParticipantiProba(Proba proba);
}
