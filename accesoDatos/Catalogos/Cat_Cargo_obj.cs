using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Catalogos
{
    public class Cat_Cargo_obj
    {
        public List<Cat_Cargo> getAllCargos()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from cargo in dc.Cat_Cargo select cargo).ToList();
            }
            catch (Exception)
            {

                return new List<Cat_Cargo>();
            }
        }
        public string guardarCargo(Cat_Cargo nuevaCargo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Cat_Cargo.InsertOnSubmit(nuevaCargo);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al guardar, no se ha podido guardar el Cargo: " + ex.Message;
            }
        }
        public string EliminarCargo(int IdCargo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_Cargo CargoElim = (from p in dc.Cat_Cargo where (p.ID_Cargo == IdCargo) select p).First();
                dc.Cat_Cargo.DeleteOnSubmit(CargoElim);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el Cargo " + ex.Message;
            }
        }
        public Cat_Cargo findById(int idCargo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from p in dc.Cat_Cargo where (p.ID_Cargo == idCargo) select p).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool findByName(string nombreC)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                //(from p in dc.Cat_Cargo where (p.NombreCargo == idNombre) select p).First();
                return dc.Cat_Cargo.Any(a => a.NombreCargo.Equals(nombreC));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string updateCargo(Cat_Cargo CargoMod, int idCargo)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_Cargo areUp = (from p in dc.Cat_Cargo where (p.ID_Cargo == idCargo) select p).First();
                areUp.NombreCargo = CargoMod.NombreCargo;
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
