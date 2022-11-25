using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.Granja
{
    public class AplicacionesTratamientos_vista : Tbl_AplicacionesTratamientos
    {
        private string nombreRegistradoPor;

        public string NombreRegistradoPor
        {
            get { return nombreRegistradoPor; }
            set { nombreRegistradoPor = value; }
        }
        private string nombreInsumo;

        public string NombreInsumo
        {
            get { return nombreInsumo; }
            set { nombreInsumo = value; }
        }
    }
}
