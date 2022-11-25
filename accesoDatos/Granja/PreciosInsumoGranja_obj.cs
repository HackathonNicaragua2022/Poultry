using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ultil;

namespace accesoDatos.Granja
{
    public class PreciosInsumoGranja_obj
    {
        /// <summary>
        /// Actualiza la tabla de precios del insumo que coincida con el ID_InsumosGranjaEngorde
        /// si ya existe solo se actualiza de los contrario se crea, se utiliza la taza de cambios del
        /// banco central para actualizar los precios en dolares
        /// </summary>
        /// <param name="ID_InsumosGranjaEngorde"></param>
        /// <param name="precioCordobas"></param>
        /// <returns></returns>
        public bool actualizarPrecio(int ID_InsumosGranjaEngorde, decimal precioCordobas)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                //si hay un objeto que coincida con el id suministrado solo se actualiza
                if (dc.Tbl_PreciosInsumosGranja.Any(a => a.ID_InsumosGranjaEngorde == ID_InsumosGranjaEngorde))
                {
                    Tbl_PreciosInsumosGranja preciosInsumoGranja = (from _preciosInsumoGranja in dc.Tbl_PreciosInsumosGranja where (_preciosInsumoGranja.ID_InsumosGranjaEngorde == ID_InsumosGranjaEngorde) select _preciosInsumoGranja).FirstOrDefault();
                    //actualizar los ultimos precios 
                    preciosInsumoGranja.ultimoPrecioCordobas = preciosInsumoGranja.precioCordobas;
                    preciosInsumoGranja.ultimoPrecioDolares = preciosInsumoGranja.precioCordobas;
                    preciosInsumoGranja.ultimaTazaCambio = preciosInsumoGranja.tazaCambio;
                    //actualizar con el nuevo precio
                    preciosInsumoGranja.precioCordobas = precioCordobas;
                    preciosInsumoGranja.precioDolares = precioCordobas * ((decimal)new TazaCambio().getTaza());
                    preciosInsumoGranja.tazaCambio = (decimal)new TazaCambio().getTaza();
                    dc.SubmitChanges();
                }
                else
                {
                    //crear un nuevo registro del precio de insumo de la granja
                    Tbl_PreciosInsumosGranja nuevoPrecioInsumoGranja = new Tbl_PreciosInsumosGranja();
                    nuevoPrecioInsumoGranja.ID_InsumosGranjaEngorde = ID_InsumosGranjaEngorde;
                    nuevoPrecioInsumoGranja.precioCordobas = precioCordobas;
                    nuevoPrecioInsumoGranja.precioDolares = precioCordobas * ((decimal)new TazaCambio().getTaza());
                    nuevoPrecioInsumoGranja.tazaCambio = (decimal)new TazaCambio().getTaza();
                    nuevoPrecioInsumoGranja.ultimoPrecioCordobas = nuevoPrecioInsumoGranja.precioCordobas;
                    nuevoPrecioInsumoGranja.ultimoPrecioDolares = nuevoPrecioInsumoGranja.precioDolares;
                    nuevoPrecioInsumoGranja.ultimaTazaCambio = nuevoPrecioInsumoGranja.tazaCambio;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// obtiene de la base de datos el precio en cordobas del insumo que coincida con el id suministrado
        /// </summary>
        /// <param name="ID_InsumosGranjaEngorde"></param>
        /// <returns>decimal</returns>
        public decimal getPrecioCordobas_InsumoGranja(int ID_InsumosGranjaEngorde)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_PreciosInsumosGranja preciosInsumoGranja = (from _preciosInsumoGranja in dc.Tbl_PreciosInsumosGranja where (_preciosInsumoGranja.ID_InsumosGranjaEngorde == ID_InsumosGranjaEngorde) select _preciosInsumoGranja).FirstOrDefault();
                return preciosInsumoGranja.precioCordobas;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// obtiene de la base de datos el valor del precio en dolares del insumo de granja que coincida
        /// con el ID suministrado
        /// </summary>
        /// <param name="ID_InsumosGranjaEngorde"></param>
        /// <returns>decimal</returns>
        public decimal getPrecioDolares_InsumoGranja(int ID_InsumosGranjaEngorde)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_PreciosInsumosGranja preciosInsumoGranja = (from _preciosInsumoGranja in dc.Tbl_PreciosInsumosGranja where (_preciosInsumoGranja.ID_InsumosGranjaEngorde == ID_InsumosGranjaEngorde) select _preciosInsumoGranja).FirstOrDefault();
                return preciosInsumoGranja.precioDolares;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// obtiene todos los insumos relacionados con sus tablas dentro de una vista
        /// </summary>
        /// <returns></returns>
        public List<PreciosInsumosGranja_vista> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<PreciosInsumosGranja_vista> result = from _preciosInsumoGranja in dc.Tbl_PreciosInsumosGranja
                                                                select new PreciosInsumosGranja_vista
                                                                {
                                                                    IDPreciosInsumosGranja = _preciosInsumoGranja.IDPreciosInsumosGranja,
                                                                    ID_InsumosGranjaEngorde = _preciosInsumoGranja.ID_InsumosGranjaEngorde,
                                                                    precioDolares = _preciosInsumoGranja.precioDolares,
                                                                    precioCordobas = _preciosInsumoGranja.precioCordobas,
                                                                    tazaCambio = _preciosInsumoGranja.tazaCambio,
                                                                    ultimaTazaCambio = _preciosInsumoGranja.ultimaTazaCambio,
                                                                    ultimoPrecioCordobas = _preciosInsumoGranja.ultimoPrecioCordobas,
                                                                    ultimoPrecioDolares = _preciosInsumoGranja.ultimoPrecioDolares,
                                                                    NombreCategoria = _preciosInsumoGranja.Tbl_InsumosGranjaEngorde.Cat_CategoriaInsumosGranjaEngorde.CategoriaInsumo,
                                                                    NombreMedida = _preciosInsumoGranja.Tbl_InsumosGranjaEngorde.Cat_UnidadMedidaInsumoGranja.UnidadMedidaInsumo,
                                                                    NombreInsumo = _preciosInsumoGranja.Tbl_InsumosGranjaEngorde.NombreInsumo
                                                                };
                return result.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
