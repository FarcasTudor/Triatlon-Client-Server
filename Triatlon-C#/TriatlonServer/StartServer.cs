using ConcursTriatlon.repository.databases;
using ConcursTriatlon.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Triatlon;
using Triatlon.Protocol;
using Triatlon.repository.interfaces;
using TriatlonNetworking.protobuff;
using TriatlonServices;
using TriatlonNetworking.utils;
using TriatlonNetworking.rpProtocols;

namespace TriatlonServer
{
    public class StartServer
    {
        public static void Main(string[] args)
        {
            InscriereRepository inscriereDB = new InscriereDB(GetConnectionStringByName("triatlon"));
            //inscriereDB.save(new Inscriere(2, 2, 1000));
            ArbitruRepository arbitruDB = new ArbitruDB(GetConnectionStringByName("triatlon"));
            ParticipantRepository participantDB = new ParticipantDB(GetConnectionStringByName("triatlon"));
            ProbaRepository probaDB = new ProbaDB(GetConnectionStringByName("triatlon"));

            TriatlonServiceInterface service = new Service(arbitruDB, inscriereDB, participantDB, probaDB);

            SerialTriatlonServer server = new SerialTriatlonServer("127.0.0.1", 55556, service);
            server.Start();

        }

        public class SerialTriatlonServer : ConcurrentAbstractServer
        {
            private TriatlonServiceInterface server;
            //private TriatlonClientRpcWorker worker;
            private ProtoV3TriatlonWorker worker;
            public SerialTriatlonServer(string host, int port, TriatlonServiceInterface server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialChatServer... " + server);
            }
            protected override Thread createWorker(TcpClient client)
            {
                //worker = new TriatlonClientRpcWorker(server, client);
                worker = new ProtoV3TriatlonWorker(server, client);
                return new Thread(new ThreadStart(worker.run));
            }
        }

        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}
