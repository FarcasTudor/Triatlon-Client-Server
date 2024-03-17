using ConcursTriatlon.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriatlonModel;

namespace Triatlon.repository.interfaces
{
    public interface ProbaRepository : Repository<long, Proba>
    {
        Dictionary<Participant, long> GetParticipantiProba(Proba proba);
    }
}
