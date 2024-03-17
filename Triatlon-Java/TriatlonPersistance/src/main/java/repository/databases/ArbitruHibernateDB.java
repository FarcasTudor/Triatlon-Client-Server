package repository.databases;

import domain.Arbitru;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.hibernate.boot.MetadataSources;
import org.hibernate.boot.registry.StandardServiceRegistry;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;
import repository.interfaces.ArbitruRepository;

import java.io.FileNotFoundException;
import java.sql.SQLException;
import java.util.List;
import java.util.Objects;
import java.util.Properties;
import org.hibernate.query.Query;


public class ArbitruHibernateDB implements ArbitruRepository {

    static SessionFactory sessionFactory;

    static void initialize() {
        // A SessionFactory is set up once for an application!
        final StandardServiceRegistry registry = new StandardServiceRegistryBuilder()
                .configure() // configures settings from hibernate.cfg.xml
                .build();
        try {
            sessionFactory = new MetadataSources(registry).buildMetadata().buildSessionFactory();
        } catch (Exception e) {
            System.err.println("Exception " + e);
            StandardServiceRegistryBuilder.destroy(registry);
        }
    }

    static void close() {
        if (sessionFactory != null) {
            sessionFactory.close();
        }
    }

    public ArbitruHibernateDB(Properties props) {
    }

    @Override
    public Arbitru findOne(Long aLong) {
        return null;
    }

    @Override
    public Iterable<Arbitru> findAll() throws SQLException {
        return null;
    }

    @Override
    public Arbitru save(Arbitru entity) throws FileNotFoundException {
        return null;
    }

    @Override
    public Arbitru delete(Long aLong) {
        return null;
    }

    @Override
    public Arbitru update(Arbitru entity1, Arbitru entity2) {
        return null;
    }

    @Override
    public Arbitru getAccount(String username, String parola) {
        initialize();
        try (Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();
                Arbitru arbitru = session.createQuery("from Arbitru where username = :username and parola = :parola", Arbitru.class)
                        .setParameter("username", username)
                        .setParameter("parola", parola)
                        .setMaxResults(1)
                        .uniqueResult();
                tx.commit();
                return arbitru;
            } catch (RuntimeException ex) {
                System.err.println("Eroare la getAccount " + ex);
                if (tx != null)
                    tx.rollback();
            }
        } catch (Exception e) {
            System.err.println("Exception " + e);
        }
        close();
        return null;
    }
}