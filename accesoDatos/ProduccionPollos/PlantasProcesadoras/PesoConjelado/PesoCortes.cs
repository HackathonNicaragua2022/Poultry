using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos.ProduccionPollos.PlantasProcesadoras.PesoConjelado
{
    public class PesoCortes : Tbl_PesoCortes_detalle
    {
        private string nombre_Rubro;

        public string Nombre_Rubro
        {
            get { return nombre_Rubro; }
            set { nombre_Rubro = value; }
        }
        private string estado_Rubro;

        public string Estado_Rubro
        {
            get { return estado_Rubro; }
            set { estado_Rubro = value; }
        }
    }
}
