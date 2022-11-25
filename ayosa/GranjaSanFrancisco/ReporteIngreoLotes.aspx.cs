using accesoDatos;
using accesoDatos.Granja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.GranjaSanFrancisco
{
    public partial class ReporteIngreoLotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fillLotes();
        }
        public void fillLotes()
        {
            IngresoLote_obj ingresos = new IngresoLote_obj();
            gv_Lotes.DataSource = ingresos.getLotesIngresados();
            gv_Lotes.DataBind();
            if (gv_Lotes.Rows.Count > 0)
            {
                this.gv_Lotes.UseAccessibleHeader = true;
                TableCellCollection cells = gv_Lotes.HeaderRow.Cells;
                gv_Lotes.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //cells[1].Attributes.Add("data-breakpoints", "md");
                //cells[2].Attributes.Add("data-breakpoints", "md");
                //cells[3].Attributes.Add("data-breakpoints", "md");
                cells[4].Attributes.Add("data-breakpoints", "xs sm");
                cells[5].Attributes.Add("data-breakpoints", "xs sm");
                cells[6].Attributes.Add("data-breakpoints", "xs sm");
                cells[7].Attributes.Add("data-breakpoints", "all");
                cells[8].Attributes.Add("data-breakpoints", "all");
                cells[9].Attributes.Add("data-breakpoints", "all");
                cells[10].Attributes.Add("data-breakpoints", "all");
                cells[11].Attributes.Add("data-breakpoints", "all");
                cells[12].Attributes.Add("data-breakpoints", "all");
                cells[13].Attributes.Add("data-breakpoints", "all");
                cells[14].Attributes.Add("data-breakpoints", "all");
                cells[15].Attributes.Add("data-breakpoints", "all");
                cells[16].Attributes.Add("data-breakpoints", "all");
                cells[17].Attributes.Add("data-breakpoints", "all");
                cells[18].Attributes.Add("data-breakpoints", "all");
                cells[19].Attributes.Add("data-breakpoints", "all");
                cells[20].Attributes.Add("data-breakpoints", "all");
                cells[21].Attributes.Add("data-breakpoints", "all");
                cells[22].Attributes.Add("data-breakpoints", "all");
                cells[23].Attributes.Add("data-breakpoints", "all");
                cells[24].Attributes.Add("data-breakpoints", "all");
                cells[25].Attributes.Add("data-breakpoints", "all");//xs sm md
                cells[26].Attributes.Add("data-breakpoints", "all");//xs sm md
                cells[27].Attributes.Add("data-breakpoints", "all");//xs sm md
                gv_Lotes.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
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
        protected void rp_LotesIngresados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Eliminar"))
            {
                int ID_LoteINgreso = int.Parse(e.CommandArgument.ToString());
                IngresoLote_obj ingresos = new IngresoLote_obj();
                string resul = ingresos.eliminarLote(ID_LoteINgreso);
                if (resul.Equals("1"))
                {

                }
                else
                {
                    mensaje("Error!" + resul, tiposAdvertencias.error);
                }
                //Session["IDLoteEliminar"] = ID_LoteINgreso;
                //ScriptManager.RegisterStartupScript(this, GetType(), "Show_Modal_Advertencia", "ShowModal_Advertencia();", true);
            }
        }

        protected void btn_EliminarLote_Click(object sender, EventArgs e)
        {
            if (Session["id_LoteEliminar"] != null)
            {
                int ID_LoteINgreso = (int)Session["id_LoteEliminar"];
                IngresoLote_obj ingresos = new IngresoLote_obj();
                string resul = ingresos.eliminarLote(ID_LoteINgreso);
                if (resul.Equals("1"))
                {
                    fillLotes();
                    mensaje("El lote se ha eliminado Correctamente!!", tiposAdvertencias.informacion);
                }
                else
                {
                 //   Response.Write(resul);
                    mensaje("Error!: " + resul, tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("intentelo Nuevamente!, se ha producido un error", tiposAdvertencias.error);
            }
        }

        protected void gv_Lotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eliminar")
            {
                gv_Lotes.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Session["id_LoteEliminar"] = Convert.ToInt32(gv_Lotes.SelectedValue);
                // mensaje("Eliminar, lote?", tiposAdvertencias.advertencia);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalAdvertencia", "ShowModal_Advertencia();", true);
            }
        }
        protected void gv_Lotes_DataBound(object sender, EventArgs e)
        {

        }

        protected void gv_Lotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID_Lotes = (int)gv_Lotes.DataKeys[e.Row.RowIndex].Value;
                //----------------------------------------------------------------------------------

                GridView gv_PersonalEntrada = (GridView)e.Row.FindControl("gv_Personal");
                IngresoLote_obj Ingreso_lotes = new IngresoLote_obj();
                gv_PersonalEntrada.DataSource = Ingreso_lotes.getPersonalEntrada(ID_Lotes);
                gv_PersonalEntrada.DataBind();              
            }

        }
    }
}