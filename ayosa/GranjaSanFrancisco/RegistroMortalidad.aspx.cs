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
    public partial class RegistroMortalidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadLotesPRoduccion();
            }
            fillLotesProduccion();
        }
        public void loadLotesPRoduccion()
        {
            InventarioGaleras_obj inventarioGaleras = new InventarioGaleras_obj();
            Session["ListaLotesEnProduccion"] = inventarioGaleras.getInventariosRel();
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
        protected void btn_guardarParametros_Click(object sender, EventArgs e)
        {
            if (Session["LoteEditar"] != null)
            {
                if (txt_FechaRegistro.Text.Length > 0)
                {
                    if (new TextboxValidator().validarLargoCadena_mayorCero(txt_Mortalidad, txt_PesoPromedio, txt_ComentarioAdicionales))
                    {
                        CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                        DateTime fechaRegistro = DateTime.Parse(txt_FechaRegistro.Text, provider);
                        int mortalidad = int.Parse(txt_Mortalidad.Text);
                        decimal pesoPromedio = decimal.Parse(txt_PesoPromedio.Text);

                        var membershipUser = System.Web.Security.Membership.GetUser();
                        Guid ingresadoPor = (Guid)membershipUser.ProviderUserKey;

                        string comentarios = txt_ComentarioAdicionales.Text;

                        Tbl_ParametrosDiarios nuevosParametros = new Tbl_ParametrosDiarios();
                        nuevosParametros.ID_InventarioBroilers = (int)Session["LoteEditar"];
                        nuevosParametros.FechaRegistro = fechaRegistro;
                        nuevosParametros.Mortalidad = mortalidad;
                        nuevosParametros.Peso_Promedio = pesoPromedio;
                        nuevosParametros.Uniformidad = 0;
                        nuevosParametros.RegistradoPor = ingresadoPor;
                        nuevosParametros.ComentariosAdicionales = comentarios;
                        nuevosParametros.URLImagenMortalidad = "";

                        string result = new ParametrosDiarios_obj().nuevoParametro(nuevosParametros);
                        if (result.Equals("1"))
                        {
                            txt_FechaRegistro.Text = "";
                            txt_Mortalidad.Text = "";
                            txt_PesoPromedio.Text = "";
                            txt_ComentarioAdicionales.Text = "";
                            Session["LoteEditar"] = null;
                            loadLotesPRoduccion();
                            fillLotesProduccion();
                            new TextboxValidator().setFormControl(txt_Mortalidad, txt_PesoPromedio, txt_ComentarioAdicionales);
                            mensaje("El nuevo parametro se ha guardado correctamente!", tiposAdvertencias.exito);
                        }
                        else
                        {
                            mensaje("Ocurrio un error y no se pudo ingresar el nuevo parametro, Error " + result, tiposAdvertencias.error);
                        }
                    }
                    else
                    {
                        mensaje("Por favor complete los campos marcados, para continuar", tiposAdvertencias.advertencia);
                    }
                }
                else
                {
                    mensaje("La fecha es necesaria, por favor verifiquela primero", tiposAdvertencias.advertencia);
                }
            }
            else
            {
                mensaje("Seleccione un lote en produccion para ingresar el parametro", tiposAdvertencias.advertencia);
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

                Session["LoteEditar"] = IDSeleccionado;
                fillLotesProduccion();
                mensaje("Se ha Seleccionado el Lote numero: " + (Convert.ToInt32(e.CommandArgument) + 1), tiposAdvertencias.informacion);
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
    }
}