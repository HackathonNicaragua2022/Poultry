using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos
{
    public class Catalogo_Clientes
    {
        /// <summary>
        /// ingresa un nuevo producto al catalogo del cliente seleccionado
        /// </summary>
        /// <param name="catalogoCliente"></param>
        /// <returns>1 si todo resulto con exito, de lo contrario devuelve el error producido</returns>
        public string nuevoProductoCatalogo(Tbl_CatalogoxCliente catalogoCliente)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Tbl_CatalogoxCliente.Any(a => a.ID_BodegaHuevos == catalogoCliente.ID_BodegaHuevos && a.ID_CLIENTES == catalogoCliente.ID_CLIENTES))
                {
                    return "El cliente ya tiene este producto en su catalogo";
                }
                else
                {
                    dc.Tbl_CatalogoxCliente.InsertOnSubmit(catalogoCliente);
                    dc.SubmitChanges();
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// obtiene todos los preductos del catalogo por cliente basado en su ID
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns>1 si todo sale con exito, de los contrario devuelve el error producido</returns>
        public List<CatalogoxCliente_obj> getAllxIDCliente(int idCliente)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<CatalogoxCliente_obj> result = from _catalogocliente in dc.Tbl_CatalogoxCliente
                                                          join _Bodega in dc.Tbl_BodegaHuevos on _catalogocliente.ID_BodegaHuevos equals _Bodega.ID_BodegaHuevos
                                                          join _tipoHUevo in dc.Cat_TipoHuevo on _Bodega.ID_TipoHUevo equals _tipoHUevo.ID_TipoHUevo
                                                          join _clasificacion in dc.Cat_ClasificacionHuevo on _Bodega.IDClasificacionHuevo equals _clasificacion.IDClasificacionHuevo
                                                          where (_catalogocliente.ID_CLIENTES == idCliente)
                                                          select new CatalogoxCliente_obj
                                                          {
                                                              Producto = _tipoHUevo.TipoHuevo + " " + _clasificacion.Clasificacion,
                                                              ID_CATALOGOXCLIENTE = _catalogocliente.ID_CATALOGOXCLIENTE,
                                                              ID_CLIENTES = _catalogocliente.ID_CLIENTES,
                                                              ID_BodegaHuevos = _catalogocliente.ID_BodegaHuevos,
                                                              CANTIDAD_CAJIAS_SUJERIDAS = _catalogocliente.CANTIDAD_CAJIAS_SUJERIDAS,
                                                              PRECIO_VENTA = _catalogocliente.PRECIO_VENTA
                                                          };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<CatalogoxCliente_obj>();
            }
        }

        /// <summary>
        /// Elimina un catalogo de la base de datos segun el cliente 
        /// </summary>
        /// <param name="IDBodega"></param>
        /// <param name="IDCliente"></param>
        /// <returns>1 si todo se realiza con exito, de lo contrario devuelve el error producido</returns>
        public string eliminarProductoDelCatalogo(int IDCatalogoxCliente)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_CatalogoxCliente catalogo = (from _catalogoCliente in dc.Tbl_CatalogoxCliente where (_catalogoCliente.ID_CATALOGOXCLIENTE == IDCatalogoxCliente) select _catalogoCliente).FirstOrDefault();
                dc.Tbl_CatalogoxCliente.DeleteOnSubmit(catalogo);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// edita los valores de Cajilla y precio en un prodcuto
        /// </summary>
        /// <param name="IDCatalgoxCliente"></param>
        /// <param name="CantidadCajillas"></param>
        /// <param name="precioVenta"></param>
        /// <returns> 1 Si la modificacion se realizo con exito, de los contrario el error producido</returns>
        public string editarProductoDeCatalogo(int IDCatalgoxCliente, decimal CantidadCajillas, decimal precioVenta)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_CatalogoxCliente catalogo = (from _catalogoCliente in dc.Tbl_CatalogoxCliente where (_catalogoCliente.ID_CATALOGOXCLIENTE == IDCatalgoxCliente) select _catalogoCliente).FirstOrDefault();
                catalogo.CANTIDAD_CAJIAS_SUJERIDAS = CantidadCajillas;
                catalogo.PRECIO_VENTA = precioVenta;
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
