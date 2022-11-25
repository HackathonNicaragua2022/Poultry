using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.Granja
{
    public class IngresoLotes_Class : Tbl_IngresoLotes
    {
        private string nombrePollo;

        public string NombrePollo
        {
            get { return nombrePollo; }
            set { nombrePollo = value; }
        }
        private string ingresadoPor;

        public string Ingresado_Por
        {
            get { return ingresadoPor; }
            set { ingresadoPor = value; }
        }
        private string proveedor;

        public string Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }
        private string galera;

        public string Galera
        {
            get { return galera; }
            set { galera = value; }
        }
    }
}
