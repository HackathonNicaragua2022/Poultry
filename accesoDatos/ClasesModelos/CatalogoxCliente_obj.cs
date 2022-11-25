using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos
{
    public class CatalogoxCliente_obj:Tbl_CatalogoxCliente
    {
        private string producto;

        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
    }
}
