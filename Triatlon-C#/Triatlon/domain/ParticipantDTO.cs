using ConcursTriatlon.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triatlon.domain
{
    internal class ParticipantDTO : Entity<long>
    {
        private string nume;
        private string prenume;
        private long punctajTotal;
        public ParticipantDTO(string nume, string prenume, long punctajTotal)
        {
            this.nume = nume;
            this.prenume = prenume;
            this.punctajTotal = punctajTotal;
        }

        public string Nume
        {
            get { return nume; }
            set { nume = value; }
        }

        public string Prenume
        {
            get { return prenume; }
            set { prenume = value; }
        }

        public long PunctajTotal
        {
            get { return punctajTotal; }
            set { punctajTotal = value; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            ParticipantDTO that = (ParticipantDTO)obj;
            return Nume == that.Nume && Prenume == that.Prenume && PunctajTotal == that.PunctajTotal;
        }


        public override string ToString()
        {
            return "ParticipantDTO{" +
                    "nume='" + nume + '\'' +
                    ", prenume='" + prenume + '\'' +
                    ", punctajTotal=" + punctajTotal +
                    '}';
        }

    }
}
