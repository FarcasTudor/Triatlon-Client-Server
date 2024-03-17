using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriatlonModel;

namespace TriatlonServices
{
    public interface TriatlonServiceInterface
    {
        //Arbitru GetAccount(string username, string password);
        Arbitru GetAccount(String username, String parola);
        Arbitru login(ArbitruDTO arbitru, TriatlonObserverInterface client);

        void logout(Arbitru arbitru, TriatlonObserverInterface client);
        List<ParticipantDTO> GetParticipantiCuPunctaj(TriatlonObserverInterface client);


        IEnumerable<Proba> FindAllProbe(TriatlonObserverInterface client);

        void AddInscriere(Inscriere inscriere, TriatlonObserverInterface client);

        List<ParticipantDTO> GetParticipantiDeLaProba(Proba proba, TriatlonObserverInterface client);

        IEnumerable<ParticipantDTO> GetParticipantiDTO(TriatlonObserverInterface client);

        IEnumerable<ParticipantDTO> GetParticipantiDTOprobaActuala(Proba proba, TriatlonObserverInterface client);

    }
}
