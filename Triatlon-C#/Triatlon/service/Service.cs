using ConcursTriatlon.domain;
using ConcursTriatlon.repository.interfaces;
using log4net.Repository.Hierarchy;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triatlon.repository.interfaces;
using Triatlon.domain;
using System.Windows.Forms;
using ConcursTriatlon.repository.databases;
using System.Web.UI.WebControls.WebParts;

namespace Triatlon.service
{
    public class Service
    {
        private InscriereRepository inscriereRepository;
        private ParticipantRepository participantRepository;
        private ProbaRepository probaRepository;
        private ArbitruRepository arbitruRepository;

        public Service(ArbitruRepository arbitruRepository, InscriereRepository inscriereRepository, ParticipantRepository participantRepository, ProbaRepository probaRepository)
        {
            this.inscriereRepository = inscriereRepository;
            this.participantRepository = participantRepository;
            this.probaRepository = probaRepository;
            this.arbitruRepository = arbitruRepository;
        }

        public Arbitru GetAccount(string username, string password)
        {
            return arbitruRepository.getAccount(username, password);
        }

        public Dictionary<Participant, long> GetParticipantiCuPunctaj()
        {
            return participantRepository.GetParticipantiCuPunctaj();
        }

        public IEnumerable<Proba> FindAllProbe()
        {
            return probaRepository.findAll();
        }

        public void AddInscriere(Inscriere inscriere)
        {
            inscriereRepository.save(inscriere);
        }

        public Dictionary<Participant, long> GetParticipantiDeLaProba(Proba proba)
        {
            return probaRepository.GetParticipantiProba(proba);
        }

        internal IEnumerable<ParticipantDTO> GetParticipantiDTO()
        {
            List<ParticipantDTO> participantiDTO = new List<ParticipantDTO>();
            
            Dictionary<Participant, long> mapParticipantPunctaj = GetParticipantiCuPunctaj();
            foreach (var entry in mapParticipantPunctaj)
            {
                Participant part = entry.Key;
                ParticipantDTO pDTO = new ParticipantDTO(part.Nume, part.Prenume, entry.Value);
                pDTO.setId(part.getId());

                participantiDTO.Add(pDTO);
            }
            participantiDTO.Sort((x, y) => x.Nume.CompareTo(y.Nume));
            return participantiDTO;
        }

        internal IEnumerable<ParticipantDTO> GetParticipantiDTOprobaActuala(Proba proba)
        {
            List<ParticipantDTO> participantiDTOdreapta = new List<ParticipantDTO>();
            Dictionary<Participant, long> mapParticipantPunctajDreapta = GetParticipantiDeLaProba(proba);
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
            return participantiDTOdreapta;         
        }
    }
}
