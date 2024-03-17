using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriatlonModel
{
    [Serializable]
    public class Entity<ID>
    {
        private static readonly long serialVersionUID = 7331115341259248461L;

        /**
         * ID-ul entitatii
         */
        private ID id;


        /**
         * Metoda ce returneaza ID-ul entitatii
         *
         * @return ID
         */
        public ID getId()
        {
            return id;
        }

        /**
         * Metoda ce seteaza ID-ul id la entitate
         *
         * @param id - id of entity
         */
        public void setId(ID id)
        {
            this.id = id;
        }
    }
}
