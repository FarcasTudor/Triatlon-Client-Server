using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriatlonModel
{
    [Serializable]
    public class Proba : Entity<long>
    {
        private string numeProba;
        private long idArbitru;


        public Proba(string numeProba, long idArbitru)
        {
            this.numeProba = numeProba;
            this.idArbitru = idArbitru;
        }

        public string NumeProba
        {
            get { return numeProba; }
            set { numeProba = value; }
        }

        public long IdArbitru
        {
            get { return idArbitru; }
            set { idArbitru = value; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Proba proba = (Proba)obj;
            return numeProba == proba.numeProba && idArbitru == proba.idArbitru;
        }

        public override string ToString()
        {
            return numeProba;
        }
    }
}
