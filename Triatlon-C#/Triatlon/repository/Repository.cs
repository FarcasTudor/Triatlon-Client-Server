using System.Collections.Generic;
using ConcursTriatlon.domain;

namespace ConcursTriatlon.repository
{
    public interface Repository<ID, E> where E : Entity<ID>
    {
        /**
         * @param id - the id of the entity to be returned
         *           id must not be null
         * @return the entity with the specified id
         * or null - if there is no entity with the given id
         * @throws ArgumentException if id is null.
         */
        E findOne(ID id);

        /**
         * @return all entities
         */
        IEnumerable<E> findAll();

        /**
         * @param entity entity must be not null
         * @return null- if the given entity is saved
         * otherwise returns the entity (id already exists)
         * @throws ArgumentException if the given entity is null.
         */
        E save(E entity);

        /**
         * removes the entity with the specified id
         *
         * @param id id must be not null
         * @return the removed entity or null if there is no entity with the given id
         * @throws ArgumentException if the given id is null.
         */
        E delete(ID id);

        E update(E entity1, E entity2);
    }
}
