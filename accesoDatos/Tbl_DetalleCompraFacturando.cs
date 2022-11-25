using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos
{
    public class Tbl_DetalleCompraFacturando: Tbl_DetalleFacturaCompra
    {
        private string nombreProducto;

        public string NombreProducto
        {
            get { return nombreProducto; }
            set { nombreProducto = value; }
        }

        private bool habilatoVenta;

        public bool HabilatoVenta
        {
            get { return habilatoVenta; }
            set { habilatoVenta = value; }
        }
    }
}
