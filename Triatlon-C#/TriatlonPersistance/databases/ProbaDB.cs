using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using log4net;
using Triatlon.repository.interfaces;
using TriatlonModel;

namespace ConcursTriatlon.repository.databases
{
    public class ProbaDB : ProbaRepository
    {
        public static readonly ILog log = LogManager.GetLogger("ProbaDB");
        private string connectionString;
        
        public ProbaDB(string connectionString)
        {
            log.Info("Creating ProbaDB");
            this.connectionString = connectionString;
        }
        
        public Proba findOne(long id)
        {
            log.InfoFormat("Entering findOne with value {0}", id);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from proba where id=@id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string nume = reader.GetString(1);
                        long idArbitru = reader.GetInt64(2);
                        Proba proba = new Proba(nume,idArbitru);
                        proba.setId(id);
                        reader.Close();

                        return proba;
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


        public Dictionary<Participant, long> GetParticipantiProba(Proba proba)
        {
            log.InfoFormat("Entering GetParticipantiProba with value {0}", proba);
            Dictionary<Participant, long> participanti = new Dictionary<Participant, long>();
            long idProba = proba.getId();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select pa.id, pa.nume, pa.prenume, insc.punctaj from participant pa inner join inscriere insc on pa.id = insc.id_participant where insc.id_proba = @id_proba and insc.punctaj > 0 order by insc.punctaj desc", connection);
                    command.Parameters.AddWithValue("@id_proba", idProba);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long idParticipant = reader.GetInt64(0);
                        string nume = reader.GetString(1);
                        string prenume = reader.GetString(2);
                        long punctaj = reader.GetInt64(3);
                        Participant participant = new Participant(nume, prenume);
                        participant.setId(idParticipant);
                        participanti.Add(participant, punctaj);
                    }
                    reader.Close();

                    connection.Close();
                } catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            log.InfoFormat("Exiting GetParticipantiProba with value {0}", participanti);
            return participanti;
        }

        public IEnumerable<Proba> findAll()
        {
            log.Info("Entering findAll");
            List<Proba> probe = new List<Proba>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from proba", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string nume = reader.GetString(1);
                        long idArbitru = reader.GetInt64(2);
                        Proba proba = new Proba(nume,idArbitru);
                        proba.setId(id);
                        probe.Add(proba);
                    }
                    reader.Close();
                } catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
                connection.Close();
            }
            log.InfoFormat("Exiting findAll with value {0}", probe);
            return probe;
        }

        public Proba save(Proba entity)
        {
            log.InfoFormat("Entering save with value {0}", entity);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("insert into proba(nume, id_arbitru) values (@nume, @idArbitru)", connection);
                    command.Parameters.AddWithValue("@nume", entity.NumeProba);
                    command.Parameters.AddWithValue("@idArbitru", entity.IdArbitru);
                    
                    command.ExecuteNonQuery();
                    
                } catch (Exception e)
                {
                    log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            log.InfoFormat("Exiting save with value {0}", entity);
            return entity;
        }

        public Proba delete(long id)
        {
            return null;
        }

        public Proba update(Proba entity1, Proba entity2)
        {
            return null;
        }

        
    }
}

