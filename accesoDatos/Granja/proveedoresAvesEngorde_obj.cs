using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Granja
{
    public class proveedoresAvesEngorde_obj
    {
        public string nuevo(tbl_ProveedoresAvesEngorde proveedor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.tbl_ProveedoresAvesEngorde.InsertOnSubmit(proveedor);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<tbl_ProveedoresAvesEngorde> getAllProveedores()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<tbl_ProveedoresAvesEngorde> result = from proveedores in dc.tbl_ProveedoresAvesEngorde orderby proveedores.Nombre_Proveedor select proveedores;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<tbl_ProveedoresAvesEngorde>();
            }
        }
        public string eliminarProveedor(int id_proveedor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                tbl_ProveedoresAvesEngorde provedor = (from _proveedor in dc.tbl_ProveedoresAvesEngorde where (_proveedor.ID_ProveedoresAves == id_proveedor) select _proveedor).FirstOrDefault();
                dc.tbl_ProveedoresAvesEngorde.DeleteOnSubmit(provedor);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string editarProveedor(int id_proveedor, string nombre, string procedencia, string telefono, string contacto, bool activo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                tbl_ProveedoresAvesEngorde provedor = (from _proveedor in dc.tbl_ProveedoresAvesEngorde where (_proveedor.ID_ProveedoresAves == id_proveedor) select _proveedor).FirstOrDefault();
                provedor.Nombre_Proveedor = nombre;
                provedor.Procedencia = procedencia;
                provedor.TelefonoContacto = telefono;
                provedor.Representante = contacto;
                provedor.Activo = activo;
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public tbl_ProveedoresAvesEngorde getProveedor(int id_proveedor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                tbl_ProveedoresAvesEngorde provedor = (from _proveedor in dc.tbl_ProveedoresAvesEngorde where (_proveedor.ID_ProveedoresAves == id_proveedor) select _proveedor).FirstOrDefault();
                return provedor;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
