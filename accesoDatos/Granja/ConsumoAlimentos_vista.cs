using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.Granja
{
    public class ConsumoAlimentos_vista : Tbl_ControlAlimenticio
    {
        private string nombreTipoAlimentos;
        private string registradoPorUsuario;

        public string RegistradoPorUsuario
        {
            get { return registradoPorUsuario; }
            set { registradoPorUsuario = value; }
        }
        public string NombreTipoAlimentos
        {
            get { return nombreTipoAlimentos; }
            set { nombreTipoAlimentos = value; }
        }
    }
}
