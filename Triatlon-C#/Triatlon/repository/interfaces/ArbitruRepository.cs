using ConcursTriatlon.domain;
using ConcursTriatlon.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triatlon.repository.interfaces
{
    public interface ArbitruRepository : Repository<long, Arbitru>
    {
        Arbitru getAccount(String username, String parola);
    }
}
