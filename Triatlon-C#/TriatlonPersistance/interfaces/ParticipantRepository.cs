using System;
using System.Collections.Generic;
using TriatlonModel;

namespace ConcursTriatlon.repository.interfaces
{
    public interface ParticipantRepository : Repository<long, Participant>
    {
        List<long> findParticipantByName(String nume);

        Dictionary<Participant, long> GetParticipantiCuPunctaj();
    }
}

