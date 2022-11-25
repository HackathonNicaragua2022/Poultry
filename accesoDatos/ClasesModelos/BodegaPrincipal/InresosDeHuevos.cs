using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.ClasesModelos.BodegaPrincipal
{
    public class InresosDeHuevos:Tbl_IngresoHuevos
    {
        string ingresadoPor;
        string nombreTipoIngreso;

        public string NombreTipoIngreso
        {
            get { return nombreTipoIngreso; }
            set { nombreTipoIngreso = value; }
        }
        public string IngresadoPor
        {
            get { return ingresadoPor; }
            set { ingresadoPor = value; }
        }
    }
}
