using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ModuloLogistica
{
    public class Proveedor_obj
    {
        /// <summary>
        /// crea nuevo proveedor
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="direccion"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public string nuevoProveedor(string nombre, string direccion, bool activo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_ProveedorCombustible nuevo = new Cat_ProveedorCombustible();
                nuevo.NombreProveedor = nombre;
                nuevo.DireccionProveedor = direccion;
                nuevo.Activo = activo;
                dc.Cat_ProveedorCombustible.InsertOnSubmit(nuevo);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// obtiene todos los proveedores de la base de datos
        /// </summary>
        /// <returns></returns>
        public List<Cat_ProveedorCombustible> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from proveedores in dc.Cat_ProveedorCombustible select proveedores).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string update(int IDProveedor, string nombre, string Direccion, bool activo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_ProveedorCombustible proveedor_old = (from oldP in dc.Cat_ProveedorCombustible where (oldP.ID_proveedorCombustible == IDProveedor) select oldP).FirstOrDefault();
                proveedor_old.NombreProveedor = nombre;
                proveedor_old.DireccionProveedor = Direccion;
                proveedor_old.Activo = activo;
                dc.SubmitChanges();
                return "1";// <-- TRUE;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public Cat_ProveedorCombustible getbyID(int IDProveedor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_ProveedorCombustible proveedor = (from oldP in dc.Cat_ProveedorCombustible where (oldP.ID_proveedorCombustible == IDProveedor) select oldP).FirstOrDefault();
                return proveedor;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Elimina un proveedor usando su id personal
        /// </summary>
        /// <param name="IDProveedor"></param>
        /// <returns></returns>
        public string delete(int IDProveedor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_ProveedorCombustible proveedorDel = (from oldP in dc.Cat_ProveedorCombustible where (oldP.ID_proveedorCombustible == IDProveedor) select oldP).FirstOrDefault();
                dc.Cat_ProveedorCombustible.DeleteOnSubmit(proveedorDel);
                dc.SubmitChanges();
                return "1";// <-- TRUE;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
