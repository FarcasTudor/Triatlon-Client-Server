package com.example.rest;



import java.io.FileNotFoundException;
import java.sql.SQLException;


public interface Repository<ID, E extends Entity<ID>> {


    /**
     * @param id -the id of the entity to be returned
     *           id must not be null
     * @return the entity with the specified id
     * or null - if there is no entity with the given id
     * @throws IllegalArgumentException if id is null.
     */
    E findOne(ID id);

    /**
     * @return all entities
     */
    Iterable<E> findAll() throws SQLException;


    /**
     * @param entity entity must be not null
     * @return null- if the given entity is saved
     * otherwise returns the entity (id already exists)
     * @throws IllegalArgumentException if the given entity is null.     *
     */
    E save(E entity) throws FileNotFoundException;


    /**
     * removes the entity with the specified id
     *
     * @param id id must be not null
     * @return the removed entity or null if there is no entity with the given id
     */
    E delete(ID id);

    E update(ID id, E entity2);


}