using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConcursTriatlon.repository.databases;
using ConcursTriatlon.repository.interfaces;
using log4net;
using Triatlon.repository.interfaces;
using TriatlonServices;
using TriatlonNetworking;
using TriatlonNetworking.rpProtocols;

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
                TriatlonServiceInterface service = new TriatlonServiceRpcProxy("127.0.0.1", 55556);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var mainController = new MainController(service);
                Application.Run(new LogIn(service,mainController));

            } catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            
        }
        
    }
}