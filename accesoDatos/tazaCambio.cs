using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ultil;

namespace accesoDatos
{
    public class tazaCambio
    {
        public bool generarTazaMensual()
        {
            new TazaCambio().getTaza();
            return true;
        }
    }
}
