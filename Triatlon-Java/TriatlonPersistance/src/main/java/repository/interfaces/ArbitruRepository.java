package repository.interfaces;

import domain.*;
import repository.Repository;

public interface ArbitruRepository extends Repository<Long, Arbitru> {

    public Arbitru getAccount(String username, String parola);

}
