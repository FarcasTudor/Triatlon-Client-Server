using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriatlonNetworking.rpProtocols
{
    public enum ResponseType
    {
        OK,
        ERROR, 
        TAKE_PROBE, 
        UPDATE_PUNCTAJ, 
        TAKE_PARTICIPANTI_CU_PUNCTAJ, 
        TAKE_PARTICIPANTI_DE_LA_PROBA
    }
}
