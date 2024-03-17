using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Web.ModelBinding;
using Google.Protobuf;
using Triatlon.Protocol;
using TriatlonServices;
using TriatlonNetworking.protobuff;


namespace TriatlonNetworking.protobuff
{
    public class ProtoV3TriatlonWorker : TriatlonObserverInterface
    {
        private TriatlonServiceInterface server;
        private TcpClient connection;
        private NetworkStream stream;
        private volatile bool connected;
        
        public ProtoV3TriatlonWorker(TriatlonServiceInterface server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        
        public void run()
        {
            while (connected)
            {
                try
                {
                    TriatlonRequest request = TriatlonRequest.Parser.ParseDelimitedFrom(stream);
                    TriatlonResponse response = handleRequest(request);
                    if (response != null)
                    {
                        sendResponse(response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine(e.Message);
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

        private void sendResponse(TriatlonResponse response)
        {
            Console.WriteLine("sending response "+response);
            lock (stream)
            {
                response.WriteDelimitedTo(stream);
                stream.Flush();
            }

        }

        private TriatlonResponse handleRequest(TriatlonRequest request)
        {
            TriatlonResponse response = null;
            TriatlonRequest.Types.Type reqType = request.Type;
            switch (reqType)
            {
                case TriatlonRequest.Types.Type.Login:
                {
                    Console.WriteLine("Login request ...");
                    TriatlonModel.ArbitruDTO arbDTO = ProtoUtils.getArbitruDTO(request);
                    TriatlonModel.Arbitru arbitru = null;
                    try
                    {
                        lock (server)
                        {   
                            arbitru = server.login(arbDTO, this);
                        }
                        return ProtoUtils.createLogInResponse(arbitru);
                    }
                    catch(Exception e)
                    {
                        connected = false;
                        return ProtoUtils.createErrorResponse(e.Message);
                    }
                }
                
                case TriatlonRequest.Types.Type.Logout:
                {
                    Console.WriteLine("Logout request");
                    TriatlonModel.Arbitru arb = ProtoUtils.getArbitru(request);
                    try
                    {
                        lock (server)
                        {
                            server.logout(arb, this);
                        }
                        connected = false;
                        return ProtoUtils.createOkResponse();
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.createErrorResponse(e.Message);
                    }
                }

                case TriatlonRequest.Types.Type.GetParticipantiCuPunctaj:
                {
                    Console.WriteLine("GetParticipantiCuPunctaj request ...");
                    try
                    {
                        List<TriatlonModel.ParticipantDTO> participanti = null;
                        lock (server)
                        {
                            participanti = server.GetParticipantiCuPunctaj(this);
                        }
                        return ProtoUtils.createGetParticipantiCuPunctajResponse(participanti);
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.createErrorResponse(e.Message);
                    }
                }
                case TriatlonRequest.Types.Type.GetProbe:
                {
                    Console.WriteLine("GetProbe request ...");
                    try
                    {
                        IEnumerable<TriatlonModel.Proba> probe = null;
                        lock (server)
                        {
                            probe = server.FindAllProbe(this);
                        }
                        Console.WriteLine("GetProbe request ... VEZI DACA LISTA E GOALA: "+probe);
                        return ProtoUtils.createGetProbeResponse(probe);
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.createErrorResponse(e.Message);
                    }
                }
                case TriatlonRequest.Types.Type.GetParticipantiDeLaProba:
                {
                    Console.WriteLine("GetParticipantiDeLaProba request ...");
                    TriatlonModel.Proba proba = ProtoUtils.getProba(request);
                    List<TriatlonModel.ParticipantDTO> participanti = null;
                    try
                    {
                        lock (server)
                        {
                            participanti = server.GetParticipantiDeLaProba(proba, this);
                        }
                        return ProtoUtils.createGetParticipantiDeLaProbaResponse(participanti);
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.createErrorResponse(e.Message);
                    }
                }
                case TriatlonRequest.Types.Type.AddInscriere:
                {
                    Console.WriteLine("AddInscriere request ...");
                    TriatlonModel.Inscriere inscriere = ProtoUtils.getInscriere(request);
                    try
                    {
                        lock (server)
                        {
                            server.AddInscriere(inscriere, this);
                        }
                        return ProtoUtils.createOkResponse();
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.createErrorResponse(e.Message);
                    }
                }
            }

            return response;
        }
        
        public virtual void updatePunctaj()
        {
            Console.WriteLine("Update punctaj request ...");
            try
            {
                sendResponse(ProtoUtils.createUpdatePunctajResponse());
            }
            catch (Exception e)
            {
                throw new Exception("Sending error: " + e);
            }
        }
    }
}