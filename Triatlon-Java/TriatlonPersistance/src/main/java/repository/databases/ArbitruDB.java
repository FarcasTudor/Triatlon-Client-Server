package repository.databases;

import domain.*;
import repository.Utils.JdbcUtils;
import repository.interfaces.ArbitruRepository;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.FileNotFoundException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class ArbitruDB implements ArbitruRepository {

    private final JdbcUtils jdbcUtils;

    private static final Logger logger = LogManager.getLogger();

    public ArbitruDB(Properties props) {
        logger.info("Initializing ArbitruDB with properties: {} ", props);
        System.out.println("Initializing ArbitruDB with properties: {} "+ props);
        jdbcUtils = new JdbcUtils(props);
    }


    @Override
    public Arbitru findOne(Long aLong) {
        logger.traceEntry("finding arbitru with {}", aLong);
        Connection con = jdbcUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("select * from arbitru where id= ?")) {
            preStmt.setLong(1, aLong);
            try(ResultSet result = preStmt.executeQuery()) {
                if(result.next()) {
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    String username = result.getString("username");
                    String parola = result.getString("parola");
                    Arbitru arbitru = new Arbitru(nume, prenume, username, parola);
                    arbitru.setId(aLong);
                    logger.traceExit(arbitru);
                    return arbitru;
                }
            } catch (SQLException e) {
                logger.error(e);
                System.err.println("Error DB " + e);
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        return null;
    }

    @Override
    public Iterable<Arbitru> findAll() throws SQLException {
        logger.traceEntry();
        Connection con = jdbcUtils.getConnection();
        List<Arbitru> arbitri = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("select * from arbitru")) {
            try(ResultSet result = preStmt.executeQuery()) {
                while(result.next()) {
                    Long id = result.getLong("id");
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    String username = result.getString("username");
                    String parola = result.getString("parola");
                    Arbitru arbitru = new Arbitru(nume, prenume,username,  parola);
                    arbitru.setId(id);
                    arbitri.add(arbitru);
                    logger.traceExit(arbitri);
                }

            } catch (SQLException e) {
                logger.error(e);
                System.err.println("Error DB " + e);
            }
            return arbitri;
        }
    }

    @Override
    public Arbitru save(Arbitru entity) throws FileNotFoundException {
        logger.traceEntry("saving arbitru {} ", entity);
        Connection con = jdbcUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("insert into arbitru (nume, prenume, username, parola) values (?, ?, ?, ?)")) {
            preStmt.setString(1, entity.getNume());
            preStmt.setString(2, entity.getPrenume());
            preStmt.setString(3, entity.getUsername());
            preStmt.setString(3, entity.getParola());
            int result = preStmt.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        logger.traceExit();
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
        logger.traceEntry("finding arbitru with {}", username);
        Connection con = jdbcUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("select * from arbitru where username = ? and parola = ?")) {
            preStmt.setString(1, username);
            preStmt.setString(2, parola);
            try(ResultSet result = preStmt.executeQuery()) {
                if(result.next()) {
                    Long id = result.getLong("id");
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    Arbitru arbitru = new Arbitru(nume, prenume, username, parola);
                    arbitru.setId(id);
                    logger.traceExit(arbitru);
                    return arbitru;
                }
            } catch (SQLException e) {
                logger.error(e);
                System.err.println("Error DB " + e);
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB " + e);
        }
        return null;
    }
}
