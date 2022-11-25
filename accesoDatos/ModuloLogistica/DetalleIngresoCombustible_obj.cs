using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class DetalleIngresoCombustible_obj
    {

        public class DetalleIngresoCombustible_Class : Tbl_DetalleIngreso
        {
            private string nombreTipoCombustible;

            public string NombreTipoCombustible
            {
                get { return nombreTipoCombustible; }
                set { nombreTipoCombustible = value; }
            }
        }
    }
}
