using accesoDatos;
using accesoDatos.Granja;
using accesoDatos.ProduccionPollos.ControlLotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.GranjaSanFrancisco
{
    public partial class resumenLoteCrecimiento : System.Web.UI.Page
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
                lbl_fechaReporte.Text = DateTime.Now.ToShortDateString();
            }
        }
        public void fillInventarioGalera(int IDGalera)
        {
            InventarioGaleras_obj iGalera = new InventarioGaleras_obj();
            Tbl_InventarioGalera inventario = iGalera.get_InventarioGalera(IDGalera);
            //------------------------------------
            Session["ID_LoteProduccion"] = inventario.ID_InventarioBroilers;
            lbl_codigoLote.Text = inventario.Tbl_IngresoLotes.CodLote;
            lbl_totalPolloIngresado.Text = string.Format("{0:n}", inventario.TotalIngreso) + " Pollitos";
            lbl_TotalMortalidad.Text = string.Format("{0:n}", inventario.TotalMortalidad) + " Aves";
            lbl_TotalRemisiones.Text = string.Format("{0:n}", inventario.TotalSalidas_RemisionesMatadero) + " Aves";
            //lbl_TotalPolloEnPie.Text = string.Format("{0:n}", inventario.TotalEnPie) + " Aves";
            //lbl_EdadDias.Text = string.Format("{0:n}", inventario.EdadLote_Dias) + " Días";
            //lbl_EdadSemanadas.Text = string.Format("{0:n}", ((double)inventario.EdadLote_Dias / 7)) + " Semanas";
            //lbl_PesoPromedio.Text = string.Format("{0:n}", ((double)inventario.PesoPromedio / 453.59)) + " Libras  (" + inventario.PesoPromedio + "gr)";
            //lbl_PorcentajeMortalidad.Text = string.Format("{0:n}", ((double)inventario.TotalMortalidad / (double)inventario.TotalIngreso) * 100) + " %";
            //lbl_LibrasPesoMatadero.Text = string.Format("{0:n}", inventario.TotalLibrasPesoVivoMatanza) + " Libras";
            //lbl_FechaIngresoAGalera.Text = string.Format("{0:D}", inventario.Fecha_IngresoGalera);
        }
        public void fillParametros()
        {
            if (Session["ID_LoteProduccion"] != null)
            {
                int ID_LoteProduccion = (int)Session["ID_LoteProduccion"];
                ParametrosDiarios_obj parametrosDiarios = new ParametrosDiarios_obj();
            }
        }

        public void modal_mensaje(string titulo, string mensaje, tiposAdvertencias alerta)
        {
            string tipoMensaje = "";
            switch (alerta)
            {
                case tiposAdvertencias.informacion:
                    tipoMensaje = "success";
                    break;
                case tiposAdvertencias.exito:
                    tipoMensaje = "info";
                    break;
                case tiposAdvertencias.advertencia:
                    tipoMensaje = "warning";
                    break;
                case tiposAdvertencias.error:
                    tipoMensaje = "error";
                    break;
                case tiposAdvertencias.pregunta:
                    tipoMensaje = "question";
                    break;
                default:
                    break;
            }
            ScriptManager.RegisterStartupScript(up_principal, GetType(), "modalAlert", "modal_alert('" + titulo + "','" + mensaje + "','" + tipoMensaje + "')", true);
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
    }
}