using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriatlonModel
{
    [Serializable]
    public class Participant : Entity<long>
    {
        private string nume;
        private string prenume;

        public Participant(string nume, string prenume)
        {
            this.nume = nume;
            this.prenume = prenume;

        }

        //setter and getters
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



        public override string ToString()
        {
            return "Participant{" +
                    "nume='" + nume + '\'' +
                    ", prenume='" + prenume + '\'' +
                    '}';
        }
        
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Participant participant = (Participant)obj;
            return nume == participant.nume && prenume == participant.prenume;
        }
    }
}
