using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConcursTriatlon.domain;
using ConcursTriatlon.repository.databases;
using ConcursTriatlon.repository.interfaces;
using log4net;
using Triatlon.repository.interfaces;
using Triatlon.service;

namespace Triatlon
{
    static class Program
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                log.Info("Entering application.");

                //testArbitruDatabase();
                //testProbaDatabase();
                //testParticipantDatabase();
                //testInscriereDatabase();
                InscriereRepository inscriereDB = new InscriereDB(GetConnectionStringByName("triatlon"));
                //inscriereDB.save(new Inscriere(2, 2, 1000));
                ArbitruRepository arbitruDB = new ArbitruDB(GetConnectionStringByName("triatlon"));
                ParticipantRepository participantDB = new ParticipantDB(GetConnectionStringByName("triatlon"));
                ProbaRepository probaDB = new ProbaDB(GetConnectionStringByName("triatlon"));

                Service service = new Service(arbitruDB, inscriereDB, participantDB, probaDB);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LogIn(service));

            } catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            
        }
        
        /*static void testInscriereDatabase()
        {
            InscriereDB inscriereDB = new InscriereDB(GetConnectionStringByName("triatlon"));
            inscriereDB.save(new Inscriere(2, 2,1000));
            Inscriere inscriere1 = inscriereDB.findOne(3);
            Console.WriteLine(inscriere1);
            IEnumerable<Inscriere> inscrieri = inscriereDB.findAll();
            foreach (Inscriere inscriere in inscrieri)
            {
                Console.WriteLine(inscriere);
            }
            //inscriereDB.update(inscriere1, new Inscriere(1, 1, 9999));
        }

        static void testParticipantDatabase()
        {
            ParticipantDB participantDB = new ParticipantDB(GetConnectionStringByName("triatlon"));
            participantDB.save(new Participant("CEVA"+new Random().Next(), "CEVA"));
            Participant participant1 = participantDB.findOne(1);
            Console.WriteLine(participant1);
            IEnumerable<Participant> participantList = participantDB.findAll();
            foreach (Participant participant in participantList)
            {
                Console.WriteLine(participant);
            }
        }
        static void testArbitruDatabase()
        {
            ArbitruDB arbitruDB = new ArbitruDB(GetConnectionStringByName("triatlon"));
            arbitruDB.save(new Arbitru("Popescu", "Ion", "popescuion"));
            Arbitru arbitru1 = arbitruDB.findOne(1);
            Console.WriteLine(arbitru1);
            IEnumerable<Arbitru> arbitri = arbitruDB.findAll();
            foreach (Arbitru arbitru in arbitri)
            {
                Console.WriteLine(arbitru);
            }
        }
        static void testProbaDatabase()
        {
            ProbaDB probaDB = new ProbaDB(GetConnectionStringByName("triatlon"));
            probaDB.save(new Proba("Inot", 2));
            Proba proba1 = probaDB.findOne(1);
            IEnumerable<Proba> probe = probaDB.findAll();
            foreach (Proba proba in probe)
            {
                Console.WriteLine(proba);
            }
        }*/

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