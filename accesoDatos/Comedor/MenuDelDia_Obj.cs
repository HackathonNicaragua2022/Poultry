using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Comedor
{
    public class MenuDelDia_Obj
    {
        public string guardarMenu(string Desayuno, string almuerzo, string Cena)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_MenuDia menu = (from _menu in dc.Cat_MenuDia select _menu).FirstOrDefault();
                if (menu != null)
                {
                    menu.Desayuno = Desayuno;
                    menu.Almuerzo = almuerzo;
                    menu.Cena = Cena;
                    menu.Fecha = DateTime.Now;
                    dc.SubmitChanges();
                }
                else
                {
                    menu = new Cat_MenuDia();
                    menu.Desayuno = Desayuno;
                    menu.Almuerzo = almuerzo;
                    menu.Cena = Cena;
                    menu.Fecha = DateTime.Now;
                    dc.Cat_MenuDia.InsertOnSubmit(menu);
                    dc.SubmitChanges();
                }
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// verifica si el menu esta actualizado con la fecha del dia
        /// </summary>
        /// <returns>Boolean</returns>
        public bool checkMenuDelDia()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Cat_MenuDia.Any(a => a.Fecha.Date == DateTime.Now.Date))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        public Cat_MenuDia getMenuDeldia()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Cat_MenuDia.Any(a => a.Fecha.Date == DateTime.Now.Date))
                {
                    return (from _menu in dc.Cat_MenuDia select _menu).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
