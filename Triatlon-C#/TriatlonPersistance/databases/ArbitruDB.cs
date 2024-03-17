using System;
using System.Collections.Generic;
using log4net;
using System.Data.SQLite;
using System.Windows.Forms;
using Triatlon.repository.interfaces;
using TriatlonModel;

namespace ConcursTriatlon.repository.databases
{
    public class ArbitruDB : ArbitruRepository
    {
        private static readonly ILog log = LogManager.GetLogger("ArbitruDB");
        private string connectionString;

        public ArbitruDB(string connectionString)
        {
            log.Info("Creating ArbitruDB");
            this.connectionString = connectionString;
        }

        public Arbitru getAccount(string username, string parola)
        {
            log.InfoFormat("Entering getAccount with value {0}", username, parola);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from arbitru where username = @username and parola = @parola", connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@parola", parola);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string nume = reader.GetString(1);
                        string prenume = reader.GetString(2);

                        Arbitru arbitru = new Arbitru(nume, prenume, parola, username);
                        arbitru.setId(id);
                        reader.Close();

                        return arbitru;
                    }
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            log.InfoFormat("Exiting getAccount with value {0}", null);

            return null;
        }

        public Arbitru findOne(long id)
        {
            log.InfoFormat("Entering findOne with value {0}", id);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from arbitru where id=@id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string nume = reader.GetString(1);
                        string prenume = reader.GetString(2);
                        string parola = reader.GetString(3);
                        string username = reader.GetString(4);
                        Arbitru arbitru = new Arbitru(nume, prenume, parola,username);
                        arbitru.setId(id);
                        reader.Close();

                        return arbitru;
                    }
                } catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
                
            }
            log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Arbitru> findAll()
        {
            log.Info("Entering findAll");
            List<Arbitru> arbitri = new List<Arbitru>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from arbitru", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string nume = reader.GetString(1);
                        string prenume = reader.GetString(2);
                        string parola = reader.GetString(3);
                        string username = reader.GetString(4);

                        Arbitru arbitru = new Arbitru(nume, prenume, parola, username);
                        arbitru.setId(id);
                        arbitri.Add(arbitru);
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            log.InfoFormat("Exiting findAll with value {0}", arbitri);
            return arbitri;
        }

        public Arbitru save(Arbitru entity)
        {
            //Console.WriteLine("save");
            log.InfoFormat("Entering save with value {0}", entity);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("insert into arbitru(nume, prenume, parola,username) values (@nume, @prenume, @parola,@username)", connection);
                    command.Parameters.AddWithValue("@nume", entity.Nume);
                    command.Parameters.AddWithValue("@prenume", entity.Prenume);
                    command.Parameters.AddWithValue("@parola", entity.Parola);
                    command.Parameters.AddWithValue("@username", entity.Username);
                    command.ExecuteNonQuery();
                } catch (Exception e)
                {   
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            log.InfoFormat("Exiting save with value {0}", null);
           
            return null;
        }

        public Arbitru delete(long id)
        {
            return null;
        }

        public Arbitru update(Arbitru entity1, Arbitru entity2)
        {
            return null;
        }

        
    }
}