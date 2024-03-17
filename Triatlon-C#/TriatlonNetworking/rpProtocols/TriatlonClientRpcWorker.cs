using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TriatlonModel;
using TriatlonServices;

namespace TriatlonNetworking.rpProtocols
{
    public class TriatlonClientRpcWorker : TriatlonObserverInterface
    {

        private TriatlonServiceInterface server;
        private TcpClient connection;
        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;

        public TriatlonClientRpcWorker(TriatlonServiceInterface server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private static Response okResponse = new Response.Builder().type(ResponseType.OK).build();

        private object handleRequest(Request request)
        {
            if (request.type == RequestType.LOGIN)
            {
                ArbitruDTO arbDTO = (ArbitruDTO)request.data;
                try
                {
                    Arbitru arb = server.login(arbDTO, this);
                    return new Response.Builder().type(ResponseType.OK).data(arb).build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }

            if (request.type == RequestType.LOGOUT)
            {
                Arbitru arb = (Arbitru)request.data;
                try
                {
                    server.logout(arb, this);
                    connected = false;
                    return okResponse;
                }
                catch (Exception e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }

            if (request.type == RequestType.GET_PARTICIPANTI_CU_PUNCTAJ)
            {
                try
                {
                    List<ParticipantDTO> participants = server.GetParticipantiCuPunctaj(this);
                    return new Response.Builder().type(ResponseType.TAKE_PARTICIPANTI_CU_PUNCTAJ).data(participants).build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }

            if (request.type == RequestType.PROBE)
            {
                try
                {
                    IEnumerable<Proba> participants = server.FindAllProbe(this);
                    return new Response.Builder().type(ResponseType.TAKE_PROBE).data(participants).build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }
            /*if (request.type == RequestType.GET_PARTICIPANTI_DE_LA_PROBA)
            {
                Proba proba = (Proba)request.data;
                try
                {
                    Dictionary<Participant, long> participants = server.GetParticipantiDeLaProba(proba, this);
                    return new Response.Builder().type(ResponseType.TAKE_PARTICIPANTI_DE_LA_PROBA).data(participants).build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }

            }*/
            if (request.type == RequestType.ADD_INSCRIERE)
            {
                Inscriere inscriere = (Inscriere)request.data;
                try
                {
                    server.AddInscriere(inscriere, this);
                    return new Response.Builder().type(ResponseType.OK).build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }
            if(request.type == RequestType.Get_Participanti_DTO)
            {
                try
                {
                    IEnumerable<ParticipantDTO> participanti = server.GetParticipantiDTO(this);
                    return new Response.Builder().type(ResponseType.OK).data(participanti).build();
                }
                catch(Exception e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }
            if(request.type == RequestType.Get_Participanti_DTO_probaActuala)
            {
                Proba proba = (Proba)request.data;
                try
                {
                    IEnumerable<ParticipantDTO> participanti=  server.GetParticipantiDTOprobaActuala(proba, this);
                    return new Response.Builder().type(ResponseType.OK).data(participanti).build();
                }
                catch(Exception e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();

                }
            }
            return null;
        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        private void sendResponse(Response response)
        {
            lock (stream)
            {
                formatter.Serialize(stream, response);
                stream.Flush();
            }
        }

        public void updatePunctaj()
        {
            Response response = new Response.Builder().type(ResponseType.UPDATE_PUNCTAJ).build();
            try
            {
                sendResponse(response);
            }
            catch (Exception e)
            {
                throw new Exception("Sending error: " + e);
            }
        }
    }
}
