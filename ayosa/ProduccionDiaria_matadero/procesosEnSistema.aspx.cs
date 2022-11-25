using accesoDatos;
using accesoDatos.ProduccionPollos.PlantasProcesadoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.ProduccionDiaria_matadero
{
    public partial class procesosEnSistema : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["EstadoProcesoVar"] = null;
            }
            CargarDatosCompra();
            fill_Procesos();
            fillCantidaProcesos();
        }


        public void CargarDatosCompra()
        {

            int ID_CompraBroilers = new Tbl_CompraPollos_obj().getIDProcesoActivo();
            if (ID_CompraBroilers > 0)
            {
                Tbl_CompraPollos_obj compraSelect = new Tbl_CompraPollos_obj();
                USP_CompraPollos_ProcesoxIDResult resultado = compraSelect.getProcesoCompraxID(ID_CompraBroilers);
                //-------------------------------
                lbl_IDCompra.Text = " #" + ID_CompraBroilers.ToString().PadLeft(5);
                lbl_NombreGranja.Text = resultado.Nombre;
                lbl_NumeroReferencia.Text = resultado.ReferenciaComentario;
                lbl_NumeroLote.Text = resultado.CodLote;
                lbl_FechaMatanza.Text = resultado.FechaMatanza.ToLongDateString();
                lbl_PrecioCompraxLibra.Text = string.Format("{0:C2}", resultado.PrecioCompraxLibra);
                lbl_TotalLibras.Text = string.Format("{0:n}", resultado.TotalLibrasCompradasCalculoBascula);
                lbl_AvesxRemision.Text = string.Format("{0:n}", resultado.TotalAvesRemisionCompradas);
                lbl_AvesConteoAutomatico.Text = string.Format("{0:n}", resultado.TotalAvesConteoAutomatico);
                lbl_RazaBroilers.Text = resultado.NombreRaza;
                lbl_enProceso.Text = (bool)resultado.EnProceso ? "No" : "Si";
                lbl_TotalCostoLibras.Text = string.Format("{0:C2}", resultado.CostoTotalxLibra);
                detallesProcesoActivo.Visible = true;
            }
            else
            {
                detallesProcesoActivo.Visible = false;
            }

        }

        public void fillCantidaProcesos()
        {
            Tbl_CompraPollos_obj compra = new Tbl_CompraPollos_obj();
            lbl_TotalProcesosPendientes.Text = compra.getTotalProcesosPendientes().ToString();
            lbl_TotalprocesosTerminados.Text = compra.getTotalProcesosTerminados().ToString();
        }

        public void fill_Procesos()
        {
            if (Session["EstadoProcesoVar"] != null)
            {
                Tbl_CompraPollos_obj compraPollo = new Tbl_CompraPollos_obj();

                bool EnEspera = (bool)Session["EstadoProcesoVar"];

                gv_adquisicion.DataSource = compraPollo.getProcesosCompraEnEspera(EnEspera);
                gv_adquisicion.DataBind();
                if (gv_adquisicion.Rows.Count > 0)
                {
                    this.gv_adquisicion.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_adquisicion.HeaderRow.Cells;

                    if (!EnEspera)
                    {
                        TituloGrid.Text = "Procesos de Matanza Terminados";
                        gv_adquisicion.Attributes.Remove("class");
                        gv_adquisicion.Attributes["class"] = "table table-bordered table-sm table-dark table-striped";
                        //gv_adquisicion.Columns[15].Visible = false;
                    }
                    else
                    {
                        TituloGrid.Text = "Procesos de Matanza En Esperas";
                        gv_adquisicion.Attributes["class"] = "table table-bordered table-sm table-light table-striped";
                        // gv_adquisicion.Columns[15].Visible = true;
                    }


                    gv_adquisicion.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    cells[1].Attributes.Add("data-breakpoints", "xs sm md");//Numero Referencia
                    //cells[2].Attributes.Add("data-breakpoints", "xs sm md");
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
                    cells[13].Attributes.Add("data-breakpoints", "all");
                    cells[14].Attributes.Add("data-breakpoints", "all");
                    gv_adquisicion.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

            }
        }

        protected void gv_adquisicion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gv_adquisicion.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int IDCompraBroilers = Convert.ToInt32(gv_adquisicion.SelectedValue);
            Session["IDCompraBroilers_Remision"] = IDCompraBroilers;
            if (e.CommandName == "cmd_Seleccionar_EnEspera")
            {
                Session["Continuarbtn"] = "command_CambioProceso";
                modalAdvertencia("Esta seguro que desea activar este proceso, si hay un proceso activo actualmente se desactivara y se pondra en espera junto a los demas procesos pendientes");
            }
            if (e.CommandName == "cmd_Borrar_Procesos")
            {
                Session["Continuarbtn"] = "command_DeleteProceso";
                modalAdvertencia("Esta completamente seguro que desa eliminar este proceso?");
            }
            if (e.CommandName == "cmd_finalizarProceso")
            {
                Session["Continuarbtn"] = "cmd_finalizarProceso";
                modalAdvertencia("Esta completamente seguro que desa Terminar este proceso no podra continuar agregando remisiones posteriormente?");
            }
            if (e.CommandName == "cmd_imprimir")
            {
                Response.Redirect("/Reportes/visorReporte.aspx?tiporeporte=rppv&codcompra=" + IDCompraBroilers);
            }
        }

        protected void btn_ProcesosPendientes_Click(object sender, EventArgs e)
        {
            Session["EstadoProcesoVar"] = true;
            fill_Procesos();
        }

        protected void btn_ProcesosTerminados_Click(object sender, EventArgs e)
        {
            Session["EstadoProcesoVar"] = false;
            fill_Procesos();

        }
        public void mensaje(string mensaje, tiposAdvertencias alerta)
        {
            switch (alerta)
            {
                case tiposAdvertencias.informacion:
                    ScriptManager.RegisterStartupScript(up_procesos, GetType(), "Alert", "info_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.exito:
                    ScriptManager.RegisterStartupScript(up_procesos, GetType(), "Alert", "success_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.advertencia:
                    ScriptManager.RegisterStartupScript(up_procesos, GetType(), "Alert", "advertencia_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.error:
                    ScriptManager.RegisterStartupScript(up_procesos, GetType(), "Alert", "error_alert('" + mensaje + "')", true);
                    break;
                case tiposAdvertencias.pregunta:
                    ScriptManager.RegisterStartupScript(up_procesos, GetType(), "Alert", "pregunta_alert('" + mensaje + "')", true);
                    break;
                default:
                    break;
            }
        }

        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (Session["Continuarbtn"] != null)
            {
                switch ((string)Session["Continuarbtn"])
                {
                    case "command_DeleteProceso":
                        if (Session["IDCompraBroilers_Remision"] != null)
                        {
                            int IDCompraBroilers = (int)Session["IDCompraBroilers_Remision"];
                            Tbl_CompraPollos_obj compras = new Tbl_CompraPollos_obj();
                            string result = compras.eliminarProceso(IDCompraBroilers);
                            if (result.Equals("1"))
                            {
                                fill_Procesos();
                                mensaje("Se elimino correctamente el proceso sin uso", tiposAdvertencias.exito);
                            }
                            else
                            {
                                mensaje("No se puede eliminar el Proceso por que ya fue usado <br/>ERROR: " + result, tiposAdvertencias.error);
                            }
                        }
                        break;
                    case "command_CambioProceso":
                        if (Session["IDCompraBroilers_Remision"] != null)
                        {
                            int IDCompraBroilers = (int)Session["IDCompraBroilers_Remision"];
                            Tbl_CompraPollos_obj compras = new Tbl_CompraPollos_obj();
                            string result = compras.ActivarProceso(IDCompraBroilers);
                            if (result.Equals("1"))
                            {
                                fill_Procesos();
                                CargarDatosCompra();                                
                                mensaje("El proceso se ha activado correctamente, ahora puede registrar las remisiones correspondientes!!", tiposAdvertencias.exito);
                            }
                            else
                            {
                                mensaje("No se puede activar el Proceso <br/>ERROR: " + result, tiposAdvertencias.error);
                            }
                        }
                        break;
                    case "cmd_finalizarProceso":
                        if (Session["IDCompraBroilers_Remision"] != null)
                        {
                            int IDCompraBroilers = (int)Session["IDCompraBroilers_Remision"];
                            Tbl_CompraPollos_obj compras = new Tbl_CompraPollos_obj();
                            string result = compras.finalizarCompra(IDCompraBroilers);
                            if (result.Equals("1"))
                            {
                                fill_Procesos();
                                CargarDatosCompra();
                                mensaje("Se ha Finalizado la Compra de Broilers Correctamente!!", tiposAdvertencias.exito);
                            }
                            else
                            {
                                mensaje("No se pudo finalizar la Compra <br/>ERROR: " + result, tiposAdvertencias.error);
                            }
                        }

                        break;
                    default:
                        break;
                }
            }
            else
            {
                mensaje("Sin comando", tiposAdvertencias.error);
            }
        }
        private void modalAdvertencia(String msg)
        {
            lbl_MsgAdvertencia.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalAdvertencia", "ShowModalAdvertencia();", true);
        }
    }
}