using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class DetalleDesapachoCombustible_obj
    {
        

        public class DetalleDespacho : Tbl_DetalleDespacho
        {
            private string tipoCombustibleInventario;

            public string TipoCombustibleInventario
            {
                get { return tipoCombustibleInventario; }
                set { tipoCombustibleInventario = value; }
            }
        }
    }
}
