using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriatlonModel
{
    [Serializable]
    public class ParticipantDTO : Entity<long>
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
