package repository.databases;

import domain.*;
import repository.Utils.JdbcUtils;
import repository.interfaces.ProbaRepository;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.FileNotFoundException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

public class ProbaDB implements ProbaRepository {
    private final JdbcUtils jdbcUtils;

    private static final Logger logger = LogManager.getLogger();

    public ProbaDB(Properties props) {
        logger.info("Initializing ProbaDB with properties: {} ", props);
        jdbcUtils = new JdbcUtils(props);
    }


    @Override
    public Proba findOne(Long aLong) {
        return null;
    }

    @Override
    public Iterable<Proba> findAll() {
        logger.traceEntry();
        Connection con = jdbcUtils.getConnection();
        List<Proba> probe = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("select * from proba")) {
            try(ResultSet result = preStmt.executeQuery()) {
                while(result.next()) {

                    Long id = result.getLong("id");
                    String nume = result.getString("nume");
                    Long id_arbitru = result.getLong("id_arbitru");

                    Proba proba = new Proba(nume, id_arbitru);
                    proba.setId(id);
                    probe.add(proba);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit(probe);
        return probe;
    }

    @Override
    public Proba save(Proba entity) throws FileNotFoundException {
        logger.traceEntry("saving arbitru {} ", entity);
        Connection con = jdbcUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("insert into proba (nume, id_arbitru) values (?,?)")) {
            preparedStatement.setString(1, entity.getNumeProba());
            preparedStatement.setLong(2, entity.getIdArbitru());


            int result = preparedStatement.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (Exception e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit();
        return null;
    }

    @Override
    public Proba delete(Long aLong) {
        return null;
    }

    @Override
    public Proba update(Proba entity1, Proba entity2) {
        return null;
    }

    @Override
    public Map<Participant, Long> getParticipantiProba(Proba proba) {
        logger.traceEntry("finding participanti with {}", proba);
        Connection con = jdbcUtils.getConnection();
        Map<Participant, Long> participanti = new HashMap<>();
        Long idProba = proba.getId();
        System.out.println(idProba);
        try(PreparedStatement preStmt = con.prepareStatement("select pa.id, pa.nume, pa.prenume, insc.punctaj from participant pa inner join inscriere insc on pa.id = insc.id_participant where insc.id_proba = ? and insc.punctaj > 0 order by insc.punctaj desc")) {
            preStmt.setLong(1, idProba);
            try(ResultSet result = preStmt.executeQuery()) {
                while(result.next()) {
                    Long id = result.getLong("id");
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    Long punctaj = result.getLong("punctaj");
                    Participant participant = new Participant(nume, prenume);
                    participant.setId(id);
                    System.out.println(participant);
                    participanti.put(participant, punctaj);
                    System.out.println(participant + " " + punctaj);
                }
            } catch (SQLException e) {
                logger.error(e);
                System.err.println("Error DB " + e);
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit(participanti);
        return participanti;
    }
}
