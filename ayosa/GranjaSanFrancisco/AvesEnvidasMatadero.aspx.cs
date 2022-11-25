using accesoDatos;
using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.GranjaSanFrancisco
{
    public partial class AvesEnvidasMatadero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["IDCompraBroilers_Remision"] = null;
            }
            fill_Procesos();
            fill_Remisiones();
        }
        public void fill_Remisiones()
        {
            Tbl_ViajesRemisionGranja_obj viajesRemisiones = new Tbl_ViajesRemisionGranja_obj();
            if (Session["IDCompraBroilers_Remision"] != null)
            {
                int IDCompraBroilers = (int)Session["IDCompraBroilers_Remision"];
                List<accesoDatos.ProduccionPollos.PlantasProcesadoras.Tbl_ViajesRemisionGranja_obj.ViajesRemision> remisiones = viajesRemisiones.getAllViajesRemision(IDCompraBroilers);
                lbl_TotalRemisiones.Text = remisiones.Count + " viajes";
                gv_Remisiones.DataSource = remisiones;
                gv_Remisiones.DataBind();
                if (gv_Remisiones.Rows.Count > 0)
                {
                    this.gv_Remisiones.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_Remisiones.HeaderRow.Cells;
                    gv_Remisiones.Attributes["class"] = "table table-sm  table-light table-condensed table-bordered table-striped";
                    gv_Remisiones.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    cells[1].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[2].Attributes.Add("data-breakpoints", "all");
                    cells[3].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[4].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[5].Attributes.Add("data-breakpoints", "all");
                    cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[7].Attributes.Add("data-breakpoints", "all");
                    cells[8].Attributes.Add("data-breakpoints", "all");
                    cells[9].Attributes.Add("data-breakpoints", "xs sm md");
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
                    cells[20].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[21].Attributes.Add("data-breakpoints", "all");
                    cells[22].Attributes.Add("data-breakpoints", "all");
                    gv_Remisiones.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        public void fill_Procesos()
        {
            Tbl_CompraPollos_obj compraPollo = new Tbl_CompraPollos_obj();
            gv_adquisicion.DataSource = compraPollo.getProcesosCompra(true);
            gv_adquisicion.DataBind();
            if (gv_adquisicion.Rows.Count > 0)
            {
                this.gv_adquisicion.UseAccessibleHeader = true;
                TableCellCollection cells = gv_adquisicion.HeaderRow.Cells;
                gv_adquisicion.Attributes["class"] = "table table-sm  table-light table-condensed table-bordered table-striped";
                gv_adquisicion.Columns[13].Visible = true;
                gv_adquisicion.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                cells[1].Attributes.Add("data-breakpoints", "xs sm md");//Numero Referencia
                cells[2].Attributes.Add("data-breakpoints", "all");
                //cells[3].Attributes.Add("data-breakpoints", "xs sm md");
                cells[4].Attributes.Add("data-breakpoints", "xs sm md");
                cells[5].Attributes.Add("data-breakpoints", "xs sm md");
                cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                cells[7].Attributes.Add("data-breakpoints", "all");
                cells[8].Attributes.Add("data-breakpoints", "all");
                cells[9].Attributes.Add("data-breakpoints", "all");
                cells[10].Attributes.Add("data-breakpoints", "all");
                cells[11].Attributes.Add("data-breakpoints", "all");
                cells[12].Attributes.Add("data-breakpoints", "all");
                //cells[13].Attributes.Add("data-breakpoints", "all");
                //cells[14].Attributes.Add("data-breakpoints", "all");
                gv_adquisicion.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
        protected void gv_adquisicion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_Seleccionar_Procesos")
            {
                //Primero capturas la fila
                int numFila = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
                gv_adquisicion.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IDCompraBroilers = Convert.ToInt32(gv_adquisicion.SelectedValue);
                // lbl_compraSeleccionada.Text = "Proceso seleccionado " + IDCompraBroilers.ToString().PadLeft(5, '0');
                Session["IDCompraBroilers_Remision"] = IDCompraBroilers;
                mensaje("Lote en proceso seleccionado: " + IDCompraBroilers, tiposAdvertencias.informacion);
                //Fill_Galeras();
                // row_NuevaRemisions.Visible = true;
                Tbl_CompraPollos_obj compraPollos = new Tbl_CompraPollos_obj();
                int IdGalera = compraPollos.getIDGalera_CompraPolloID(IDCompraBroilers);
                // fillInventarioGalera(IdGalera);
                fill_Remisiones();
            }
        }
        public void mensaje(string mensaje, tiposAdvertencias alerta)
        {
            switch (alerta)
            {
                case tiposAdvertencias.informacion:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "info_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.exito:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "success_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.advertencia:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "advertencia_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.error:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "error_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.pregunta:
                    ScriptManager.RegisterStartupScript(up_principal, GetType(), "Alert", "pregunta_alert('" + mensaje + "')", true);
                    break;
                default:
                    break;
            }
        }

        protected void gv_Remisiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_EliminarEnvio")
            {
                //Primero capturas la fila
                int numFila = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
                gv_Remisiones.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                Session["IDViajeRemisionGranja_Eliminar"] = Convert.ToInt32(gv_Remisiones.SelectedValue);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal_Advertencia", "ShowModalAdvertencia();", true);
            }
        }

        protected void gv_Remisiones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID_ViajeRemision = (int)gv_Remisiones.DataKeys[e.Row.RowIndex].Value;
                //----------------------------------------------------------------------------------

                GridView gv_Pesajes = (GridView)e.Row.FindControl("gv_Pesajes");
                Tbl_IngresoPesoVivo_Obj ingresoPesos = new Tbl_IngresoPesoVivo_Obj();
                gv_Pesajes.DataSource = ingresoPesos.getAllPesajes(ID_ViajeRemision);
                gv_Pesajes.DataBind();

                //---------------------------------------------------------------------------
                GridView gv_JavasEnviadas = (GridView)e.Row.FindControl("gv_javasEnviadas");
                Tbl_JavasEnviadas_obj javasEnviadas = new Tbl_JavasEnviadas_obj();
                gv_JavasEnviadas.DataSource = javasEnviadas.getAllJavasEnviadas(ID_ViajeRemision);
                gv_JavasEnviadas.DataBind();
            }
        }

        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (Session["IDViajeRemisionGranja_Eliminar"] != null)
            {
                int IDViajeRemisionGranja_Eliminar = (int)Session["IDViajeRemisionGranja_Eliminar"];
                Tbl_ViajesRemisionGranja_obj viajeRemisionGranja = new Tbl_ViajesRemisionGranja_obj();
                string result = viajeRemisionGranja.eliminarRemision(IDViajeRemisionGranja_Eliminar);
                if (result.Equals("1"))
                {
                    fill_Remisiones();
                    mensaje("El registro de remision se ha eliminado Correctamente, todos los datos se han restablecido!!", tiposAdvertencias.informacion);
                }
                else
                {
                    mensaje("Error intenta eliminar el Registro de Remision, Error: " + result, tiposAdvertencias.error);
                }
            }
        }
    }
}