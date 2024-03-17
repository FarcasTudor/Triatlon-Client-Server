package repository.databases;

import domain.*;
import repository.Utils.JdbcUtils;
import repository.interfaces.InscriereRepository;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class InscriereDB implements InscriereRepository{
    private final JdbcUtils jdbcUtils;

    private static final Logger logger = LogManager.getLogger();

    public InscriereDB(Properties props) {
        logger.info("Initializing InscriereDB with properties: {} ", props);
        jdbcUtils = new JdbcUtils(props);
    }
    @Override
    public List<Long> findParticipantsByProba(Long idProba) {
        logger.traceEntry("finding participants by proba with {}", idProba);
        Connection con = jdbcUtils.getConnection();
        List<Long> participanti = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("select * from inscriere where id_proba= ?")) {
            preStmt.setLong(1, idProba);
            try(ResultSet result = preStmt.executeQuery()) {
                while(result.next()) {
                    Long id = result.getLong("id_participant");
                    participanti.add(id);
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
    public List<Inscriere> findInscriereByProbaAndParticipant(Long idProba, Long idParticipant) {
        logger.traceEntry("finding inscriere by proba and participant with {}", idProba, idParticipant);
        Connection con = jdbcUtils.getConnection();
        List<Inscriere> inscrieri = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("select * from inscriere where id_proba= ? and id_participant= ?")) {
            preStmt.setLong(1, idProba);
            preStmt.setLong(2, idParticipant);
            try(ResultSet result = preStmt.executeQuery()) {
                while(result.next()) {
                    Long id = result.getLong("id");
                    Long punctaj = result.getLong("punctaj");
                    Inscriere inscriere = new Inscriere(idParticipant, idProba, punctaj);
                    inscriere.setId(id);
                    inscrieri.add(inscriere);
                }
            } catch (SQLException e) {
                logger.error(e);
                System.err.println("Error DB " + e);
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit(inscrieri);
        return inscrieri;
    }

    @Override
    public Inscriere findOne(Long aLong) {
        logger.traceEntry("finding inscriere with {}", aLong);
        Connection con = jdbcUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("select * from inscriere where id= ?")) {
            preStmt.setLong(1, aLong);
            try(ResultSet result = preStmt.executeQuery()) {
                if(result.next()) {
                    Long id = result.getLong("id");
                    Long idParticipant = result.getLong("id_participant");
                    Long idProba = result.getLong("id_proba");
                    Long punctaj = result.getLong("punctaj");
                    Inscriere inscriere = new Inscriere(idParticipant, idProba, punctaj);
                    inscriere.setId(id);
                    logger.traceExit(inscriere);
                    return inscriere;
                }
            } catch (SQLException e) {
                logger.error(e);
                System.err.println("Error DB " + e);
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit("No inscriere found with id {}", aLong);
        return null;
    }

    @Override
    public Iterable<Inscriere> findAll() {
        logger.traceEntry();
        Connection con = jdbcUtils.getConnection();
        List<Inscriere> inscrieri = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("select * from inscriere")) {
            try(ResultSet result = preStmt.executeQuery()) {
                while(result.next()) {
                    Long id = result.getLong("id");
                    Long idParticipant = result.getLong("id_participant");
                    Long idProba = result.getLong("id_proba");
                    Long punctaj = result.getLong("punctaj");
                    Inscriere inscriere = new Inscriere(idParticipant, idProba, punctaj);
                    inscriere.setId(id);
                    inscrieri.add(inscriere);
                }
            } catch (SQLException e) {
                logger.error(e);
                System.err.println("Error DB " + e);
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit(inscrieri);
        return inscrieri;
    }

    @Override
    public Inscriere save(Inscriere entity) {
        logger.traceEntry("saving inscriere {} ", entity);
        Connection con = jdbcUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("insert into inscriere(id_participant, id_proba, punctaj) values (?,?,?)")) {
            preStmt.setLong(1, entity.getIdParticipant());
            preStmt.setLong(2, entity.getIdProba());
            preStmt.setLong(3, entity.getPunctaj());
            int result = preStmt.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit(entity);
        return entity;
    }

    @Override
    public Inscriere delete(Long aLong) {
        return null;
    }

    @Override
    public Inscriere update(Inscriere entity1, Inscriere entity2) {
        logger.traceEntry("updating inscriere {} ", entity1);
        Connection con = jdbcUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("update inscriere set punctaj=? where id_participant=? and id_proba=?")) {
            preStmt.setLong(1, entity2.getPunctaj());
            preStmt.setLong(2, entity2.getIdParticipant());
            preStmt.setLong(3, entity2.getIdProba());
            int result = preStmt.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit(entity2);
        return entity2;
    }


}
