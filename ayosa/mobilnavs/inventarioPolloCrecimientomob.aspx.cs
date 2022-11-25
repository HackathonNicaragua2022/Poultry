using accesoDatos;
using accesoDatos.Granja;
using accesoDatos.ProduccionPollos.ControlLotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class inventarioPolloCrecimientomob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //Obtiene el valor de la variable idgalera de la Url
                    int IdGalera = int.Parse(Request.QueryString["idgalera"]);
                    Session["IdGalera"] = IdGalera;
                    Tbl_Galeras galera = getGaleraSeleccionada(IdGalera);
                    fillInventarioGalera(IdGalera);
                    lbl_NUmeroGalera.Text = galera.NumeroOrden.ToString();
                    lbl_capacidadInstalada.Text = string.Format("{0:n}", galera.CapacidadInstalada);
                    lbl_CapacidadNormal.Text = string.Format("{0:n}", galera.CapacidadNormal);
                    decimal porcentajeUso = 0;
                    try
                    {
                        porcentajeUso = (Convert.ToDecimal(galera.CapacidadNormal) / Convert.ToDecimal(galera.CapacidadInstalada)) * 100;
                    }
                    catch (Exception)
                    {
                        porcentajeUso = 0;
                    }
                    lblPorcentajeUso.Text = string.Format("{0:n}", porcentajeUso);
                }
                catch (Exception)
                {
                    Response.Redirect("~/GranjaSanFrancisco/LotesBroilers.aspx");
                }
            }
            fillParametros();
            filConsumoAlimentos();
        }
        public void filConsumoAlimentos()
        {
            if (Session["ID_LoteProduccion"] != null)
            {
                int ID_LoteProduccion = (int)Session["ID_LoteProduccion"];

                ControlAlimenticio_obj registroConsumoAlimento = new ControlAlimenticio_obj();
                // chale cree dos clases, da pereza
                RegistroConsumoAlimentos_obj registroAlimentos = new RegistroConsumoAlimentos_obj();
                /// Resumen
                rp_ControlAlimentos.DataSource = registroAlimentos.getResumenConsumoAlimentoLote(ID_LoteProduccion);
                rp_ControlAlimentos.DataBind();
                gv_ConsumoAlimentos.DataSource = registroConsumoAlimento.getAll(ID_LoteProduccion);
                gv_ConsumoAlimentos.DataBind();
                if (gv_ConsumoAlimentos.Rows.Count > 0)
                {
                    /*
                     Default BreakPoins
                     * "xs": 480, // extra small
                     * "sm": 768, // small
                     * "md": 992, // medium
                     * "lg": 1200 // large
                     */


                    this.gv_ConsumoAlimentos.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_ConsumoAlimentos.HeaderRow.Cells;
                    gv_ConsumoAlimentos.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    gv_ConsumoAlimentos.HeaderRow.Attributes.Add("class", "thead-dark");
                    // cells[1].Attributes.Add("data-breakpoints", "all");
                    cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[3].Attributes.Add("data-breakpoints", "xs sm");
                    cells[4].Attributes.Add("data-breakpoints", "xs sm");
                    //  cells[5].Attributes.Add("data-breakpoints", "all");
                    cells[6].Attributes.Add("data-breakpoints", "all");
                    cells[7].Attributes.Add("data-breakpoints", "all");
                    cells[8].Attributes.Add("data-breakpoints", "all");
                    cells[9].Attributes.Add("data-breakpoints", "all");
                    cells[10].Attributes.Add("data-breakpoints", "all");
                 //   cells[11].Attributes.Add("data-breakpoints", "all");
                    //cells[26].Attributes.Add("data-breakpoints", "all");//xs sm md
                    gv_ConsumoAlimentos.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

        }
        public void fillInventarioGalera(int IDGalera)
        {
            InventarioGaleras_obj iGalera = new InventarioGaleras_obj();
            Tbl_InventarioGalera inventario = iGalera.get_InventarioGalera(IDGalera);
            //------------------------------------
            Session["ID_LoteProduccion"] = inventario.ID_InventarioBroilers;
            lbl_totalPolloIngresado.Text = string.Format("{0:n}", inventario.TotalIngreso) + " Pollitos";
            lbl_TotalMortalidad.Text = string.Format("{0:n}", inventario.TotalMortalidad) + " Aves";
            lbl_TotalRemisiones.Text = string.Format("{0:n}", inventario.TotalSalidas_RemisionesMatadero) + " Aves";
            lbl_TotalPolloEnPie.Text = string.Format("{0:n}", inventario.TotalEnPie) + " Aves";
            lbl_EdadDias.Text = string.Format("{0:n}", inventario.EdadLote_Dias) + " Días";
            lbl_EdadSemanadas.Text = string.Format("{0:n}", ((double)inventario.EdadLote_Dias / 7)) + " Semanas";
            lbl_PesoPromedio.Text = string.Format("{0:n}", ((double)inventario.PesoPromedio / 453.59)) + " Libras  (" + inventario.PesoPromedio + "gr)";
            lbl_PorcentajeMortalidad.Text = string.Format("{0:n}", ((double)inventario.TotalMortalidad / (double)inventario.TotalIngreso) * 100) + " %";
            lbl_LibrasPesoMatadero.Text = string.Format("{0:n}", inventario.TotalLibrasPesoVivoMatanza) + " Libras";
            lbl_FechaIngresoAGalera.Text = string.Format("{0:D}", inventario.Fecha_IngresoGalera);
        }

        public void fillParametros()
        {
            if (Session["ID_LoteProduccion"] != null)
            {
                int ID_LoteProduccion = (int)Session["ID_LoteProduccion"];
                ParametrosDiarios_obj parametrosDiarios = new ParametrosDiarios_obj();
                gv_Parametros.DataSource = parametrosDiarios.getParametrosDiarios(ID_LoteProduccion);
                gv_Parametros.DataBind();
                if (gv_Parametros.Rows.Count > 0)
                {
                    this.gv_Parametros.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_Parametros.HeaderRow.Cells;
                    gv_Parametros.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    gv_Parametros.HeaderRow.Attributes.Add("class", "thead-dark");
                    cells[1].Attributes.Add("data-breakpoints", "all");
                    //cells[2].Attributes.Add("data-breakpoints", "xs sm");
                    //cells[3].Attributes.Add("data-breakpoints", "xs sm");
                    cells[4].Attributes.Add("data-breakpoints", "xs sm");
                    cells[5].Attributes.Add("data-breakpoints", "all");
                    cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[7].Attributes.Add("data-breakpoints", "all");
                    //cells[8].Attributes.Add("data-breakpoints", "all");
                    //cells[9].Attributes.Add("data-breakpoints", "all");
                    //cells[9].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[10].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[26].Attributes.Add("data-breakpoints", "all");//xs sm md
                    gv_Parametros.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
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
        public Tbl_Galeras getGaleraSeleccionada(int IdGalera)
        {
            return new ControlGaleras().get_GalerasxGranjaEnProduccion(IdGalera);
        }


        protected void btn_Continuar_Click(object sender, EventArgs e)
        {
            if (Session["ID_ParametroDiario_Anular"] != null)
            {
                int IDParametro = (int)Session["ID_ParametroDiario_Anular"];
                string result = new ParametrosDiarios_obj().EliminarParametro(IDParametro);
                if (result.Equals("1"))
                {
                    fillParametros();
                    Session["ID_ParametroDiario_Anular"] = null;
                    int IDGalera = (int)Session["IdGalera"];
                    fillInventarioGalera(IDGalera);
                    mensaje("El parametro fue eliminado correctamente!", tiposAdvertencias.exito);
                }
                else
                {
                    mensaje("Ocurrio un error y no se pudo Eliminar el parametro, Error " + result, tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("Ha ocurrido un error, intentelo nuevamente!", tiposAdvertencias.error);
            }
        }

        protected void gv_Parametros_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            gv_Parametros.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int ID_ParametroDiario = Convert.ToInt32(gv_Parametros.SelectedValue);
            Session["ID_ParametroDiario_Anular"] = ID_ParametroDiario;
            if (e.CommandName.Equals("cmd_anular"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal_Advertencia", "ShowModalAdvertencia();", true);
            }
            else if (e.CommandName.Equals("cmd_actualizar"))
            {
                RegistroConsumoAlimentos_obj registroComsumoAlimentos = new RegistroConsumoAlimentos_obj();
                txt_pesoPromedioUp.Text = registroComsumoAlimentos.getPesoGramos(ID_ParametroDiario);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal_modalpesos_Prom", "ShowModal_modalpesosProm();", true);
            }
        }

        protected void gv_ConsumoAlimentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_anular"))
            {
                gv_ConsumoAlimentos.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int ID_ControlAlimenticio = Convert.ToInt32(gv_ConsumoAlimentos.SelectedValue);
                Session["ID_ControlAlimenticio_Anular"] = ID_ControlAlimenticio;
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal_AdvertenciaCA", "ShowModalAdvertenciaCA();", true);
            }
        }

        protected void btn_ControlAlimento_Click(object sender, EventArgs e)
        {
            if (Session["ID_ControlAlimenticio_Anular"] != null)
            {
                int ID_ControlAlimento = (int)Session["ID_ControlAlimenticio_Anular"];
                string result = new RegistroConsumoAlimentos_obj().eliminarRegistro(ID_ControlAlimento);
                if (result.Equals("1"))
                {
                    Session["ID_ControlAlimenticio_Anular"] = null;
                    fillParametros();
                    filConsumoAlimentos();
                    mensaje("El Registro de alimento fue eliminado correctamente!", tiposAdvertencias.exito);
                }
                else
                {
                    mensaje("Ocurrio un error y no se pudo Eliminar el Registro de Alimento, Error " + result, tiposAdvertencias.error);
                }
            }
            else
            {
                mensaje("Ha ocurrido un error, intentelo nuevamente!", tiposAdvertencias.error);
            }

        }

        protected void btn_ActualizarPesoProm_Click(object sender, EventArgs e)
        {
            if (Session["ID_ParametroDiario_Anular"] != null)
            {
                int ID_ParametroDiario = (int)Session["ID_ParametroDiario_Anular"];
                if (txt_pesoPromedioUp.Text.Length > 0)
                {
                    decimal pesoPromedio = decimal.Parse(txt_pesoPromedioUp.Text);
                    string result = new ParametrosDiarios_obj().ActualizarPesoPromedio(ID_ParametroDiario, pesoPromedio, chk_actualizarLote.Checked);
                    if (result.Equals("1"))
                    {
                        Session["ID_ParametroDiario_Anular"] = null;
                        fillParametros();
                        filConsumoAlimentos();
                        mensaje("El Registro de alimento fue actualizado correctamente!", tiposAdvertencias.exito);
                    }
                    else
                    {
                        mensaje("Ocurrio un error y no se pudo actualizar el Registro de Alimento, Error " + result, tiposAdvertencias.error);
                    }
                }
                else
                {
                    mensaje("No puede dejar vacio el campo de peso promedio", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                mensaje("Ha ocurrido un error, intentelo nuevamente!", tiposAdvertencias.error);
            }
        }
    }
}