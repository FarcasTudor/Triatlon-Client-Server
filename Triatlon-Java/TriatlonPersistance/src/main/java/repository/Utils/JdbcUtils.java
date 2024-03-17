package repository.Utils;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Properties;


public class JdbcUtils {

    private Properties jdbcProps;

    private static final Logger logger= LogManager.getLogger();

    public JdbcUtils(Properties props){
        jdbcProps=props;
    }

    private  Connection instance=null;

    private Connection getNewConnection(){
        logger.traceEntry();

        String driver=jdbcProps.getProperty("repository.driver");
        String url=jdbcProps.getProperty("repository.url");
        String user=jdbcProps.getProperty("repository.user");
        String pass=jdbcProps.getProperty("repository.pass");

        logger.info("trying to connect to database ... {}",url);
        logger.info("user: {}",user);
        logger.info("pass: {}", pass);
        Connection con=null;
        try {
            Class.forName(driver);
            con= DriverManager.getConnection(url,user,pass);
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error getting connection "+e);
        } catch (ClassNotFoundException e) {
            System.out.println("Error loading driver "+e);
        }
        return con;
    }

    public Connection getConnection(){
        try {
            if (instance==null || instance.isClosed())
                instance=getNewConnection();

        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB "+e);
        }
        return instance;
    }
}
