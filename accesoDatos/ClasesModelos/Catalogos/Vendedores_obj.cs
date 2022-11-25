using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos.BodegaPrincipal
{
    public class Vendedores_obj
    {
        public List<Cat_Vendedores> getAll() {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_Vendedores> result = from _vendedores in dc.Cat_Vendedores select _vendedores;
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Cat_Vendedores>();
            }
        }
        public Cat_Vendedores getVendedorByID(int IDVendedor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from _Vendedor in dc.Cat_Vendedores where (_Vendedor.ID_VENDEDOR == IDVendedor) select _Vendedor).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string nuevoVendedor(Cat_Vendedores _nuevoVendedor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.Cat_Vendedores.Any(a => a.NOMBRE_VENDEDOR.Equals(_nuevoVendedor.NOMBRE_VENDEDOR)))
                {
                    dc.Cat_Vendedores.InsertOnSubmit(_nuevoVendedor);
                    dc.SubmitChanges();
                    return "1";
                }
                else
                {
                    throw new Exception("Ya hay un Vendedor con ese nombre!!");
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    
        public string actualizar(Cat_Vendedores VendedorActualizar)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_Vendedores VendedorOriginal = (from _Vendedors in dc.Cat_Vendedores where (_Vendedors.ID_VENDEDOR == VendedorActualizar.ID_VENDEDOR) select _Vendedors).FirstOrDefault();
                VendedorOriginal.NOMBRE_VENDEDOR = VendedorActualizar.NOMBRE_VENDEDOR;
                VendedorOriginal.CEDULA = VendedorActualizar.CEDULA;
                VendedorOriginal.TELEFONO = VendedorActualizar.TELEFONO;
                VendedorOriginal.CODIGO = VendedorActualizar.CODIGO;
                VendedorOriginal.ACTIVO = VendedorActualizar.ACTIVO ;                
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
      
        public string eliminar(int IDVendedor)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_Vendedores eliminarVendedor = (from _Vendedor in dc.Cat_Vendedores where (_Vendedor.ID_VENDEDOR == IDVendedor) select _Vendedor).FirstOrDefault();
                dc.Cat_Vendedores.DeleteOnSubmit(eliminarVendedor);
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
