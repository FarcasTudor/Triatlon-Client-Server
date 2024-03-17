using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using ConcursTriatlon.repository.interfaces;
using log4net;
using TriatlonModel;

namespace ConcursTriatlon.repository.databases
{
    public class ParticipantDB : ParticipantRepository
    {
        public static readonly ILog log = LogManager.GetLogger("ParticipantDB");
        private string connectionString;
        
        public ParticipantDB(string connectionString)
        {
            log.Info("Creating ParticipantDB");
            this.connectionString = connectionString;
        }
    
        public List<long> findParticipantByName(string nume)
        {
            log.InfoFormat("Entering findParticipantByName with value {0}", nume);
            List<long> ids = new List<long>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select id from participant where nume=@nume", connection);
                    command.Parameters.AddWithValue("@nume", nume);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        ids.Add(id);
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            log.InfoFormat("Exiting findParticipantByName with value {0}", ids);
            return ids;
        }
    
        public Participant findOne(long id)
        {
            log.InfoFormat("Entering findOne with value {0}", id);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from participant where id=@id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string nume = reader.GetString(1);
                        string prenume = reader.GetString(2);
                        Participant participant = new Participant(nume, prenume);
                        participant.setId(id);

                        reader.Close();

                        return participant;
                    }

                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public Dictionary<Participant, long> GetParticipantiCuPunctaj()
        {
            log.Info("Entering GetParticipantiCuPunctaj");
            Dictionary<Participant, long> participanti = new Dictionary<Participant, long>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("SELECT pa.id, pa.nume, pa.prenume, SUM(insc.punctaj) as puntajTotal from participant pa inner join inscriere insc on pa.id = insc.id_participant inner join proba pr on pr.id = insc.id_proba group by pa.id, pa.nume, pa.prenume order by pa.nume", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string nume = reader.GetString(1);
                        string prenume = reader.GetString(2);
                        long punctajTotal = reader.GetInt64(3);
                        Participant participant = new Participant(nume, prenume);
                        participant.setId(id);
                        participanti.Add(participant, punctajTotal);
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
                connection.Close(); 
            }
            log.InfoFormat("Exiting GetParticipantiCuPunctaj with value {0}", participanti);
            return participanti;
        }

        public IEnumerable<Participant> findAll()
        {
            log.Info("Entering findAll");
            List<Participant> participanti = new List<Participant>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from participant", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string nume = reader.GetString(1);
                        string prenume = reader.GetString(2);
                        Participant participant = new Participant(nume, prenume);
                        participant.setId(id);
                        participanti.Add(participant);
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            log.InfoFormat("Exiting findAll with value {0}", participanti);
            return participanti;
        }

        public Participant save(Participant entity)
        {
            log.InfoFormat("Entering save with value {0}", entity);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("insert into participant(nume, prenume) values (@nume, @prenume)", connection);
                    command.Parameters.AddWithValue("@nume", entity.Nume);
                    command.Parameters.AddWithValue("@prenume", entity.Prenume);
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

        public Participant delete(long id)
        {
            return null;
        }

        public Participant update(Participant entity1, Participant entity2)
        {
            return null;
        }

        
    }
}

