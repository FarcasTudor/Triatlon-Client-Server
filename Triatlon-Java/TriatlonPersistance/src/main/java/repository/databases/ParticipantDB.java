package repository.databases;

import domain.*;
import repository.Utils.JdbcUtils;
import repository.interfaces.ParticipantRepository;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.FileNotFoundException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

public class ParticipantDB implements ParticipantRepository {
    private final JdbcUtils jdbcUtils;

    private static final Logger logger = LogManager.getLogger();

    public ParticipantDB(Properties props) {
        logger.info("Initializing ParticipantDB with properties: {} ", props);
        jdbcUtils = new JdbcUtils(props);
    }
    @Override
    public List<Participant> findParticipantByName(String nume) {
        logger.traceEntry("finding participant with {}", nume);
        Connection con = jdbcUtils.getConnection();
        List<Participant> participanti = new ArrayList<>();
           try(PreparedStatement preStmt = con.prepareStatement("select * from participant where nume= ?")) {
                preStmt.setString(1, nume);
                try(ResultSet result = preStmt.executeQuery()) {
                    while(result.next()) {
                        Long id = result.getLong("id");
                        String prenume = result.getString("prenume");
                        Participant participant = new Participant(nume, prenume);
                        participant.setId(id);
                        participanti.add(participant);
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

    @Override
    public Map<Participant, Long> getParticipantiCuPunctaj() {
        logger.traceEntry();
        Connection con = jdbcUtils.getConnection();
        Map<Participant, Long> participanti = new HashMap<>();
        try(PreparedStatement preStmt = con.prepareStatement("SELECT pa.id, pa.nume, pa.prenume, SUM(insc.punctaj) as puntajTotal from participant pa inner join inscriere insc on pa.id = insc.id_participant inner join proba pr on pr.id = insc.id_proba group by pa.id, pa.nume, pa.prenume order by pa.nume")) {
            try(ResultSet result = preStmt.executeQuery()) {
                while(result.next()) {
                    Long id = result.getLong("id");
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    Long punctaj = result.getLong("puntajTotal");
                    Participant participant = new Participant(nume, prenume);
                    participant.setId(id);
                    participanti.put(participant, punctaj );
                }
            } catch (SQLException e) {
                logger.error(e);
                System.err.println("Error DB " + e);
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        return participanti;
    }

    @Override
    public Participant findOne(Long aLong) {
        logger.traceEntry("finding participant with {}", aLong);
        Connection con = jdbcUtils.getConnection();
        Participant participant = null;
        try(PreparedStatement preStmt = con.prepareStatement("select * from participant where id= ?")) {
            preStmt.setLong(1, aLong);
            try(ResultSet result = preStmt.executeQuery()) {
                while(result.next()) {
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    participant = new Participant(nume, prenume);
                    participant.setId(aLong);
                }
            } catch (SQLException e) {
                logger.error(e);
                System.err.println("Error DB " + e);
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit(participant);
        return participant;
    }

    @Override
    public Iterable<Participant> findAll() {
        logger.traceEntry();
        Connection con = jdbcUtils.getConnection();
        List<Participant> participanti = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("select * from participant")) {
            try(ResultSet result = preStmt.executeQuery()) {
                while(result.next()) {
                    Long id = result.getLong("id");
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    Participant participant = new Participant(nume, prenume);
                    participant.setId(id);
                    participanti.add(participant);
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

    @Override
    public Participant save(Participant entity) throws FileNotFoundException {
        logger.traceEntry("saving participant {} ", entity);
        Connection con = jdbcUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("insert into participant(nume, prenume) values (?, ?)")) {
            preStmt.setString(1, entity.getNume());
            preStmt.setString(2, entity.getPrenume());
            int result = preStmt.executeUpdate();
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit(entity);
        return entity;
    }

    @Override
    public Participant delete(Long aLong) {
        return null;
    }

    @Override
    public Participant update(Participant entity1, Participant entity2) {
        return null;
    }


}
