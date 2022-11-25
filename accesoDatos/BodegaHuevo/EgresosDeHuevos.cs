using accesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlHuevos.ClasesModelos.BodegaPrincipal
{
    public class EgresosDeHuevos:Tbl_SalidaBodega
    {
        string ingresadoPor;
        string nombreRuta;

        public string NombreRuta
        {
            get { return nombreRuta; }
            set { nombreRuta = value; }
        }
        string nombreVendedor;

        public string NombreVendedor
        {
            get { return nombreVendedor; }
            set { nombreVendedor = value; }
        }
        string nombreCliente;

        public string NombreCliente
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }
        public string IngresadoPor
        {
            get { return ingresadoPor; }
            set { ingresadoPor = value; }
        }
    }
}
