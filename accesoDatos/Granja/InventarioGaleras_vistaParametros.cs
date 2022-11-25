using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.Granja
{
    public class InventarioGaleras_vistaParametros : Tbl_InventarioGalera
    {
        private double pesoPromedioLibras;

        public double PesoPromedioLibras
        {
            get { return pesoPromedioLibras; }
            set { pesoPromedioLibras = value; }
        }
        private bool seleccionado;

        public bool Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; }
        }
        private string nombreGalera;

        public string NombreGalera
        {
            get { return nombreGalera; }
            set { nombreGalera = value; }
        }
    }
}
