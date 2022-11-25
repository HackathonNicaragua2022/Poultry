using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.Granja
{
    public class ParametrosVistaRel : Tbl_ParametrosDiarios
    {
        private double pesoPromedioLibras;

        public double PesoPromedioLibras
        {
            get { return pesoPromedioLibras; }
            set { pesoPromedioLibras = value; }
        }
        private string nombreRegistrado;

        public string NombreRegistrado
        {
            get { return nombreRegistrado; }
            set { nombreRegistrado = value; }
        }
    }
}
