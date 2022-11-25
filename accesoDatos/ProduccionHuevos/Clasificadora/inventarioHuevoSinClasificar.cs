using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos.Clasificadora
{
    public class inventarioHuevoSinClasificar
    {
        /// <summary>
        /// Obtiene una lista de todos los tipos de huevos en la bodega de huevos sin clasificar
        /// </summary>
        /// <returns></returns>
        public List<tipoHuevoEnInventario> getAllTipoHuevoEnBodehaHSC()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<tipoHuevoEnInventario> resultado = from bodegaHuevoSinclasificar in dc.Tbl_Inventario_HuevoSinClasificar
                                                              join tipoHuevo in dc.Cat_TipoHuevo on bodegaHuevoSinclasificar.ID_TipoHUevo equals tipoHuevo.ID_TipoHUevo
                                                              select new tipoHuevoEnInventario
                                                              {
                                                                  //--- Correjir, solo se debe mostrar el tipo de huevo como string pero el id debe ser el de la tabla de inventario huevo sin clasificar y no el de la tabla de Cat_TipoHuevo
                                                                  ID_TipoHuevo = bodegaHuevoSinclasificar.ID_HuevoSinClasificar,
                                                                  TipoHuevo = tipoHuevo.TipoHuevo
                                                              };


                return resultado.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// obtiene todos los tipos de huevos en la bodeja de tipos de huevos que contiene saldos positivos
        /// </summary>
        /// <returns></returns>
        public List<tipoHuevoEnInventario> getAllTipoHuevoEnBodehaHSCconExistecia()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<tipoHuevoEnInventario> resultado = from bodegaHuevoSinclasificar in dc.Tbl_Inventario_HuevoSinClasificar
                                                              join tipoHuevo in dc.Cat_TipoHuevo on bodegaHuevoSinclasificar.ID_TipoHUevo equals tipoHuevo.ID_TipoHUevo
                                                              where bodegaHuevoSinclasificar.SaldoHuevoSinClasificar > 0
                                                              orderby tipoHuevo.TipoHuevo
                                                              select new tipoHuevoEnInventario
                                                              {
                                                                  //--- Correjir, solo se debe mostrar el tipo de huevo como string pero el id debe ser el de la tabla de inventario huevo sin clasificar y no el de la tabla de Cat_TipoHuevo
                                                                  ID_TipoHuevo = bodegaHuevoSinclasificar.ID_HuevoSinClasificar,
                                                                  TipoHuevo = tipoHuevo.TipoHuevo
                                                              };
                return resultado.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// obtiene todos los tipos de huevos en bodega sin clasificar, consolidados por grupo
        /// 
        /// </summary>
        /// <returns></returns>
        public List<USP_Consolidado_InventarioHSCResult> getHuevoEnBodehaHSC()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();                
                return dc.USP_Consolidado_InventarioHSC().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        public List<USP_TOTALCAJILLAS_CLAS_DIAResult> TotalCajillasClasificadas()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_TOTALCAJILLAS_CLAS_DIA().ToList();
            }
            catch (Exception)
            {
                return new List<USP_TOTALCAJILLAS_CLAS_DIAResult>();
            }
        }

        public List<ingresoInventario> getAllIngresosEntreFechas(DateTime fechaInicial, DateTime FechaFinal)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<ingresoInventario> result = from ingresoInventariosHSC in dc.Tbl_IngresoInventario_HSC
                                                       join detalleIngresoHSC in dc.Tbl_DetalleIngreso_HSC on ingresoInventariosHSC.ID_IngresoInventario equals detalleIngresoHSC.ID_IngresoInventario
                                                       join usuarios in dc.Cat_Usuarios on ingresoInventariosHSC.IngresadoPor equals usuarios.IDUsuarios
                                                       where (ingresoInventariosHSC.FechaIngresoSistema.Date >= fechaInicial.Date && ingresoInventariosHSC.FechaIngresoSistema.Date <= FechaFinal)
                                                       select new ingresoInventario
                                                       {
                                                           ID_IngresoInventario = ingresoInventariosHSC.ID_IngresoInventario,

                                                       };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<ingresoInventario>();
            }
        }


        /// <summary>
        /// Obtiene el Saldo en inventario de huevo sin clasificar por medio del ID        /// </summary>
        /// <param name="IDTipoHuevo"></param>
        /// <returns></returns>
        public List<USP_SaldoHuevosSinClasificarxIDResult> getSaldoInventarioHuevoSinClasificarxID(int IDTipoHuevo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_SaldoHuevosSinClasificarxID(IDTipoHuevo).ToList();
            }
            catch (Exception)
            {
                return new List<USP_SaldoHuevosSinClasificarxIDResult>();
            }
        }
        public List<SaldoHuevosSinClasificarParaClasificadora> getSaldoInventarioHuevoSinClasificarxIDparaClasificadora(int IDTipoHuevo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                List<USP_SaldoHuevosSinClasificarxIDResult> resul = dc.USP_SaldoHuevosSinClasificarxID(IDTipoHuevo).ToList();

                List<SaldoHuevosSinClasificarParaClasificadora> nuevaLista = new List<SaldoHuevosSinClasificarParaClasificadora>();
                foreach (USP_SaldoHuevosSinClasificarxIDResult saldoHuevo in resul)
                {
                    SaldoHuevosSinClasificarParaClasificadora tem = new SaldoHuevosSinClasificarParaClasificadora();
                    tem.ID_HuevoSinClasificar = saldoHuevo.ID_HuevoSinClasificar;
                    tem.TipoHuevo = saldoHuevo.TipoHuevo;
                    tem.TOTALE = saldoHuevo.TOTALE;
                    tem.TS = saldoHuevo.TS;
                    tem.SALDO = saldoHuevo.SALDO;
                    tem.FechaProduccion = saldoHuevo.FechaProduccion;
                    tem.PROMEDIO_DIAS = saldoHuevo.PROMEDIO_DIAS;
                    tem.Seleccionado = false;
                    nuevaLista.Add(tem);
                }
                return nuevaLista;
            }
            catch (Exception)
            {
                return new List<SaldoHuevosSinClasificarParaClasificadora>();
            }
        }
        /// <summary>
        /// Esta clase hereda del procedimiento almacenado USP_SaldoHuevosSinClasificarxID, solo se usa para agregar una variable
        /// que sirba para identificar que elemento se selecciono
        /// </summary>
        public class SaldoHuevosSinClasificarParaClasificadora : USP_SaldoHuevosSinClasificarxIDResult
        {
            private bool seleccionado;

            public bool Seleccionado
            {
                get { return seleccionado; }
                set { seleccionado = value; }
            }
        }
        public class ingresoInventario : Tbl_IngresoInventario_HSC
        {
            private string ingresado_Por;

            public string Ingresado_Por
            {
                get { return ingresado_Por; }
                set { ingresado_Por = value; }
            }
        }

        public class HuevoEnInventarioSinClasificar : Tbl_Inventario_HuevoSinClasificar
        {
            private string tipohuevo;

            public string Tipohuevo
            {
                get { return tipohuevo; }
                set { tipohuevo = value; }
            }
        }
        public class tipoHuevoEnInventario
        {
            private int iD_TipoHuevo;

            public int ID_TipoHuevo
            {
                get { return iD_TipoHuevo; }
                set { iD_TipoHuevo = value; }
            }
            private string tipoHuevo;

            public string TipoHuevo
            {
                get { return tipoHuevo; }
                set { tipoHuevo = value; }
            }
        }
    }
}
