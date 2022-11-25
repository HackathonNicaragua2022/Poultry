using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Catalogos
{
    public class Tbl_Proveedores_obj
    {
        public List<Tbl_Proveedores> getAllProveedores()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from Proveedores in dc.Tbl_Proveedores select Proveedores).ToList(); ;
            }
            catch (Exception)
            {
                return new List<Tbl_Proveedores>();
            }
        }
        public string guardarProveedores(Tbl_Proveedores nuevoProveedores)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Tbl_Proveedores.InsertOnSubmit(nuevoProveedores);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al guardar, no se ha podido guardar el Proveedor: " + ex.Message;
            }
        }
        public string EliminarProveedores(int IdProveedores)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Proveedores ProveedoresElim = (from p in dc.Tbl_Proveedores where (p.ID_Proveedores == IdProveedores) select p).First();
                dc.Tbl_Proveedores.DeleteOnSubmit(ProveedoresElim);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el Proveedor " + ex.Message;
            }
        }
        public Tbl_Proveedores findById(int idProveedores)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from p in dc.Tbl_Proveedores where (p.ID_Proveedores == idProveedores) select p).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool findByName(string nombreProv)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                //(from p in dc.Tbl_Proveedores where (p.NombreProveedores == idNombre) select p).First();
                return dc.Tbl_Proveedores.Any(a => a.NombreEmpresa.Equals(nombreProv));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string updateProveedores(Tbl_Proveedores ProveedoresMod, int idProveedores)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Tbl_Proveedores proveedorUp = (from p in dc.Tbl_Proveedores where (p.ID_Proveedores == idProveedores) select p).First();
                proveedorUp.NombreEmpresa = ProveedoresMod.NombreEmpresa;
                proveedorUp.NombreContacto = ProveedoresMod.NombreContacto;
                proveedorUp.NumeroTelf = ProveedoresMod.NumeroTelf;
                proveedorUp.Direccion = ProveedoresMod.Direccion;
                proveedorUp.Email = ProveedoresMod.Email;
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al actualizar " + ex.Message;
            }
        }
    }
}
