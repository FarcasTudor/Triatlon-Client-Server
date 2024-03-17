using ConcursTriatlon.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triatlon.repository.interfaces;
using System.Windows.Forms;
using ConcursTriatlon.repository.databases;
using System.Web.UI.WebControls.WebParts;
using TriatlonServices;
using TriatlonModel;

namespace Triatlon
{
    public class Service : TriatlonServiceInterface
    {
        private InscriereRepository inscriereRepository;
        private ParticipantRepository participantRepository;
        private ProbaRepository probaRepository;
        private ArbitruRepository arbitruRepository;

        private Dictionary<long, TriatlonObserverInterface> loggedClients;

        public Service(ArbitruRepository arbitruRepository, InscriereRepository inscriereRepository, ParticipantRepository participantRepository, ProbaRepository probaRepository)
        {
            this.inscriereRepository = inscriereRepository;
            this.participantRepository = participantRepository;
            this.probaRepository = probaRepository;
            this.arbitruRepository = arbitruRepository;
            this.loggedClients = new Dictionary<long, TriatlonObserverInterface>();
        }

        public Arbitru getAccount(string username, string parola)
        {
            return arbitruRepository.getAccount(username, parola);
        }

        public Arbitru login(ArbitruDTO arbitru, TriatlonObserverInterface client)
        {
            Arbitru arbitru1 = arbitruRepository.getAccount(arbitru.User, arbitru.Pass);
            if (arbitru1 != null)
            {
                if (loggedClients.ContainsKey(arbitru1.getId()))
                    throw new Exception("User already logged in.");
                loggedClients[arbitru1.getId()] = client;
                return arbitru1;
            }
            else
                throw new Exception("Authentication failed.");
        }
        public void logout(Arbitru arbitru, TriatlonObserverInterface client)
        {
            bool removed = loggedClients.Remove(arbitru.getId());
            if (!removed)
                throw new Exception("User " + arbitru.getId() + " is not logged in.");
        }

        public Arbitru GetAccount(string username, string password)
        {
            return arbitruRepository.getAccount(username, password);
        }

        public List<ParticipantDTO> GetParticipantiCuPunctaj(TriatlonObserverInterface client)
        {
            IDictionary<Participant, long> parti = participantRepository.GetParticipantiCuPunctaj();
            List<ParticipantDTO> listaParticipantiDTO = new List<ParticipantDTO>();
            List<Participant> participanti = parti.Keys.ToList();
            List<long> puncte = parti.Values.ToList();
            for(int i=0;i<participanti.Count;i++) {
                ParticipantDTO partDTO = new ParticipantDTO(participanti[i].Nume, participanti[i].Prenume, puncte[i]);
                partDTO.setId(participanti[i].getId());
                listaParticipantiDTO.Add(partDTO);
            }
            return listaParticipantiDTO;
        }

        public IEnumerable<Proba> FindAllProbe(TriatlonObserverInterface client)
        {
            return probaRepository.findAll();
        }

        public void AddInscriere(Inscriere inscriere, TriatlonObserverInterface client)
        {
            Inscriere inscriere1 = inscriereRepository.save(inscriere);
            if (inscriere1 != null)
            {
                foreach (long key in loggedClients.Keys)
                {
                    Console.WriteLine("Notificare fiecare client XXXXXXXXXXXXXX");
                    TriatlonObserverInterface receiver = loggedClients[key];
                    Task.Run(() => { receiver.updatePunctaj(); });
                }
            }
            else throw new Exception("Nu s-a putut adauga inscrierea!");
        }

        public List<ParticipantDTO> GetParticipantiDeLaProba(Proba proba, TriatlonObserverInterface client)
        {
            Dictionary<Participant, long> parti = probaRepository.GetParticipantiProba(proba);
            List<ParticipantDTO> listaParticDTO = new List<ParticipantDTO>();

            List<Participant> participanti = parti.Keys.ToList();
            List<long> puncte = parti.Values.ToList();
            for(int i=0;i<participanti.Count;i++) {
                ParticipantDTO partDTO = new ParticipantDTO(participanti[i].Nume, participanti[i].Prenume, puncte[i]);
                partDTO.setId(participanti[i].getId());
                listaParticDTO.Add(partDTO);
            }
            return listaParticDTO;
        }

        public IEnumerable<ParticipantDTO> GetParticipantiDTO(TriatlonObserverInterface client)
        {
            List<ParticipantDTO> participantiDTO = GetParticipantiCuPunctaj(client);
            
            /*Dictionary<Participant, long> mapParticipantPunctaj = GetParticipantiCuPunctaj(client);
            foreach (var entry in mapParticipantPunctaj)
            {
                Participant part = entry.Key;
                ParticipantDTO pDTO = new ParticipantDTO(part.Nume, part.Prenume, entry.Value);
                pDTO.setId(part.getId());

                participantiDTO.Add(pDTO);
            }*/
            participantiDTO.Sort((x, y) => x.Nume.CompareTo(y.Nume));
            return participantiDTO;
        }

        public IEnumerable<ParticipantDTO> GetParticipantiDTOprobaActuala(Proba proba, TriatlonObserverInterface client)
        {
            /*List<ParticipantDTO> participantiDTOdreapta = new List<ParticipantDTO>();
            Dictionary<Participant, long> mapParticipantPunctajDreapta = GetParticipantiDeLaProba(proba,client);
            Dictionary<long, ParticipantDTO> latestParticipants = new Dictionary<long, ParticipantDTO>();

            foreach (KeyValuePair<Participant, long> entry in mapParticipantPunctajDreapta)
            {
                Participant part = entry.Key;
                long participantId = part.getId();
                if (latestParticipants.ContainsKey(participantId))
                {
                    ParticipantDTO latestDTO = latestParticipants[participantId];
                    latestDTO.PunctajTotal = entry.Value;
                }
                else
                {
                    // If not, create a new ParticipantDTO object and add it to the list
                    ParticipantDTO pDTO = new ParticipantDTO(part.Nume, part.Prenume, entry.Value);
                    pDTO.setId(participantId);
                    participantiDTOdreapta.Add(pDTO);

                    // Store the latest ParticipantDTO object for this participant ID
                    latestParticipants[participantId] = pDTO;
                }
            }
            participantiDTOdreapta.Sort((p1, p2) => p2.PunctajTotal.CompareTo(p1.PunctajTotal));
            return participantiDTOdreapta;    */
            return null;
        }

        
    }
}
