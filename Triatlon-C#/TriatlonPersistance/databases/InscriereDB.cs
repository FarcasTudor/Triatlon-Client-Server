using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Forms;
using ConcursTriatlon.repository.interfaces;
using log4net;
using TriatlonModel;

namespace ConcursTriatlon.repository.databases
{
    public class InscriereDB : InscriereRepository
    {
        public static readonly ILog Log = LogManager.GetLogger("InscriereDB");
        private string connectionString;
        
        public InscriereDB(string connectionString)
        {
            Log.Info("Creating InscriereDB");
            this.connectionString = connectionString;
        }
        
        public List<long> findParticipantByProba(long idProba)
        {
            Log.InfoFormat("Entering findParticipantByProba with value {0}", idProba);
            List<long> idParticipanti = new List<long>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select id_participant from inscriere where id_proba=@idProba", connection);
                    command.Parameters.AddWithValue("@idProba", idProba);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long idParticipant = reader.GetInt64(0);
                        idParticipanti.Add(idParticipant);
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            Log.InfoFormat("Exiting findParticipantByProba with value {0}", idParticipanti);
            return idParticipanti;
        }

        public List<long> findInscriereByProbaAndParticipant(long idProba, long idParticipant)
        {
            Log.InfoFormat("Entering findInscriereByProbaAndParticipant with value {0} and {1}", idProba, idParticipant);
            List<long> idInscrieri = new List<long>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select id from inscriere where id_proba=@idProba and id_participant=@idParticipant", connection);
                    command.Parameters.AddWithValue("@idProba", idProba);
                    command.Parameters.AddWithValue("@idParticipant", idParticipant);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long idInscriere = reader.GetInt64(0);
                        idInscrieri.Add(idInscriere);
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            Log.InfoFormat("Exiting findInscriereByProbaAndParticipant with value {0}", idInscrieri);
            return idInscrieri;
        }
        
        public Inscriere findOne(long id)
        {
            Log.InfoFormat("Entering findOne with value {0}", id);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from inscriere where id=@id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long idProba = reader.GetInt64(1);
                        long idParticipant = reader.GetInt64(2);
                        Inscriere inscriere = new Inscriere(id, idProba, idParticipant);
                        Log.InfoFormat("Exiting findOne with value {0}", inscriere);
                        reader.Close();

                        return inscriere;
                    }
                } catch (Exception e)
                {
                    Log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            Log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Inscriere> findAll()
        {
            Log.InfoFormat("Entering findAll");
            List<Inscriere> inscrieri = new List<Inscriere>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from inscriere", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        long idParticipant = reader.GetInt64(1);
                        long idProba = reader.GetInt64(2);
                        long punctaj = reader.GetInt64(3);
                        Inscriere inscriere = new Inscriere(idParticipant, idProba, punctaj);
                        inscriere.setId(id);
                        inscrieri.Add(inscriere);
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
                //connection.Close();
            }
            
            Log.InfoFormat("Exiting findAll with value {0}", inscrieri);
            return inscrieri;
        }

        public Inscriere save(Inscriere entity)
        {
            Log.InfoFormat("Entering save with value {0}", entity);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("INSERT INTO inscriere (id_participant, id_proba, punctaj) values (@idParticipant, @idProba, @punctaj)", connection);
                    command.Parameters.AddWithValue("@idParticipant", entity.IdParticipant);
                    command.Parameters.AddWithValue("@idProba", entity.IdProba);
                    command.Parameters.AddWithValue("@punctaj", entity.Punctaj);

                    MessageBox.Show("Inscriere IN PROCES!");
                    MessageBox.Show(entity.ToString());
                    command.ExecuteNonQuery();
                    MessageBox.Show("Inscriere REUSITA!");
                } catch (SQLiteException e)
                {
                    Log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
                
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            Log.InfoFormat("Exiting save with value {0}", entity);
            return entity;
        }

        public Inscriere delete(long id)
        {
            return null;
        }

        public Inscriere update(Inscriere entity1, Inscriere entity2)
        {
            Log.InfoFormat("Entering update with value {0} and {1}", entity1, entity2);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("update inscriere set punctaj=@punctaj where id_participant=@idParticipant and id_proba=@idProba", connection);
                    command.Parameters.AddWithValue("@punctaj", entity2.Punctaj);
                    command.Parameters.AddWithValue("@idParticipant", entity2.IdParticipant);
                    command.Parameters.AddWithValue("@idProba", entity2.IdProba);
                    command.ExecuteNonQuery();
                } catch (Exception e)
                {
                    Log.Error(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
            Log.InfoFormat("Exiting update with value {0}", entity2);
            return entity2;
        }
        
    }
}

