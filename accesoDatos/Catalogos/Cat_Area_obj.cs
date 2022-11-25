using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Catalogos
{
    public class Cat_Area_obj
    {
        public List<Cat_Area> getAllArea()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from area in dc.Cat_Area select area).ToList(); ;
            }
            catch (Exception)
            {
                return new List<Cat_Area>();
            }
        }
        public string guardarArea(Cat_Area nuevaArea)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                dc.Cat_Area.InsertOnSubmit(nuevaArea);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al guardar, no se ha podido guardar la Area: " + ex.Message;
            }
        }
        public string EliminarArea(int IdArea)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_Area AreaElim = (from p in dc.Cat_Area where (p.ID_Area == IdArea) select p).First();
                dc.Cat_Area.DeleteOnSubmit(AreaElim);
                dc.SubmitChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el Area " + ex.Message;
            }
        }
        public Cat_Area findById(int idArea)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from p in dc.Cat_Area where (p.ID_Area == idArea) select p).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool findByName(string nombreA)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                //(from p in dc.Cat_Area where (p.NombreArea == idNombre) select p).First();
                return dc.Cat_Area.Any(a => a.NombreArea.Equals(nombreA));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string updateArea(Cat_Area areaMod, int idArea)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_Area areUp = (from p in dc.Cat_Area where (p.ID_Area == idArea) select p).First();
                areUp.NombreArea = areaMod.NombreArea;
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
