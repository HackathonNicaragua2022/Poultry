using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos;
using accesoDatos.Catalogos;
using System.Web.Security;
using ultil;
namespace POULTRY 
{
    public partial class trabajadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //============================[Correge error de Sys.WebForms.PageRequestManagerParserErrorException: No se pudo analizar el mensaje recibido del servidor. ]=======================================
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.Btn_ExportarExcel);
                scriptManager.RegisterPostBackControl(this.btn_ExportarPDF);
            }
            catch (Exception)
            {
            }
            //===================================================================
            if (!IsPostBack)
            {
                Session["EstadoGuardado"] = null;
                Session["ID_Trabajador"] = null;
                dr_Area.DataSource = new Cat_Area_obj().getAllArea();
                dr_Area.DataTextField = "NombreArea";
                dr_Area.DataValueField = "ID_Area";
                dr_Area.DataBind();
                //---------------------------------------------------------------------------------
                dr_Cargo.DataSource = new Cat_Cargo_obj().getAllCargos();
                dr_Cargo.DataTextField = "NombreCargo";
                dr_Cargo.DataValueField = "ID_Cargo";
                dr_Cargo.DataBind();
            }
            if (Session["EstadoGuardado"] != null)
            {
                string res = (string)Session["EstadoGuardado"];
                if (res.Equals("ok"))
                {
                    advertenciaMsg(true, tiposAdvertencias.informacion, "Guardado exitosamente!!", "El nuevo Trabajador Fue guardado de manera correcta!", "Todos los cambios fueron guardado exitosamente");
                }
                else
                {
                    advertenciaMsg(true, tiposAdvertencias.error, "Error al Guardar el Trabajador", res, "Revice los datos he intente nuevamente");
                }
            }
            fillData();
        }
        public void fillData()
        {
            gv_Trabajador.DataSource = new Tbl_Trabajador_obj().getAllTrabajadorUSP();
            gv_Trabajador.DataBind();
            if (gv_Trabajador.Rows.Count > 0)
            {
                this.gv_Trabajador.UseAccessibleHeader = true;
                TableCellCollection cells = gv_Trabajador.HeaderRow.Cells;
                gv_Trabajador.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //cells[1].Attributes.Add("data-sortable", "true");
                cells[1].Attributes.Add("data-breakpoints", "all");
                cells[2].Attributes.Add("data-breakpoints", "all");
                //cells[4].Attributes.Add("data-breakpoints", "xs sm md");
                cells[5].Attributes.Add("data-breakpoints", "all");
                cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                cells[7].Attributes.Add("data-breakpoints", "xs sm md");
                // cells[4].Attributes.Add("data-breakpoints", "all");
                gv_Trabajador.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void gv_Trabajador_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gv_Trabajador.BottomPagerRow;
            if (gv_Trabajador.Rows.Count > 0)
            {
                // Recupera los controles DropDownList y label...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
                Label pageLabel = (Label)pagerRow.Cells[0].FindControl("currentPage_Trabajador");
                if ((pageList != null))
                {
                    // Se crean los valores del DropDownList tomando el número total de páginas... 
                    int i = 0;
                    for (i = 0; i <= gv_Trabajador.PageCount - 1; i++)
                    {
                        // Se crea un objeto ListItem para representar la �gina...
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());
                        if (i == gv_Trabajador.PageIndex)
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
                    int currentPage = gv_Trabajador.PageIndex + 1;
                    // Actualiza el Label control con la �gina actual.
                    pageLabel.Text = "Página " + currentPage.ToString() + " de " + gv_Trabajador.PageCount.ToString();
                }
            }
        }

        protected void gv_Trabajador_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eliminar")
            {
                gv_Trabajador.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Session["OrigenAdvertencia_tr"] = "cmd_Eliminar";
                Session["ID_Trabajador"] = Convert.ToInt32(gv_Trabajador.SelectedValue);
                modalAdvertencia("Este Trabajador se eliminara por completo del sistema siempre y cuando no se ha usado en ventas<br/>¿Desea continuar?");
            }
            if (e.CommandName == "actualizar")
            {

                gv_Trabajador.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                tbl_trabajadores objEdit = new Tbl_Trabajador_obj().findById((int)gv_Trabajador.SelectedValue);
                if (objEdit != null)
                {
                    dr_Area.SelectedValue = objEdit.ID_Area.ToString();
                    dr_Cargo.SelectedValue = objEdit.ID_Cargo.ToString();

                    Session["OrigenAdvertencia_tr"] = "cmd_Editar";
                    Session["ID_Trabajador"] = Convert.ToInt32(gv_Trabajador.SelectedValue);

                    txt_Cedula.Text = objEdit.Cedula;
                    txt_PrimerNOmbre.Text = objEdit.Nombre_1;
                    txt_Clave.Text = objEdit.ClaveAccesoSistema;
                    txt_CodigoBarra.Text = objEdit.CodigoBarra;
                    chk_activo.Checked =(bool) objEdit.Activo;
                    btn_Guardar.Attributes.Add("class", "btn btn-warning form-control");
                    btn_Guardar.Text = "Guardar los Cambios";
                    ScriptManager.RegisterStartupScript(this, GetType(), "modalEditar", "ShowModalNuevo();", true);
                }
            }
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
        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (Session["OrigenAdvertencia_tr"] != null)
            {
                // lbl_MsgAdvertencia.Text = "Trabajador eliminado correctamente";
                // ScriptManager.RegisterStartupScript(this, GetType(), "OcultarModalAdvertencia", "ShowModalAdvertencia();", true);
                //Response.Redirect(Request.Url.ToString(), false);
                if (Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Eliminar"))
                {
                    int idTrabajador = Convert.ToInt32(Session["ID_Trabajador"]);
                    string result = new Tbl_Trabajador_obj().EliminarTrabajador(idTrabajador);
                    if (result.Equals("ok"))
                    {
                        Session["EstadoGuardado"] = null;
                        Session["ID_Trabajador"] = null;
                        modalInfo("El Trabajador fue eliminado correctamente   ", tiposAdvertencias.informacion);
                    }
                    else
                    {
                        modalInfo("Error   " + result
                            , tiposAdvertencias.error);
                    }
                    fillData();
                }
            }
        }

        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (dr_Cargo.Items.Count <= 0 && dr_Area.Items.Count <= 0)
            {
                advertenciaMsg(true, tiposAdvertencias.advertencia, "Advertencia!!", "No hay elementos en los controles Area y Cargo, por favor verifique que ya se han ingresado con anterioridad y vuelva a intentarlo", "Las listas estan vacias!!");
            }
            else
            {

                string nombre1 = txt_PrimerNOmbre.Text;

                string Cedula = txt_Cedula.Text;

                int idArea = int.Parse(dr_Area.SelectedValue);
                int idCargo = int.Parse(dr_Cargo.SelectedValue);
                string codigoBarra = txt_CodigoBarra.Text;
                string Clave = txt_Clave.Text;
                bool activo = chk_activo.Checked;

                tbl_trabajadores nobj = new tbl_trabajadores();
                nobj.Cedula = Cedula;
                nobj.Nombre_1 = nombre1;
                nobj.ID_Area = idArea;
                nobj.ID_Cargo = idCargo;
                nobj.ClaveAccesoSistema = Clave;
                nobj.CodigoBarra = codigoBarra;
                nobj.Activo = activo;
                nobj.CreadoPor = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
                Tbl_Trabajador_obj Trabajador_obj = new Tbl_Trabajador_obj();
                string res;
                if (!Convert.ToString(Session["OrigenAdvertencia_tr"]).Equals("cmd_Editar"))
                {
                    Session["EstadoGuardado"] = null;
                    Session["ID_Trabajador"] = null;
                    res = Trabajador_obj.guardarTrabajador(nobj);
                }
                else
                {
                    int idTrabajador = Convert.ToInt32(Session["ID_Trabajador"]);
                    res = Trabajador_obj.updateTrabajador(nobj, idTrabajador);
                }

                Session["EstadoGuardado"] = res;
                Response.Redirect(Request.Url.ToString(), false);

            }
        }

        protected void dr_Page_Trabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow pagerRow = gv_Trabajador.BottomPagerRow;
                // Recupera el control DropDownList...
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
                // Se Establece la propiedad PageIndex para visualizar la página seleccionada...           
                gv_Trabajador.PageIndex = pageList.SelectedIndex;
                //------------------------------
                fillData();
            }
            catch (Exception)
            {
            }
        }

        protected void Btn_ExportarExcel_Click(object sender, EventArgs e)
        {
            Exportacion ex = new Exportacion("POULTRYConn");
            try
            {
                ex.ExportToExcel("EXEC [dbo].[GetAllTrabajadores_rel]", "TODOS LOS EMPLEADOS POULTRY-" + DateTime.Now);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btn_ExportarPDF_Click(object sender, EventArgs e)
        {
            Exportacion ex = new Exportacion("POULTRYConn");
            try
            {
                //ex.ExportToExcel("EXEC [dbo].[GetAllTrabajadores_rel]", "TODOS LOS EMPLEADOS POULTRY-" + DateTime.Now);
                ex.ExportToPDF("EXEC [dbo].[GetAllTrabajadores_rel]", "TODOS LOS EMPLEADOS POULTRY-" + DateTime.Now);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}