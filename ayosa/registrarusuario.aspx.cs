using accesoDatos.ProduccionHuevos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY 
{
    public partial class registrarusuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            //CreateUserWizard1.UserName;
            ControlUsuario_obj usuarioBodegaHuevo = new ControlUsuario_obj();

            MembershipUser user = Membership.GetUser(CreateUserWizard1.UserName);
            Guid guid = (Guid)user.ProviderUserKey;

            if (guid != null)
            {
                string result = usuarioBodegaHuevo.insertUser(2, CreateUserWizard1.UserName, CreateUserWizard1.Password, "", "", true, guid);
            }
        }
    }
}