using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriatlonNetworking.rpProtocols
{
    public enum RequestType
    {
        GET_ARBITRU,
        ADD_INSCRIERE,
        PROBE,
        GET_PARTICIPANTI_CU_PUNCTAJ,
        GET_PARTICIPANTI_DE_LA_PROBA,
        Get_Participanti_DTO_probaActuala,
        Get_Participanti_DTO,
        LOGIN,
        LOGOUT
    }
}
