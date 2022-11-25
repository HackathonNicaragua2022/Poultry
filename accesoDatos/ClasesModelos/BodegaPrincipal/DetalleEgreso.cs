using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos.BodegaPrincipal
{
    public class DetalleEgreso : Tbl_DetalleSalida
    {
        string producto;

        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
    }
}
