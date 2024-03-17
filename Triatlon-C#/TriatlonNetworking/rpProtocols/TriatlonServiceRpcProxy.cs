using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TriatlonModel;
using TriatlonServices;

namespace TriatlonNetworking.rpProtocols
{
    public class TriatlonServiceRpcProxy : TriatlonServiceInterface
    {
        private string host;
        private int port;

        private TriatlonObserverInterface client;
        private NetworkStream stream;
        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public TriatlonServiceRpcProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
        }

        private void initializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        private void startReader()
        {
            Thread tw = new Thread(run);
            tw.Start();
        }

        private bool isUpdate(Response response)
        {
            return response.type == ResponseType.UPDATE_PUNCTAJ;
        }

        public virtual void run()
        {
            while (!finished)
            {
                try
                {
                    object response = (Response)formatter.Deserialize(stream);
                    if (isUpdate((Response)response))
                    {
                        handleUpdate((Response)response);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue((Response)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Reading error " + ex);
                }
            }
        }

        private void handleUpdate(Response response)
        {
            if (response.type == ResponseType.UPDATE_PUNCTAJ)
            {
                try
                {
                    client.updatePunctaj();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }


        private Response readResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    response = responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }


        private void sendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new Exception("Error sending object " + e);
            }
        }

        private void closeConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


        public virtual Arbitru login(ArbitruDTO arbitru, TriatlonObserverInterface client)
        {
            initializeConnection();
            Request request = new Request.Builder().type(RequestType.LOGIN).data(arbitru).build();
            sendRequest(request);
            Response response = readResponse();
            if(response.type == ResponseType.ERROR)
            {
                string err = response.data.ToString();
                closeConnection();
                throw new Exception(err);
            }
            this.client = client;
            return (Arbitru)response.data;
        }

        public void logout(Arbitru arbitru, TriatlonObserverInterface client)
        {
            Request request = new Request.Builder().type(RequestType.LOGOUT).data(arbitru).build();
            sendRequest(request);
            Response response = readResponse();
            closeConnection();
            if (response.type == ResponseType.ERROR)
            {
                string err = response.data.ToString();
                throw new Exception(err);
            }

            MessageBox.Show("Logout realizat cu succes!");
        }

        public virtual List<ParticipantDTO> GetParticipantiCuPunctaj(TriatlonObserverInterface client)
        {
            Request request = new Request.Builder().type(RequestType.GET_PARTICIPANTI_CU_PUNCTAJ).build();
            sendRequest(request);
            Response response = readResponse();
            if (response.type == ResponseType.ERROR)
            {
                string err = response.data.ToString();
                throw new Exception(err);
            }
            List<ParticipantDTO> participanti = (List<ParticipantDTO>)response.data;
            return participanti;
        }

        public virtual IEnumerable<Proba> FindAllProbe(TriatlonObserverInterface client)
        {
            Request request = new Request.Builder().type(RequestType.PROBE).build();
            sendRequest(request);
            Response response = readResponse();
            if (response.type == ResponseType.ERROR)
            {
                string err = response.data.ToString();
                throw new Exception(err);
            }
            IEnumerable<Proba> probe = (IEnumerable<Proba>)response.data;
            return probe;
        }

        public void AddInscriere(Inscriere inscriere, TriatlonObserverInterface client)
        {
            Request request = new Request.Builder().type(RequestType.ADD_INSCRIERE).data(inscriere).build();
            sendRequest(request);
            Response response = readResponse();
            if (response.type == ResponseType.ERROR)
            {
                string err = response.data.ToString();
                throw new Exception(err);
            }
        }

        public virtual List<ParticipantDTO> GetParticipantiDeLaProba(Proba proba, TriatlonObserverInterface client)
        {
            /*Request request = new Request.Builder().type(RequestType.GET_PARTICIPANTI_DE_LA_PROBA).data(proba).build();
            sendRequest(request);
            Response response = readResponse();
            if (response.type == ResponseType.ERROR)
            {
                string err = response.data.ToString();
                throw new Exception(err);
            }
            Dictionary<Participant, long> participanti = (Dictionary<Participant, long>)response.data;
            return participanti;*/
            return null;
        }

        public IEnumerable<ParticipantDTO> GetParticipantiDTO(TriatlonObserverInterface client)
        {
            Request request = new Request.Builder().type(RequestType.Get_Participanti_DTO).build();
            sendRequest(request);
            Response response = readResponse();
            if (response.type == ResponseType.ERROR)
            {
                string err = response.data.ToString();
                throw new Exception(err);
            }
            IEnumerable<ParticipantDTO> participanti = (IEnumerable<ParticipantDTO>)response.data;
            return participanti;
        }

        public IEnumerable<ParticipantDTO> GetParticipantiDTOprobaActuala(Proba proba, TriatlonObserverInterface client)
        {
            Request request = new Request.Builder().type(RequestType.Get_Participanti_DTO_probaActuala).data(proba).build();
            sendRequest(request);
            Response response = readResponse();
            if (response.type == ResponseType.ERROR)
            {
                string err = response.data.ToString();
                throw new Exception(err);
            }
            IEnumerable<ParticipantDTO> participanti = (IEnumerable<ParticipantDTO>)response.data;
            return participanti;
        }

        public Arbitru GetAccount(string username, string parola)
        {
            return null;
        }
    }
}
