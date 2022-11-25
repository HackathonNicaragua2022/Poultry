using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos.Catalogos;
using accesoDatos;
namespace POULTRY 
{
    public partial class arealaboral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EstadoGuardado"] != null)
            {
                string res = (string)Session["EstadoGuardado"];
                if (res.Equals("ok"))
                {
                    advertenciaMsg(true, tiposAdvertencias.informacion, "Guardado exitosamente!!", "La nueva Area Fue guardada de manera correcta!", "Todos los cambios fueron guardado exitosamente");
                }
                else
                {
                    advertenciaMsg(true, tiposAdvertencias.error, "Error al Guardar el Area", res, "Revice los datos he intente nuevamente");
                }
            }
            fillData();
        }

        protected void gv_area_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gv_area.BottomPagerRow;
            if (gv_area.Rows.Count > 0)
            {
                // Recupera los controles DropDownList y label...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("dr_areaPage");
                Label pageLabel = (Label)pagerRow.Cells[0].FindControl("currentPage_Area");
                if ((pageList != null))
                {
                    // Se crean los valores del DropDownList tomando el número total de páginas... 
                    int i = 0;
                    for (i = 0; i <= gv_area.PageCount - 1; i++)
                    {
                        // Se crea un objeto ListItem para representar la �gina...
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());
                        if (i == gv_area.PageIndex)
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
                    int currentPage = gv_area.PageIndex + 1;
                    // Actualiza el Label control con la �gina actual.
                    pageLabel.Text = "Página " + currentPage.ToString() + " de " + gv_area.PageCount.ToString();
                }
            }
        }
        protected void gv_area_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eliminar")
            {
                gv_area.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Session["OrigenAdvertencia_tr"] = "cmd_Eliminar";
                Session["ID_area"] = Convert.ToInt32(gv_area.SelectedValue);
                modalAdvertencia("Esta Area laboral se eliminara por completo del sistema siempre y cuando no se ha usado en el sistema<br/>¿Desea continuar?");
            }
            if (e.CommandName == "actualizar")
            {

                gv_area.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Cat_Area areaEdit = new Cat_Area_obj().findById((int)gv_area.SelectedValue);
                if (areaEdit != null)
                {
                    Session["OrigenAdvertencia_tr"] = "cmd_Editar";
                    Session["ID_area"] = Convert.ToInt32(gv_area.SelectedValue);

                    txt_Area.Text = areaEdit.NombreArea;

                    btn_Guardar.Attributes.Add("class", "btn btn-warning form-control");
                    btn_Guardar.Text = "Guardar los Cambios";
                    ScriptManager.RegisterStartupScript(this, GetType(), "modalEditar", "ShowModalNuevo();", true);
                }
            }
        }
        protected void dr_areaPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow pagerRow = gv_area.BottomPagerRow;
                // Recupera el control DropDownList...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("dr_areaPage");
                // Se Establece la propiedad PageIndex para visualizar la página seleccionada...           
                gv_area.PageIndex = pageList.SelectedIndex;
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
        protected void btn_Guardar_Click(object sender, EventArgs e)
        {

            string nombreA = txt_Area.Text;

            Cat_Area nArea = new Cat_Area();
            nArea.NombreArea = nombreA;

            Cat_Area_obj area_obj = new Cat_Area_obj();
            string res;
            if (!Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Editar"))
            {
                res = area_obj.guardarArea(nArea);
                Session["OrigenAdvertencia_tr"] = null;
            }
            else
            {
                int id_Area = Convert.ToInt32(Session["ID_area"]);
                res = area_obj.updateArea(nArea, id_Area);
            }
            Session["EstadoGuardado"] = res;
            Response.Redirect(Request.Url.ToString(), false);


        }
        public void fillData()
        {
            gv_area.DataSource = new Cat_Area_obj().getAllArea();
            gv_area.DataBind();
            if (gv_area.Rows.Count > 0)
            {
                this.gv_area.UseAccessibleHeader = true;
                TableCellCollection cells = gv_area.HeaderRow.Cells;
                gv_area.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                cells[3].Attributes.Add("data-breakpoints", "xs sm md");
                gv_area.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (Session["OrigenAdvertencia_tr"] != null)
            {
                if (Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Eliminar"))
                {
                    int idArea = Convert.ToInt32(Session["ID_area"]);
                    string result = new Cat_Area_obj().EliminarArea(idArea);
                    if (result.Equals("ok"))
                    {
                        modalInfo("El Area fue eliminado correctamente   ", tiposAdvertencias.informacion);
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