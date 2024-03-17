using ConcursTriatlon.domain;
using System;
using System.Windows.Forms;
using Triatlon.service;

namespace Triatlon
{
    public partial class LogIn : Form
    {
        Service service;

        public LogIn(Service serviceApp)
        {
            InitializeComponent();
            this.service = serviceApp;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            return;
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void textBoxParola_TextChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
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
                    Arbitru arbitru = service.GetAccount(username, parola);
                    if (arbitru == null)
                    {
                        textBoxUsername.Clear();
                        textBoxParola.Clear();
                        MessageBox.Show("Nu exista arbitru cu aceste date de intrare!");
                        return;
                    }
                    else
                    {
                        //this.Hide();
                        textBoxUsername.Clear();
                        textBoxParola.Clear();
                        MainController mainController = new MainController(service, arbitru);
                        mainController.ShowDialog();
                        //this.Close();
                    }
                }
                
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }
    }
}