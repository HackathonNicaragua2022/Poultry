using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class AplicacionesMedicinas_vista:Tbl_AplicacionesMedicinas
    {
        private string nombreInsumo;

        public string NombreInsumo
        {
            get { return nombreInsumo; }
            set { nombreInsumo = value; }
        }
        private string registrado_por;

        public string Registrado_por
        {
            get { return registrado_por; }
            set { registrado_por = value; }
        }
    }
}
