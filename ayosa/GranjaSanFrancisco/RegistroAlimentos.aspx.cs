using accesoDatos;
using accesoDatos.Granja;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ultil;

namespace POULTRY.GranjaSanFrancisco
{
    public partial class RegistroAlimentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["LoteEditar"] = null;
                Session["ListaLotesEnProduccion"] = null;
                loadLotesPRoduccion();
                fillTipoAlimento();
            }
            fillLotesProduccion();
            loadUltimoRegistro();
        }
        public void fillTipoAlimento()
        {
            TipoAlimentosEngorde_obj tiposAlimentos = new TipoAlimentosEngorde_obj();
            dr_TipoAlimento.DataSource = tiposAlimentos.getAll();
            dr_TipoAlimento.DataTextField = "NombreAlimento";
            dr_TipoAlimento.DataValueField = "ID_TipoAlimentos";
            dr_TipoAlimento.DataBind();
        }
        public void loadLotesPRoduccion()
        {
            InventarioGaleras_obj inventarioGaleras = new InventarioGaleras_obj();
            Session["ListaLotesEnProduccion"] = inventarioGaleras.getInventariosRel();
        }
        public void loadUltimoRegistro()
        {
            if (Session["LoteEditar"] != null)
            {
                int IDLote = (int)Session["LoteEditar"];
                RegistroConsumoAlimentos_obj RegistroConsumoAlimentos = new RegistroConsumoAlimentos_obj();
                txt_InventarioInicial.Text = RegistroConsumoAlimentos.ultimoInventarioFinal(IDLote).ToString();
            }
        }
        public void fillLotesProduccion()
        {
            if (Session["ListaLotesEnProduccion"] != null)
            {
                List<InventarioGaleras_vistaParametros> listaLP = (List<InventarioGaleras_vistaParametros>)Session["ListaLotesEnProduccion"];

                gv_LotesEnProduccion.DataSource = listaLP;
                gv_LotesEnProduccion.DataBind();
                if (gv_LotesEnProduccion.Rows.Count > 0)
                {
                    this.gv_LotesEnProduccion.UseAccessibleHeader = true;
                    TableCellCollection cells = gv_LotesEnProduccion.HeaderRow.Cells;
                    gv_LotesEnProduccion.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    //cells[1].Attributes.Add("data-breakpoints", "md");
                    cells[2].Attributes.Add("data-breakpoints", "xs sm");
                    cells[3].Attributes.Add("data-breakpoints", "xs sm");
                    cells[4].Attributes.Add("data-breakpoints", "xs sm");
                    cells[5].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[7].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[8].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[9].Attributes.Add("data-breakpoints", "xs sm md");
                    cells[10].Attributes.Add("data-breakpoints", "xs sm md");
                    //cells[26].Attributes.Add("data-breakpoints", "all");//xs sm md
                    gv_LotesEnProduccion.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        protected void gv_LotesEnProduccion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                List<InventarioGaleras_vistaParametros> listaLP = (List<InventarioGaleras_vistaParametros>)Session["ListaLotesEnProduccion"];
                if (listaLP.ElementAt(e.Row.RowIndex).Seleccionado == true)
                {
                    e.Row.Attributes.Add("class", "table-info");
                }
            }
            catch (Exception)
            {
            }
        }

        protected void gv_LotesEnProduccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Seleccionar"))
            {
                gv_LotesEnProduccion.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int IDSeleccionado = Convert.ToInt32(gv_LotesEnProduccion.SelectedValue);

                List<InventarioGaleras_vistaParametros> listaLP = (List<InventarioGaleras_vistaParametros>)Session["ListaLotesEnProduccion"];
                listaLP.ForEach(c => c.Seleccionado = false);
                listaLP.Where(a => a.ID_InventarioBroilers == IDSeleccionado).FirstOrDefault().Seleccionado = true;


                txt_QuintalesRecibidos.Text = "0";

                Session["LoteEditar"] = IDSeleccionado;
                loadUltimoRegistro();
                decimal UltimoInventario = new RegistroConsumoAlimentos_obj().ultimoInventarioFinal(IDSeleccionado);
                txt_InventarioInicial.Text = UltimoInventario.ToString();
                Session["FechaConsumoFinalAnterior"] = new RegistroConsumoAlimentos_obj().ultimaFechaInventarioFinal(IDSeleccionado);
                fillLotesProduccion();
                mensaje("Se ha Seleccionado el Lote numero: " + (Convert.ToInt32(e.CommandArgument) + 1), tiposAdvertencias.informacion);
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

        protected void btn_GuardarConsumo_Click(object sender, EventArgs e)
        {
            if (Session["LoteEditar"] != null)
            {
                if (new TextboxValidator().validarLargoCadena_mayorCero(txt_FechaRegistro, txt_InventarioInicial, txt_InventarioFinal, txt_QuintalesRecibidos))
                {
                    new TextboxValidator().setFormControl(txt_FechaRegistro, txt_InventarioInicial, txt_InventarioFinal, txt_QuintalesRecibidos);
                    if (dr_TipoAlimento.Items.Count > 0)
                    {
                        int idTipoAlimento = int.Parse(dr_TipoAlimento.SelectedItem.Value);
                        int IDLoteProduccion = (int)Session["LoteEditar"];
                        CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                        DateTime fechaRegistro = DateTime.Parse(txt_FechaRegistro.Text, provider);
                        var membershipUser = System.Web.Security.Membership.GetUser();
                        Guid ingresadoPor = (Guid)membershipUser.ProviderUserKey;
                        decimal inventarioInicial = decimal.Parse(txt_InventarioInicial.Text);
                        decimal quintalesEnviados = decimal.Parse(txt_QuintalesRecibidos.Text);
                        decimal inventarioFinal = decimal.Parse(txt_InventarioFinal.Text);
                        string comentario = txt_ComentariosAdicionales.Text;

                        DateTime fechaFinalAnterior = (DateTime)Session["FechaConsumoFinalAnterior"];

                        Tbl_ControlAlimenticio nuevoControl = new Tbl_ControlAlimenticio();
                        nuevoControl.ID_TipoAlimentos = idTipoAlimento;
                        nuevoControl.ID_InventarioBroilers = IDLoteProduccion;
                        nuevoControl.FechaRegistro = DateTime.Now;
                        nuevoControl.InventarioInicialQtl = inventarioInicial;
                        nuevoControl.QtlRecibidoDiario = quintalesEnviados;
                        nuevoControl.FechaHoraRegistroConsumo_in = fechaFinalAnterior;//Es la fecha de registro anterior para usarse como calculo de 24 en todo caso
                        nuevoControl.FechaHoraRegistroConsumo_fin = fechaRegistro;
                        nuevoControl.InventarioFinal = inventarioFinal;
                        nuevoControl.registroManual = true;
                        nuevoControl.RegistradoPor = ingresadoPor;
                        nuevoControl.Comentarios = comentario;

                        string result = new RegistroConsumoAlimentos_obj().Nuevo(nuevoControl);
                        if (result.Equals("1"))
                        {
                            Session["LoteEditar"] = null;
                            txt_FechaRegistro.Text = string.Empty;
                            txt_InventarioInicial.Text = string.Empty;
                            txt_InventarioFinal.Text = string.Empty;
                            txt_QuintalesRecibidos.Text = string.Empty;
                            txt_ComentariosAdicionales.Text = string.Empty;
                            txt_ConsumoDiario.Text = string.Empty;
                            loadUltimoRegistro();
                            mensaje("El registro de Consumo se ha ingresado correctamente!!", tiposAdvertencias.exito);
                        }
                        else
                        {
                            mensaje("No se pudo ingresar el registro debido al siguiente error: " + result, tiposAdvertencias.error);
                        }
                    }
                    else
                    {
                        mensaje("La lista de Tipo de Alimento esta Vacia, por favor revicelo he intentelo de nuevo", tiposAdvertencias.advertencia);
                    }

                }
                else
                {
                    mensaje("Por favor complete los campos marcados en Rojo", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                mensaje("Seleccione un Lote en Produccion", tiposAdvertencias.advertencia);
            }
        }
    }
}