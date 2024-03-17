using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TriatlonModel;
using TriatlonServices;

namespace Triatlon
{
    public partial class MainController : Form, TriatlonObserverInterface
    {
        private TriatlonServiceInterface service;
        public Arbitru arbitru { get; set; }
        Proba proba;
        public MainController(TriatlonServiceInterface serviceApp)
        {
            try
            {
                this.service = serviceApp;
                InitializeComponent();

                //fillDataGridView();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void fillDataGridView()
        {
            setProba(getProba());
            initializeLeftTable();
            initializeRightTable();
        }

        private void initializeLeftTable()
        {
            dataGridViewParticipanti.DataSource = service.GetParticipantiDTO(this);
        }
        
        private void initializeRightTable()
        {
            //dataGridView1.DataSource = service.GetParticipantiDTOprobaActuala(proba, this);
            dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;

        }

        private void setProba(Proba proba)
        {
            textBoxProba.Text = proba.ToString();
            this.proba = proba;
        }


        public void setArbitru(Arbitru arbitru)
        {
            textBoxArbitru.Text = arbitru.ToString();
            this.arbitru = arbitru;

        }

        public Proba getProba()
        {
            Proba probaActuala = null;
            foreach (Proba proba in service.FindAllProbe(this))
            {
                if(proba.IdArbitru == arbitru.getId())
                {
                    probaActuala = proba;
                    probaActuala.setId(proba.getId());
                }
            }
            //this.proba = probaActuala;
            return probaActuala;
        }

        private void buttonAdaugaPunctaj_Click(object sender, EventArgs e)
        {
            try
            {
 
                ParticipantDTO participantDTO = dataGridViewParticipanti.SelectedRows[0].DataBoundItem as ParticipantDTO;
                long punctajDeAdaugat = long.Parse(textBoxAdaugaPunctaj.Text);

                Proba probaActuala = getProba();

                Participant participant = new Participant(participantDTO.Nume, participantDTO.Prenume);
                participant.setId(participantDTO.getId());

                Inscriere inscriere = new Inscriere(participant.getId(), probaActuala.getId(), punctajDeAdaugat);

                service.AddInscriere(inscriere,this);
                textBoxAdaugaPunctaj.Clear();
                initializeLeftTable();
                initializeRightTable();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Incepe delogarea!");
                service.logout(arbitru, this);
                this.Close();
                Application.Exit();
            } catch {
                MessageBox.Show("Eroare la inchidere!");
            }


        }

        public void updatePunctaj()
        {
            dataGridViewParticipanti.BeginInvoke(new Action((() => { fillDataGridView(); })));
        }
    }
}