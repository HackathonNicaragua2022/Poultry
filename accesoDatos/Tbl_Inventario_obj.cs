using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos
{
    public class Tbl_Inventario_obj
    {
        public List<Tbl_Inventario> getAllInventario()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from Inventario in dc.Tbl_Inventario select Inventario).ToList();
            }
            catch (Exception)
            {
                return new List<Tbl_Inventario>();
            }
        }
        public List<UPS_InventarioResult> getAllInventario(bool activo, int IDCategoria)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();

                return dc.UPS_Inventario(activo, IDCategoria).ToList();
            }
            catch (Exception)
            {
                return new List<UPS_InventarioResult>();
            }
        }
        public List<UPS_InventarioConExistenciaResult> getAllInventarioConExistencia(bool activo, int IDCategoria)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();

                return dc.UPS_InventarioConExistencia(activo, IDCategoria).ToList();
            }
            catch (Exception)
            {
                return new List<UPS_InventarioConExistenciaResult>();
            }
        }

        public string guardarInventario(Tbl_Inventario nuevoInvent)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Tbl_Inventario.InsertOnSubmit(nuevoInvent);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al guardar, no se ha podido guardar el nuevo Inventario: " + ex.Message;
            }
        }
        public string EliminarInventario(int IdInventario)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Inventario Inventario_Elim = (from p in dc.Tbl_Inventario where (p.ID_Inventario == IdInventario) select p).First();
                dc.Tbl_Inventario.DeleteOnSubmit(Inventario_Elim);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el Inventario " + ex.Message;
            }
        }
        public Tbl_Inventario findById(int ID_Producto)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from p in dc.Tbl_Inventario where (p.ID_Producto == ID_Producto) select p).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Tbl_Inventario findByIdInventario(int ID_Inventario)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from p in dc.Tbl_Inventario where (p.ID_Inventario == ID_Inventario) select p).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int getExistenciaByIdInventario(int ID_Inventario)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (int)(from p in dc.Tbl_Inventario where (p.ID_Inventario == ID_Inventario) select p).FirstOrDefault().ExistenciaActual;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public string ActualizarProducto_Perdido(int ID_Inventario, DateTime FechaIngreso, int CantidadActualizar, Guid Usario)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                /////////////////////////////////////////////               
                /*
                 Primero actualizar los productos Perdidos del inventario
                 * 
                 */
                Tbl_Inventario inventario = (from _inv in dc.Tbl_Inventario where (_inv.ID_Inventario == ID_Inventario) select _inv).FirstOrDefault();
                if (inventario != null)//Verificar que el resultado no es NULL, que en todo caso seria que no se encontro el producto
                {
                    inventario.CantidadPerdida += CantidadActualizar;// Se actualizal el valor original mas la nueva cantidad                   
                    //Un Nuevo Registro que indica los detalles de los productos Perdidos
                    Tbl_InventarioPerdido inventarioPer = new Tbl_InventarioPerdido();
                    inventarioPer.ID_Inventario = ID_Inventario;
                    inventarioPer.FechaRegistro = FechaIngreso;
                    inventarioPer.CantidadPerdida = CantidadActualizar;
                    inventarioPer.RegistradoPor = Usario;
                    dc.Tbl_InventarioPerdido.InsertOnSubmit(inventarioPer);
                }
                else
                {
                    throw new Exception("No se encontro el inventario que requiere actualizar");
                }
                dc.SubmitChanges();
                dc.Transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                dc.Transaction.Rollback();
                return ex.Message;
            }
            finally
            {
                dc.Connection.Close();
            }
        }

        public string ActualizarProducto_Danger(int ID_Inventario, DateTime FechaIngreso, int CantidadActualizar, Guid Usario)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                /////////////////////////////////////////////               
                /*
                 Primero actualizar los productos Dañados del inventario
                 * 
                 */
                Tbl_Inventario inventario = (from _inv in dc.Tbl_Inventario where (_inv.ID_Inventario == ID_Inventario) select _inv).FirstOrDefault();
                if (inventario != null)//Verificar que el resultado no es NULL, que en todo caso seria que no se encontro el producto
                {
                    inventario.CantidadDanada += CantidadActualizar;// Se actualizal el valor original mas la nueva cantidad                   
                    //Un Nuevo Registro que indica los detalles de los productos Dañados
                    Tbl_InventarioDanado inventariodanados = new Tbl_InventarioDanado();
                    inventariodanados.ID_Inventario = ID_Inventario;
                    inventariodanados.FechaRegistro = FechaIngreso;
                    inventariodanados.CantidadDanada = CantidadActualizar;
                    inventariodanados.RegistradoPor = Usario;
                    dc.Tbl_InventarioDanado.InsertOnSubmit(inventariodanados);
                }
                else
                {
                    throw new Exception("No se encontro el inventario que requiere actualizar");
                }
                dc.SubmitChanges();
                dc.Transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                dc.Transaction.Rollback();
                return ex.Message;
            }
            finally
            {
                dc.Connection.Close();
            }
        }

        public string ActualizarProducto_Vencidos(int ID_Inventario, DateTime FechaIngreso, int CantidadActualizar, Guid Usario)
        {
            ayosabdDataContext dc = new ayosabdDataContext();
            try
            {
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                /////////////////////////////////////////////               
                /*
                 Primero actualizar los productos Dañados del inventario
                 * 
                 */
                Tbl_Inventario inventario = (from _inv in dc.Tbl_Inventario where (_inv.ID_Inventario == ID_Inventario) select _inv).FirstOrDefault();
                if (inventario != null)//Verificar que el resultado no es NULL, que en todo caso seria que no se encontro el producto
                {
                    inventario.CantidadVencida += CantidadActualizar;// Se actualizal el valor original mas la nueva cantidad                   
                    //Un Nuevo Registro que indica los detalles de los productos Dañados
                    Tbl_InventarioVencido inventarioVencidos = new Tbl_InventarioVencido();
                    inventarioVencidos.ID_Inventario = ID_Inventario;
                    inventarioVencidos.FechaRegistro = FechaIngreso;
                    inventarioVencidos.CantidadVencido = CantidadActualizar;
                    inventarioVencidos.RegistradoPor = Usario;
                    dc.Tbl_InventarioVencido.InsertOnSubmit(inventarioVencidos);
                }
                else
                {
                    throw new Exception("No se encontro el inventario que requiere actualizar");
                }
                dc.SubmitChanges();
                dc.Transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                dc.Transaction.Rollback();
                return ex.Message;
            }
            finally
            {
                dc.Connection.Close();
            }
        }

        public List<USP_ProductosEnInventarioResult> ProductosEnInventario(int IdCategoria)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_ProductosEnInventario(IdCategoria).ToList();
            }
            catch (Exception)
            {
                return new List<USP_ProductosEnInventarioResult>();
            }
        }
        public Tbl_Producto getProdcutoByInventarioID(int IDInventario)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Producto pro = (from p in dc.Tbl_Producto join inv in dc.Tbl_Inventario on p.ID_Producto equals inv.ID_Producto where (inv.ID_Inventario == IDInventario) select p).FirstOrDefault();
                return pro;
            }
            catch (Exception)
            {
                return new Tbl_Producto();
            }
        }

        public List<USP_LISTAPRODUCTOS_INVENTARIOResult> getProductosEnInventario(bool activos, bool conExistencia)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return dc.USP_LISTAPRODUCTOS_INVENTARIO(activos, conExistencia).ToList();
            }
            catch (Exception)
            {
                return new List<USP_LISTAPRODUCTOS_INVENTARIOResult>();
            }
        }
    }
}
