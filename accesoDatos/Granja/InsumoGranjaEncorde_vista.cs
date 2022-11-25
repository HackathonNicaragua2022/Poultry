using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.Granja
{
    public class InsumoGranjaEncorde_vista : Tbl_InsumosGranjaEngorde
    {
        string nombreCategoria;

        public string NombreCategoria
        {
            get { return nombreCategoria; }
            set { nombreCategoria = value; }
        }
        string nombreMedida;

        public string NombreMedida
        {
            get { return nombreMedida; }
            set { nombreMedida = value; }
        }
    }
}
