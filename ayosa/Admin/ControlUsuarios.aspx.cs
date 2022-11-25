using accesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY 
{
    public partial class ControlUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fillgridview();
            if (!Page.IsPostBack)
            {
                //Roles.CreateRole("Admin");
                //Roles.CreateRole("FacturadorComedor");

                // Bind the users and roles 
                BindUsersToUserList();
                BindRolesToList();
                CheckRolesForSelectedUser();
            }
        }
        private void fillgridview()
        {
            MembershipUserCollection member = Membership.GetAllUsers();
            gv_miembros.DataSource = member;
            gv_miembros.DataBind();
            gv_miembros.HeaderRow.TableSection = TableRowSection.TableHeader;

            this.gv_miembros.UseAccessibleHeader = true;
            TableCellCollection cells = gv_miembros.HeaderRow.Cells;
            gv_miembros.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
            cells[1].Attributes.Add("data-breakpoints", "xs sm md");
            cells[2].Attributes.Add("data-breakpoints", "all");
            cells[3].Attributes.Add("data-breakpoints", "all");
            cells[5].Attributes.Add("data-breakpoints", "all");
            cells[6].Attributes.Add("data-breakpoints", "all");
            cells[7].Attributes.Add("data-breakpoints", "all");
            cells[8].Attributes.Add("data-breakpoints", "all");
            cells[9].Attributes.Add("data-breakpoints", "all");
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
            List<String> rolesStringroles = roles.ToList<String>();
            rolesStringroles.Add("= Sin Roll Asignado =");
            dr_roles.DataSource = rolesStringroles;
            //dr_roles.DataTextField = "RoleName";
            dr_roles.DataBind();
        }
        private void CheckRolesForSelectedUser()
        {
            // Determine what roles the selected user belongs to 
            string selectedUserName = dr_Usuarios.SelectedValue;
            string[] selectedUsersRoles = Roles.GetRolesForUser(selectedUserName);
            if (selectedUsersRoles.Length > 0)
            {
                this.dr_roles.SelectedIndex = this.dr_roles.Items.IndexOf(this.dr_roles.Items.FindByText(selectedUsersRoles[0]));
            }
            else
            {
                this.dr_roles.SelectedIndex = this.dr_roles.Items.IndexOf(this.dr_roles.Items.FindByText("= Sin Roll Asignado ="));
            }
        }

        protected void dr_Usuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckRolesForSelectedUser();
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            string selectedUserName = dr_Usuarios.SelectedValue;
            string[] selectedUsersRoles = Roles.GetRolesForUser(selectedUserName);
            String actualRoll = selectedUsersRoles.Length > 0 ? selectedUsersRoles[0] : "= Sin Roll Asignado =";
            String selectedValue = dr_roles.SelectedValue;
            if (selectedValue.Equals("= Sin Roll Asignado ="))
            {
                try
                {
                    foreach (var item in Roles.GetAllRoles())
                    {
                        if (Roles.IsUserInRole(selectedUserName, item))
                        {
                            Roles.RemoveUserFromRole(selectedUserName, item);
                        }
                    }
                    advertenciaMsg(true, tiposAdvertencias.informacion, "Informacion!!", "El Usuario fue removido de su Rol", "Removido!");
                }
                catch (System.Configuration.Provider.ProviderException ex)
                {
                    advertenciaMsg(true, tiposAdvertencias.error, "Error!!", ex.Message, "ERROR!");
                }
            }
            else
            {
                //.RemoveUserFromRoles(selectedUserName, Roles.GetAllRoles());
                try
                {
                    foreach (var item in Roles.GetAllRoles())
                    {
                        if (Roles.IsUserInRole(selectedUserName, item))
                        {
                            Roles.RemoveUserFromRole(selectedUserName, item);
                        }
                    }
                    Roles.AddUserToRole(selectedUserName, selectedValue);
                    advertenciaMsg(true, tiposAdvertencias.informacion, "Informacion!!", "El Usuario fue agregado al Roll: " + selectedValue + ", correctamente", "Agregado!");
                }
                catch (System.Configuration.Provider.ProviderException ex)
                {
                    advertenciaMsg(true, tiposAdvertencias.error, "Error!!", ex.Message, "ERROR!");
                }
            }
        }

        protected void btn_Suspender_Click(object sender, EventArgs e)
        {
            String _user = dr_Usuarios.SelectedValue;
            if (Membership.GetUser(_user).IsLockedOut == false)
            {
                MembershipUser user = Membership.GetUser(_user);
                for (int i = 0; i < Membership.MaxInvalidPasswordAttempts; i++)
                {
                    user.ChangePassword("Nothing", "Nothing");
                }
                Membership.UpdateUser(user);
                advertenciaMsg(true, tiposAdvertencias.informacion, "Informacion!!", "La cuenta del Usuario " + _user + ", se ha Suspendido!!", "Suspendido!");

            }
            else
            {
                advertenciaMsg(true, tiposAdvertencias.advertencia, "Advertencia!!", "La cuenta del Usuario " + _user + ", ya se encuentra suspendida", "Y esta suspendido!");
            }
            fillgridview();
        }

        protected void btn_reactivar_Click(object sender, EventArgs e)
        {
            String _user = dr_Usuarios.SelectedValue;
            if (Membership.GetUser(_user).IsLockedOut)
            {
                Membership.GetUser(_user).UnlockUser();
                Membership.GetUser(_user).IsApproved = true;
                MembershipUser usuario = Membership.GetUser(_user);
                Membership.UpdateUser(usuario);
                advertenciaMsg(true, tiposAdvertencias.informacion, "Informacion!!", "La cuenta del Usuario " + _user + ", se ha reactivado!!", "Reactivado!");

            }
            else
            {
                advertenciaMsg(true, tiposAdvertencias.advertencia, "Advertencia!!", "La cuenta del Usuario " + _user + ", no se encuentra suspendida", "No esta suspendido!");

            }
            fillgridview();
        }
        public void advertenciaMsg(bool visible, tiposAdvertencias tipoa, string cabeza, string cuerpo, string pie)
        {
            advertenciaAlert.Visible = visible;
            switch (tipoa)
            {

                case tiposAdvertencias.informacion:
                    advertenciaAlert.Attributes["class"] = "alert alert-info";
                    break;
                case tiposAdvertencias.exito:
                    advertenciaAlert.Attributes["class"] = "alert alert-success";
                    break;
                case tiposAdvertencias.advertencia:
                    advertenciaAlert.Attributes["class"] = "alert alert-warning";
                    break;
                case tiposAdvertencias.error:
                    advertenciaAlert.Attributes["class"] = "alert alert-danger";
                    break;
                default:
                    advertenciaAlert.Attributes["class"] = "alert alert-info";
                    break;
            }
            lbl_advertenciaHeader.Text = cabeza;
            lbl_CuerpoAdvertencia.Text = cuerpo;
            lbl_piesAdvertencia.Text = pie;
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Membership.DeleteUser(dr_Usuarios.SelectedItem.Text);
                BindUsersToUserList();
                advertenciaMsg(true, tiposAdvertencias.informacion, "Eliminando Usuario!!", "Usuario eliminado  " + dr_Usuarios.SelectedItem.Text, "Eliminacion de Usuario");
            }
            catch (Exception ex)
            {
                advertenciaMsg(true, tiposAdvertencias.error, "Error Eliminando Usuario!!", "No se puede eliminar el usuario por que ya hay un historial en el sistema</br>Error: " + ex.Message, "Ya hay una relacion entre otros objetos de la base de datos");
            }
        }
    }
}