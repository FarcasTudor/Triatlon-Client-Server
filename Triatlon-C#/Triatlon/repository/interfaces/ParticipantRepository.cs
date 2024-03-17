using System;
using System.Collections.Generic;
using ConcursTriatlon.domain;

namespace ConcursTriatlon.repository.interfaces
{
    public interface ParticipantRepository : Repository<long, Participant>
    {
        List<long> findParticipantByName(String nume);

        Dictionary<Participant, long> GetParticipantiCuPunctaj();
    }
}

