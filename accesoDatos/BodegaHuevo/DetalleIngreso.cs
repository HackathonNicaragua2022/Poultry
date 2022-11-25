using accesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlHuevos.ClasesModelos.BodegaPrincipal
{
    public class DetalleIngreso:Tbl_DetalleCajias
    {
        private string producto;

        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
    }
}
