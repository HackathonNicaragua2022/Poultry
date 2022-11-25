using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ProduccionHuevos
{
    public class ControlUsuario_obj
    {
        public string insertUser(int ID_Roles, string Usuario, string Contraseña, string Email, string telefono, bool activo, Guid GUID_websistem)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_Usuarios nuevoUsuario = new Cat_Usuarios();
                nuevoUsuario.ID_Roles = ID_Roles;
                nuevoUsuario.Usuario = Usuario;
                nuevoUsuario.Contraseña = Contraseña;
                nuevoUsuario.Email = Email;
                nuevoUsuario.telefono = telefono;
                nuevoUsuario.Activo = activo;
                nuevoUsuario.GUID_websisten = GUID_websistem;
                dc.Cat_Usuarios.InsertOnSubmit(nuevoUsuario);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public int getIDFromGUID(Guid idUsuario)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from usuario in dc.Cat_Usuarios where usuario.GUID_websisten == idUsuario select usuario).FirstOrDefault().IDUsuarios;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }

}
