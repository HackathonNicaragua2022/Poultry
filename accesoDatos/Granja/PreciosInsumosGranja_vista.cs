using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.Granja
{
    public class PreciosInsumosGranja_vista : Tbl_PreciosInsumosGranja
    {
        private string nombreCategoria;

        public string NombreCategoria
        {
            get { return nombreCategoria; }
            set { nombreCategoria = value; }
        }
        private string nombreMedida;

        public string NombreMedida
        {
            get { return nombreMedida; }
            set { nombreMedida = value; }
        }
        private string nombreInsumo;

        public string NombreInsumo
        {
            get { return nombreInsumo; }
            set { nombreInsumo = value; }
        }
    }
}
