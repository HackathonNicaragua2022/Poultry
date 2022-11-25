using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.Granja
{
    public class RegistroInsumoGranja_vista : Tbl_RegistroInsumosGranjaLote
    {
        private string registrado_Por;

        public string Registrado_Por
        {
            get { return registrado_Por; }
            set { registrado_Por = value; }
        }
        private string nombreInsumo;

        public string NombreInsumo
        {
            get { return nombreInsumo; }
            set { nombreInsumo = value; }
        }
    }
}
