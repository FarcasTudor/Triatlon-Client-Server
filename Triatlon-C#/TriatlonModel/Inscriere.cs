using System;

namespace TriatlonModel
{
    [Serializable]
    public class Inscriere : Entity<long>
    {
        private long idParticipant;
        private long idProba;
        private long punctaj;
        
        public Inscriere(long idParticipant, long idProba, long punctaj)
        {
            this.idParticipant = idParticipant;
            this.idProba = idProba;
            this.punctaj = punctaj;
        }
        //setters and getters
        public long IdParticipant
        {
            get { return idParticipant; }
            set { idParticipant = value; }
        }
        public long IdProba
        {
            get { return idProba; }
            set { idProba = value; }
        }
        public long Punctaj
        {
            get { return punctaj; }
            set { punctaj = value; }
        }
        

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Inscriere inscriere = (Inscriere)obj;
            return idParticipant == inscriere.idParticipant && idProba == inscriere.idProba && punctaj == inscriere.punctaj;
        }

        public override string ToString()
        {
            return "Inscriere{" +
                   "idParticipant=" + idParticipant +
                   ", idProba=" + idProba +
                   ", punctaj=" + punctaj +
                   '}';
        }
    }
}

