using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos;
using accesoDatos.Catalogos;
namespace POULTRY.Admin
{
    public partial class proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EstadoGuardado"] != null)
            {
                string res = (string)Session["EstadoGuardado"];
                if (res.Equals("ok"))
                {
                    advertenciaMsg(true, tiposAdvertencias.informacion, "Guardado exitosamente!!", "El nuevo Proveedor Fue guardada de manera correcta!", "Todos los cambios fueron guardado exitosamente");
                }
                else
                {
                    advertenciaMsg(true, tiposAdvertencias.error, "Error al Guardar el Proveedor", res, "Revice los datos he intente nuevamente");
                }
            }
            fillData();
        }

        protected void gv_proveedores_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gv_proveedores.BottomPagerRow;
            if (gv_proveedores.Rows.Count > 0)
            {
                // Recupera los controles DropDownList y label...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("dr_ProveedorPage");
                Label pageLabel = (Label)pagerRow.Cells[0].FindControl("currentPage_Proveedor");
                if ((pageList != null))
                {
                    // Se crean los valores del DropDownList tomando el número total de páginas... 
                    int i = 0;
                    for (i = 0; i <= gv_proveedores.PageCount - 1; i++)
                    {
                        // Se crea un objeto ListItem para representar la �gina...
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());
                        if (i == gv_proveedores.PageIndex)
                        {
                            item.Selected = true;
                        }
                        // Se añade el ListItem a la colección de Items del DropDownList...
                        pageList.Items.Add(item);
                    }
                }
                if ((pageLabel != null))
                {
                    // Calcula el nº de �gina actual...
                    int currentPage = gv_proveedores.PageIndex + 1;
                    // Actualiza el Label control con la �gina actual.
                    pageLabel.Text = "Página " + currentPage.ToString() + " de " + gv_proveedores.PageCount.ToString();
                }
            }
        }

        protected void gv_proveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eliminar")
            {
                gv_proveedores.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Session["OrigenAdvertencia_tr"] = "cmd_Eliminar";
                Session["ID_Proovedor_mod"] = Convert.ToInt32(gv_proveedores.SelectedValue);
                modalAdvertencia("Este Proveedor se eliminara por completo del sistema siempre y cuando no se ha usado en el sistema<br/>¿Desea continuar?");
            }
            if (e.CommandName == "actualizar")
            {

                gv_proveedores.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Tbl_Proveedores ProveedorEdit = new Tbl_Proveedores_obj().findById((int)gv_proveedores.SelectedValue);
                if (ProveedorEdit != null)
                {
                    Session["OrigenAdvertencia_tr"] = "cmd_Editar";
                    Session["ID_Proovedor_mod"] = Convert.ToInt32(gv_proveedores.SelectedValue);

                    txt_Empresa.Text = ProveedorEdit.NombreEmpresa;
                    txt_Contacto.Text = ProveedorEdit.NombreContacto;
                    txt_Telefono.Text = ProveedorEdit.NumeroTelf;
                    txt_Direccion.Text = ProveedorEdit.Direccion;
                    txt_Email.Text = ProveedorEdit.Email;

                    btn_Guardar.Attributes.Add("class", "btn btn-warning form-control");
                    btn_Guardar.Text = "Guardar los Cambios";
                    ScriptManager.RegisterStartupScript(this, GetType(), "modalEditar", "ShowModalNuevo();", true);
                }
            }
        }

        protected void dr_ProveedorPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow pagerRow = gv_proveedores.BottomPagerRow;
                // Recupera el control DropDownList...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("dr_ProveedorPage");
                // Se Establece la propiedad PageIndex para visualizar la página seleccionada...           
                gv_proveedores.PageIndex = pageList.SelectedIndex;
                //------------------------------
                fillData();
            }
            catch (Exception)
            {
            }
        }

        protected void btn_Guardar_Click(object sender, EventArgs e)
        {

            string NombreEmpresa = txt_Empresa.Text;
            string NombreContacto = txt_Contacto.Text;
            string Telefono = txt_Telefono.Text;
            string Direccion = txt_Direccion.Text;
            string Email = txt_Email.Text;

            Tbl_Proveedores nProveedor = new Tbl_Proveedores();
            nProveedor.NombreEmpresa = NombreEmpresa;
            nProveedor.NombreContacto = NombreContacto;
            nProveedor.NumeroTelf = Telefono;
            nProveedor.Direccion = Direccion;
            nProveedor.Email = Email;

            Tbl_Proveedores_obj Proveedor_obj = new Tbl_Proveedores_obj();
            string res;
            if (!Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Editar"))
            {
                res = Proveedor_obj.guardarProveedores(nProveedor);
                Session["OrigenAdvertencia_tr"] = null;
            }
            else
            {
                int ID_Proovedor_mod = Convert.ToInt32(Session["ID_Proovedor_mod"]);
                res = Proveedor_obj.updateProveedores(nProveedor, ID_Proovedor_mod);
            }
            Session["EstadoGuardado"] = res;
            Response.Redirect(Request.Url.ToString(), false);
        }

        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (Session["OrigenAdvertencia_tr"] != null)
            {
                if (Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Eliminar"))
                {
                    int idProveedor = Convert.ToInt32(Session["ID_Proovedor_mod"]);
                    string result = new Tbl_Proveedores_obj().EliminarProveedores(idProveedor);
                    if (result.Equals("ok"))
                    {
                        modalInfo("El Proveedor fue eliminado correctamente   ", tiposAdvertencias.informacion);
                    }
                    else
                    {
                        modalInfo("Error " + result
                            , tiposAdvertencias.error);
                    }
                    fillData();
                }
            }
        }
        public void fillData()
        {
            gv_proveedores.DataSource = new Tbl_Proveedores_obj().getAllProveedores();
            gv_proveedores.DataBind();
            if (gv_proveedores.Rows.Count > 0)
            {
                this.gv_proveedores.UseAccessibleHeader = true;
                TableCellCollection cells = gv_proveedores.HeaderRow.Cells;
                gv_proveedores.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //cells[3].Attributes.Add("data-breakpoints", "xs sm md");
                gv_proveedores.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        private void modalInfo(String msg, tiposAdvertencias advertencias)
        {
            lbl_modalInfo.Text = msg;
            switch (advertencias)
            {
                case tiposAdvertencias.advertencia:
                    imagenModalInfo.Src = "~/Imagenes/warning.png";
                    break;
                case tiposAdvertencias.error:
                    imagenModalInfo.Src = "~/Imagenes/error.png";
                    break;
                case tiposAdvertencias.informacion:
                    imagenModalInfo.Src = "~/Imagenes/camera_test.png";
                    break;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "ModalInfo", "ShowModalInfo();", true);
        }
        private void modalAdvertencia(String msg)
        {
            lbl_MsgAdvertencia.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalAdvertencia", "ShowModalAdvertencia();", true);
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
    }
}