using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.CajaChicaComedor
{
   public class tbl_ArqueoCajaChica_Comedor_obj:Tbl_ArqueoCajaChica_Comedor

    {
        private decimal totalDenominacion_local;//Para mostrar en tablas o controles se usara esta variable pues la de la tabla es una columna calculada y en tiempo de creacion local no se puede obtener el total

        public decimal TotalDenominacion_local
        {
            get { return totalDenominacion_local; }
            set { totalDenominacion_local = value; }
        }
    }
}
