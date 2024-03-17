using System.Collections.Generic;
using ConcursTriatlon.repository;
using TriatlonModel;

namespace ConcursTriatlon.repository.interfaces
{
    public interface InscriereRepository : Repository<long, Inscriere>
    {
        List<long> findParticipantByProba(long idProba);
        List<long> findInscriereByProbaAndParticipant(long idProba, long idParticipant);

    }
}

