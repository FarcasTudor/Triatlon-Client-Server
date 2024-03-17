
using System;

namespace TriatlonModel
{
    [Serializable]
    public class Arbitru : Entity<long>
    {
        private string nume;
        private string prenume;
        private string parola;
        private string username;

        public Arbitru(string nume, string prenume, string parola, string username)
        {
            this.nume = nume;
            this.prenume = prenume;
            this.parola = parola;
            this.username = username;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
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

        public string Parola
        {
            get { return parola; }
            set { parola = value; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Arbitru arbitru = (Arbitru)obj;
            return nume == arbitru.nume && prenume == arbitru.prenume && parola == arbitru.parola;
        }
        

        public override string ToString()
        {
            return nume + " " + prenume;
        }

    }
}







