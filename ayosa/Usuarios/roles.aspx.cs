using accesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace POULTRY.Usuarios
{
    public partial class roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Bind the users and roles 
                BindUsersToUserList();
                BindRolesToList();
                CheckRolesForSelectedUser();
                DisplayUsersBelongingToRole();
            }
            fill_folderTree();
        }
        public void fill_folderTree()
        {
            var homeNode = SiteMap.Provider.FindSiteMapNode("~/POULTRY ");
            rptMenu.DataSource = homeNode.ChildNodes;
            rptMenu.DataBind();
        }
        private void BindUsersToUserList()
        {
            // Get all of the user accounts 
            MembershipUserCollection users = Membership.GetAllUsers();
            dr_Usuarios.DataSource = users;
            dr_Usuarios.DataTextField = "UserName";
            dr_Usuarios.DataBind();
        }
        private void BindRolesToList()
        {
            // Get all of the roles 
            string[] roles = Roles.GetAllRoles();
            rp_roles.DataSource = roles;
            rp_roles.DataBind();

            dr_Roles.DataSource = roles;
            dr_Roles.DataBind();


            dr_RolesPaginas.DataSource = roles;
            dr_RolesPaginas.DataBind();
        }
        private void DisplayUsersBelongingToRole()
        {
            // Get the selected role 
            string selectedRoleName = dr_Roles.SelectedValue;

            // Get the list of usernames that belong to the role 
            string[] usersBelongingToRole = Roles.GetUsersInRole(selectedRoleName);

            // Bind the list of users to the GridView 
            gv_UsuariosenRoles.DataSource = usersBelongingToRole;
            gv_UsuariosenRoles.DataBind();
        }
        protected void dr_Usuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckRolesForSelectedUser();
        }
        public void mensaje(string mensaje, tiposAdvertencias alerta)
        {
            switch (alerta)
            {
                case tiposAdvertencias.informacion:
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Alert", "info_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.exito:
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Alert", "success_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.advertencia:
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Alert", "advertencia_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.error:
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Alert", "error_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.pregunta:
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Alert", "pregunta_alert('" + mensaje + "')", true);
                    break;
                default:
                    break;
            }
        }

        protected void btn_NuevoRole_Click(object sender, EventArgs e)
        {
            if (txt_NombreNUevoRole.Text.Length > 0)
            {
                Roles.CreateRole(txt_NombreNUevoRole.Text);
                txt_NombreNUevoRole.Text = "";
                BindUsersToUserList();
                BindRolesToList();
                CheckRolesForSelectedUser();
                mensaje("Nuevo Role Credo Con exito!!", tiposAdvertencias.exito);
            }
            else
            {
                mensaje("No deje el Campo Nombre Nuevo Role vacio!!", tiposAdvertencias.advertencia);
            }
        }
        private void CheckRolesForSelectedUser()
        {
            // Determine what roles the selected user belongs to 
            string selectedUserName = dr_Usuarios.SelectedValue;
            string[] selectedUsersRoles = Roles.GetRolesForUser(selectedUserName);

            // Loop through the Repeater's Items and check or uncheck the checkbox as needed 

            foreach (RepeaterItem ri in rp_roles.Items)
            {
                // Programmatically reference the CheckBox 
                CheckBox RoleCheckBox = ri.FindControl("chk_roles") as CheckBox;
                // See if RoleCheckBox.Text is in selectedUsersRoles 
                if (selectedUsersRoles.Contains<string>(RoleCheckBox.Text))
                    RoleCheckBox.Checked = true;
                else
                    RoleCheckBox.Checked = false;
            }
        }

        protected void chk_roles_CheckedChanged(object sender, EventArgs e)
        {
            // Reference the CheckBox that raised this event 
            CheckBox RoleCheckBox = sender as CheckBox;

            // Get the currently selected user and role 
            string selectedUserName = dr_Usuarios.SelectedValue;

            string roleName = RoleCheckBox.Text;

            // Determine if we need to add or remove the user from this role 
            if (RoleCheckBox.Checked)
            {
                // Add the user to the role 
                Roles.AddUserToRole(selectedUserName, roleName);

                DisplayUsersBelongingToRole();

                // Display a status message 
                mensaje(string.Format("El Usuario {0} fue agregado al role {1}.", selectedUserName, roleName), tiposAdvertencias.exito);
            }
            else
            {
                // Remove the user from the role 
                Roles.RemoveUserFromRole(selectedUserName, roleName);
                // Display a status message 
                mensaje(string.Format("El usuario {0} fue removido del role {1}.", selectedUserName, roleName), tiposAdvertencias.advertencia);
            }
        }

        protected void dr_Roles_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayUsersBelongingToRole();
        }

        protected void gv_UsuariosenRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the selected role 
            string selectedRoleName = dr_Roles.SelectedValue;

            // Reference the UserNameLabel 
            Label UserNameLabel = gv_UsuariosenRoles.Rows[e.RowIndex].FindControl("UserNameLabel") as Label;

            // Remove the user from the role 
            Roles.RemoveUserFromRole(UserNameLabel.Text, selectedRoleName);

            // Refresh the GridView 
            DisplayUsersBelongingToRole();
            CheckRolesForSelectedUser();
            // Display a status message 
            mensaje(string.Format("El Usuario {0} fue removido del rol {1}.", UserNameLabel.Text, selectedRoleName), tiposAdvertencias.informacion);
        }



        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            AuthorizationRule newRule;
            newRule = new AuthorizationRule(AuthorizationRuleAction.Allow);
            AddRoleRule(newRule, txt_Ruta.Text.Trim(), dr_RolesPaginas.SelectedValue.Trim());
        }

        private void UpdateLocation(AuthorizationRule newRule, string location, string role)
        {
            try
            {
                string path = Server.MapPath("~/Web.Config");
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                XmlNodeList list = xDoc.DocumentElement.SelectNodes("location");
                if (list.Count > 0)
                {
                    XmlNode node = list[0];
                    XmlAttribute attribute = node.Attributes["path"];
                    attribute.Value = this.txt_Ruta.Text.Trim();
                    node.Attributes.Append(attribute);
                    xDoc.Save(path);
                }
                else
                {//create location element and the path attribute with it's value set to the
                    //selected page
                    XmlElement newLocationelement = xDoc.CreateElement("location");
                    XmlAttribute newLocationAttrib = xDoc.CreateAttribute("path");
                    newLocationAttrib.Value = location;
                    newLocationelement.Attributes.Append(newLocationAttrib);
                    XmlElement newSystemWebelement = xDoc.CreateElement("system.web");
                    XmlElement newAuthorizationelement = xDoc.CreateElement("authorization");
                    //create the allow element
                    XmlElement newAllowelement = xDoc.CreateElement("allow");
                    XmlAttribute newAllowAttrib = xDoc.CreateAttribute("roles");
                    newRule.Roles.Add(role).ToString();
                    string listofRoles = "";
                    foreach (var item in newRule.Roles)
                    {
                        listofRoles = item.ToString();
                    }
                    newAllowAttrib.Value = listofRoles;
                    newAllowelement.Attributes.Append(newAllowAttrib);
                    //create the deny element
                    XmlElement newDenyelement = xDoc.CreateElement("deny");
                    XmlAttribute newUsersAttrib = xDoc.CreateAttribute("users");
                    newUsersAttrib.Value = "*";
                    newDenyelement.Attributes.Append(newUsersAttrib);
                    newAuthorizationelement.AppendChild(newAllowelement);
                    newAuthorizationelement.AppendChild(newDenyelement);
                    newLocationelement.AppendChild(newSystemWebelement);
                    newSystemWebelement.AppendChild(newAuthorizationelement);
                    xDoc.DocumentElement.AppendChild(newLocationelement);
                    xDoc.PreserveWhitespace = true;
                    //write to web.config file using xml writer
                    XmlTextWriter xwriter = new XmlTextWriter(path, null);
                    xDoc.WriteTo(xwriter);
                    xwriter.Close();

                }
                mensaje("se ha guardado la configuracion correctamente!!", tiposAdvertencias.exito);
            }
            catch (Exception ex)
            {
                mensaje("Ocurrio un error y no se puede realizar la accion necesaria </br>Error:" + ex.Message, tiposAdvertencias.error);
            }
        }

        private void AddRoleRule(AuthorizationRule newRule, string location, string role)
        {
            try
            {
                string path = Server.MapPath("~/Web.Config");
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
 

                //create location element and the path attribute with it's value set to the
                //selected page
                XmlElement newLocationelement = xDoc.CreateElement("location");
                XmlAttribute newLocationAttrib = xDoc.CreateAttribute("path");
                newLocationAttrib.Value = location;
                newLocationelement.Attributes.Append(newLocationAttrib);
                XmlElement newSystemWebelement = xDoc.CreateElement("system.web");
                XmlElement newAuthorizationelement = xDoc.CreateElement("authorization");
                //create the allow element
                XmlElement newAllowelement = xDoc.CreateElement("allow");
                XmlAttribute newAllowAttrib = xDoc.CreateAttribute("roles");
                newRule.Roles.Add(role).ToString();
                string listofRoles = "";
                foreach (var item in newRule.Roles)
                {
                    listofRoles = item.ToString();
                }
                newAllowAttrib.Value = listofRoles;
                newAllowelement.Attributes.Append(newAllowAttrib);
                //create the deny element
                XmlElement newDenyelement = xDoc.CreateElement("deny");
                XmlAttribute newUsersAttrib = xDoc.CreateAttribute("users");
                newUsersAttrib.Value = "*";
                newDenyelement.Attributes.Append(newUsersAttrib);
                newAuthorizationelement.AppendChild(newAllowelement);
                newAuthorizationelement.AppendChild(newDenyelement);
                newLocationelement.AppendChild(newSystemWebelement);
                newSystemWebelement.AppendChild(newAuthorizationelement);
                xDoc.DocumentElement.AppendChild(newLocationelement);
                xDoc.PreserveWhitespace = true;
                //write to web.config file using xml writer
                XmlTextWriter xwriter = new XmlTextWriter(path, null);
                xDoc.WriteTo(xwriter);
                xwriter.Close();
                mensaje("se ha guardado la configuracion correctamente!!", tiposAdvertencias.exito);
            }
            catch (Exception ex)
            {
                mensaje("Ocurrio un error y no se puede realizar la accion necesaria </br>Error:" + ex.Message, tiposAdvertencias.error);
            }
        }

        protected void btn_Acutalizar_Click(object sender, EventArgs e)
        {
            AuthorizationRule newRule;
            newRule = new AuthorizationRule(AuthorizationRuleAction.Allow);
            this.UpdateLocation(newRule, txt_Ruta.Text.Trim(), dr_RolesPaginas.SelectedValue.Trim());
        }

        protected void btn_padresRoot_Command(object sender, CommandEventArgs e)
        {
            txt_Ruta.Text = e.CommandArgument.ToString().Substring(1);
        }

        protected void btn_padreschildrens_Command(object sender, CommandEventArgs e)
        {
            txt_Ruta.Text = e.CommandArgument.ToString().Substring(1);
        }
    }
}