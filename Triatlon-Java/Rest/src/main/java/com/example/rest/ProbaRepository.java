package com.example.rest;

import org.springframework.stereotype.Component;

import java.io.FileNotFoundException;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

@Component
public class ProbaRepository implements Repository<Long, Proba> {

    private Map<Long, Proba> entities;

    public ProbaRepository() throws FileNotFoundException {
        this.entities = new HashMap<Long, Proba>();
        populateProbe();
    }

    private void populateProbe() throws FileNotFoundException {
        Arbitru a1 = new Arbitru("nume1", "prenume1", "username1", "parola1");
        a1.setId(1L);
        Proba p1 = new Proba("100m", 1L);
        p1.setId(1L);
        Arbitru a2 = new Arbitru("nume2", "prenume2", "username2", "parola2");
        a2.setId(2L);
        Proba p2 = new Proba("200m", 2L);
        p2.setId(2L);
        Arbitru a3 = new Arbitru("nume3", "prenume3", "username3", "parola3");
        a3.setId(3L);
        Proba p3 = new Proba("300m", 3L);
        p3.setId(3L);
        Arbitru a4 = new Arbitru("nume4", "prenume4", "username4", "parola4");
        a4.setId(4L);
        Proba p4 = new Proba("400m", 4L);
        p4.setId(4L);
        this.save(p1);
        this.save(p2);
        this.save(p3);
        this.save(p4);

    }

    @Override
    public Proba findOne(Long aLong) {
        if(aLong == null){
            throw new IllegalArgumentException("ID-ul nu poate fi null!\n");
        }
        else if(entities.get(aLong) == null){
            //throw new LackException("Entitatea cu acest ID nu exista!\n");
        }
        return entities.get(aLong);
    }

    @Override
    public Iterable<Proba> findAll() throws SQLException {
        if(entities.values().size() == 0){
            //throw new LackException("Nu avem nicio entitate!\n");
        }
        return entities.values();
    }

    @Override
    public Proba save(Proba entity) throws FileNotFoundException {
        if(entity == null){
            //throw new IllegalArgumentException("ID-ul nu poate fi null!\n");
        }
        for(Proba ent : entities.values()){
            if(ent.getId() == entity.getId()){
                //throw new DuplicateException("ID-ul exista deja!\n");
            }
        }
        entities.put(entity.getId(), entity);
        return null;
    }

    @Override
    public Proba delete(Long aLong) {
        if(entities.get(aLong) == null){
            //throw new LackException("Entitatea cu acest ID nu exista!\n");
        }
        entities.remove(aLong);
        return null;
    }

    @Override
    public Proba update(Long id, Proba entity2) {
        entities.put(id, entity2);
        return null;
    }
}
