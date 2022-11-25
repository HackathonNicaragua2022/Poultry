using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.Comedor.Control_Comedor
{
    public class loginUsuarioComedor_obj
    {
        /// <summary>
        /// verifica si el codigo y la contraseña ingresado son conrrectos
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="contrasena"></param>
        /// <returns>1 si los datos son correctos, -1 si el usuario no coincide, o el error generado</returns>
        public string verificarUsuario(String codigo, string contrasena)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.tbl_trabajadores.Any(a => a.CodigoBarra.ToUpper().Equals(codigo.ToUpper()) && a.ClaveAccesoSistema.Equals(contrasena)))
                {
                    if (contrasena.Equals("POULTRY 2022"))
                    {
                        return "0";// Indica que la contraseña esta por defecto
                    }
                    else {
                        return "1";
                    }
                }
                else
                {
                    return "-1";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// obtiene el id del trabajador por medio de su codigo y clave de acceso
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="contrasena"></param>
        /// <returns>int el ID si es correcto o bien -1 si no se encuentra</returns>
        public int getIDUsuarioComedor(String codigo, string contrasena)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.tbl_trabajadores.Any(a => a.CodigoBarra.ToUpper().Equals(codigo.ToUpper()) && a.ClaveAccesoSistema.Equals(contrasena)))
                {
                    return (from _trabajadores in dc.tbl_trabajadores where (_trabajadores.ClaveAccesoSistema.Equals(contrasena) && _trabajadores.CodigoBarra.ToUpper().Equals(codigo.ToUpper())) select _trabajadores).FirstOrDefault().ID_Trabajador;
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }


        public string actualizarcontrasena(int IDUsuario, string nuevacontrasena)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                tbl_trabajadores trabajador = (from _trabajador in dc.tbl_trabajadores where (_trabajador.ID_Trabajador == IDUsuario) select _trabajador).ToList().FirstOrDefault();
                trabajador.ClaveAccesoSistema = nuevacontrasena;
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
