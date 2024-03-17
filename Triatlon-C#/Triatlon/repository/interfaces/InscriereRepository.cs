using System.Collections.Generic;
using ConcursTriatlon.domain;
using ConcursTriatlon.repository;

namespace ConcursTriatlon.repository.interfaces
{
    public interface InscriereRepository : Repository<long, Inscriere>
    {
        List<long> findParticipantByProba(long idProba);
        List<long> findInscriereByProbaAndParticipant(long idProba, long idParticipant);

    }
}

