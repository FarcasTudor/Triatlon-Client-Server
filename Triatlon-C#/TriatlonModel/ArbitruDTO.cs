using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriatlonModel
{
    [Serializable]
    public class ArbitruDTO
    {
        public string User { get; set; }

        public string Pass { get; set; }

        public ArbitruDTO(string username, string parola)
        {
            User = username;
            Pass = parola;
        }
    }
}
