using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos
{
    public class objUsuarios
    {
        public int verificarUsuario(String usuario, string password)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                if (dc.Cat_Usuarios.Any(a => a.Usuario.Equals(usuario.ToUpper()) && a.Contraseña.Equals(password)))
                {
                    Cat_Usuarios result = (from _usuarios in dc.Cat_Usuarios where (_usuarios.Contraseña.Equals(password) && _usuarios.Usuario.Equals(usuario)) select _usuarios).FirstOrDefault();
                    return result.IDUsuarios;
                }
                else {
                    return -1;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
