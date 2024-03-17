using ConcursTriatlon.domain;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Triatlon.domain;
using Triatlon.service;

namespace Triatlon
{
    public partial class MainController : Form
    {
        Service service;
        Arbitru arbitru;
        Proba proba;
        public MainController(Service serviceApp, Arbitru loggedArbitru)
        {
            try
            {
                InitializeComponent();
                this.service = serviceApp;
                setArbitru(loggedArbitru);
                setProba(getProba());
                initializeLeftTable();
                initializeRightTable();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void initializeLeftTable()
        {
            dataGridViewParticipanti.DataSource = service.GetParticipantiDTO();
        }
        
        private void initializeRightTable()
        {
            dataGridView1.DataSource = service.GetParticipantiDTOprobaActuala(proba);
        }

        private void setProba(Proba proba)
        {
            textBoxProba.Text = proba.ToString();
            this.proba = proba;
        }


        private void setArbitru(Arbitru arbitru)
        {
            textBoxArbitru.Text = arbitru.ToString();
            this.arbitru = arbitru;

        }

        public Proba getProba()
        {
            Proba probaActuala = null;
            foreach (Proba proba in service.FindAllProbe())
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

                service.AddInscriere(inscriere);

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
                this.Close();
            } catch {
                MessageBox.Show("Eroare la inchidere!");
            }


        }
    }
}