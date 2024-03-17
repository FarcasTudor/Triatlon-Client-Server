using System;
using System.Windows.Forms;
using TriatlonModel;
using TriatlonServices;

namespace Triatlon
{
    public partial class LogIn : Form
    {
        TriatlonServiceInterface service;
        private MainController mainController;

        public LogIn(TriatlonServiceInterface serviceApp, MainController mainController)
        {
            InitializeComponent();
            this.service = serviceApp;
            this.mainController = mainController;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            return;
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            return;
        }

        private void textBoxParola_TextChanged(object sender, EventArgs e)
        {
            return;
        }

        private void logInButton_Click(object sender, EventArgs e)
        {
            try
            {
                string username = textBoxUsername.Text;
                string parola = textBoxParola.Text;
                if (username == "" || parola == "")
                {
                    MessageBox.Show("Username sau parola nu au fost introduse!");
                    return;
                }
                else
                {
                    ArbitruDTO arbitruDTO = new ArbitruDTO(username, parola);
                    Arbitru arbitru = service.login(arbitruDTO, mainController);
                    if (arbitru == null)
                    {
                        textBoxUsername.Clear();
                        textBoxParola.Clear();
                        MessageBox.Show("Nu exista arbitru cu aceste date de intrare!");
                        return;
                    }
                    else
                    {
                        mainController.setArbitru(arbitru);
                        mainController.arbitru = arbitru;
                        mainController.fillDataGridView();
                        
                        mainController.Show();
                        //mainController.ShowDialog();
                        this.Hide();

                    }
                }
                
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }
    }
}