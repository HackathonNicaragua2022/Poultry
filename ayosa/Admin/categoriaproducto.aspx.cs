using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos;
using accesoDatos.Catalogos;
namespace POULTRY 
{
    public partial class categoriaproducto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["EstadoGuardado"] = null;
                advertenciaAlert.Visible = false;
            }
            if (Session["EstadoGuardado"] != null)
            {
                string res = (string)Session["EstadoGuardado"];
                if (res.Equals("ok"))
                {
                    advertenciaMsg(true, tiposAdvertencias.informacion, "Guardado exitosamente!!", "La nueva Categoria Fue guardada de manera correcta!", "Todos los cambios fueron guardado exitosamente");
                }
                else
                {
                    advertenciaMsg(true, tiposAdvertencias.error, "Error al Guardar la Categoria", res, "Revice los datos he intente nuevamente");
                }
            }
            fillData();
        }

        protected void gv_Categoria_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gv_Categoria.BottomPagerRow;
            if (gv_Categoria.Rows.Count > 0)
            {
                // Recupera los controles DropDownList y label...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("dr_Categoria_Page");
                Label pageLabel = (Label)pagerRow.Cells[0].FindControl("currentPage_Categoria");
                if ((pageList != null))
                {
                    // Se crean los valores del DropDownList tomando el número total de páginas... 
                    int i = 0;
                    for (i = 0; i <= gv_Categoria.PageCount - 1; i++)
                    {
                        // Se crea un objeto ListItem para representar la �gina...
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());
                        if (i == gv_Categoria.PageIndex)
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
                    int currentPage = gv_Categoria.PageIndex + 1;
                    // Actualiza el Label control con la �gina actual.
                    pageLabel.Text = "Página " + currentPage.ToString() + " de " + gv_Categoria.PageCount.ToString();
                }
            }
        }

        protected void gv_Categoria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eliminar")
            {
                gv_Categoria.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Session["OrigenAdvertencia_tr"] = "cmd_Eliminar";
                Session["ID_CategoriaPro"] = Convert.ToInt32(gv_Categoria.SelectedValue);
                modalAdvertencia("Esta Cartegoria de producto se eliminara por completo del sistema siempre y cuando no se ha usado en el sistema<br/>¿Desea continuar?");
            }
            if (e.CommandName == "actualizar")
            {

                gv_Categoria.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Cat_CategoriaProducto categoriaEdit = new Cat_CategoriaProducto_obj().findById((int)gv_Categoria.SelectedValue);
                if (categoriaEdit != null)
                {
                    Session["OrigenAdvertencia_tr"] = "cmd_Editar";
                    Session["ID_CategoriaPro"] = Convert.ToInt32(gv_Categoria.SelectedValue);

                    txt_Categoria.Text = categoriaEdit.NombreCategoriaProducto;

                    btn_Guardar.Attributes.Add("class", "btn btn-warning form-control");
                    btn_Guardar.Text = "Guardar los Cambios";
                    ScriptManager.RegisterStartupScript(this, GetType(), "modalEditar", "ShowModalNuevo();", true);
                }
            }
        }

        protected void dr_Categoria_Page_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow pagerRow = gv_Categoria.BottomPagerRow;
                // Recupera el control DropDownList...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("dr_Categoria_Page");
                // Se Establece la propiedad PageIndex para visualizar la página seleccionada...           
                gv_Categoria.PageIndex = pageList.SelectedIndex;
                //------------------------------
                fillData();
            }
            catch (Exception)
            {
            }
        }
        private void modalAdvertencia(String msg)
        {
            lbl_MsgAdvertencia.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalAdvertencia", "ShowModalAdvertencia();", true);
        }
        protected void btn_Continuar_Click(object sender, EventArgs e)
        {

            if (Session["OrigenAdvertencia_tr"] != null)
            {
                if (Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Eliminar"))
                {
                    int ID_CategoriaPro = Convert.ToInt32(Session["ID_CategoriaPro"]);
                    string result = new Cat_CategoriaProducto_obj().EliminarCategoria_Producto(ID_CategoriaPro);
                    if (result.Equals("ok"))
                    {
                        modalInfo("La Categoria del producto fue eliminado correctamente   ", tiposAdvertencias.informacion);
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
            gv_Categoria.DataSource = new Cat_CategoriaProducto_obj().getAllCategoriaProducto();
            gv_Categoria.DataBind();
            if (gv_Categoria.Rows.Count > 0)
            {
                this.gv_Categoria.UseAccessibleHeader = true;
                TableCellCollection cells = gv_Categoria.HeaderRow.Cells;
                gv_Categoria.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                cells[3].Attributes.Add("data-breakpoints", "xs sm md");
                gv_Categoria.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
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
        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            string nombreC = txt_Categoria.Text;

            Cat_CategoriaProducto nCategoria = new Cat_CategoriaProducto();
            nCategoria.NombreCategoriaProducto = nombreC;

            Cat_CategoriaProducto_obj categoria_obj = new Cat_CategoriaProducto_obj();
            string res;
            if (!Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Editar"))
            {
                res = categoria_obj.guardarCategoria_Producto(nCategoria);
                Session["OrigenAdvertencia_tr"] = null;
            }
            else
            {
                int ID_CategoriaPro = Convert.ToInt32(Session["ID_CategoriaPro"]);
                res = categoria_obj.updateCategoria_Producto(nCategoria, ID_CategoriaPro);
            }
            Session["EstadoGuardado"] = res;
            Response.Redirect(Request.Url.ToString(), false);
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
    }
}