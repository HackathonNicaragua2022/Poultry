using accesoDatos;
using accesoDatos.ProduccionHuevos.Clasificadora;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.ModuloClasificadoraHuevos
{
    public partial class IngresoHuevosSC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fillIngresosHuevosporFecha();
        }
        public void loadData()
        {
            try
            {
                string[] fechas = txt_rangofecha.Text.Trim().Split('-');
                CultureInfo provider = CultureInfo.CreateSpecificCulture("en-US");
                DateTime fecha1 = DateTime.Parse(fechas[0], provider);
                DateTime fecha2 = DateTime.Parse(fechas[1], provider);

                ingresoInventario_HSC ingresosInventarioHsc = new ingresoInventario_HSC();
                Session["LstaIngresos"] = ingresosInventarioHsc.getAllIngresoHSCxFecha(fecha1, fecha2);
                fillIngresosHuevosporFecha();
            }
            catch (Exception ex)
            {
                mensaje("Error cargando los registros de ingreso " + ex.Message, tiposAdvertencias.error);
            }
        }
        public void fillIngresosHuevosporFecha()
        {
            if (Session["LstaIngresos"] != null)
            {
                List<accesoDatos.ProduccionHuevos.Clasificadora.ingresoInventario_HSC.ingresoInventarioHCSObj> datos = (List<accesoDatos.ProduccionHuevos.Clasificadora.ingresoInventario_HSC.ingresoInventarioHCSObj>)Session["LstaIngresos"];
                rp_IngresosPorFecha.DataSource = datos;
                rp_IngresosPorFecha.DataBind();
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

        protected void rp_IngresosPorFecha_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("cmd_Detalle"))
            {
                if (Session["LstaIngresos"] != null)
                {
                    List<accesoDatos.ProduccionHuevos.Clasificadora.ingresoInventario_HSC.ingresoInventarioHCSObj> datos = (List<accesoDatos.ProduccionHuevos.Clasificadora.ingresoInventario_HSC.ingresoInventarioHCSObj>)Session["LstaIngresos"];
                    int ID_Ingreso = int.Parse(e.CommandArgument.ToString());
                    bool mostrar = datos.Where(a => a.ID_IngresoInventario == ID_Ingreso).FirstOrDefault().MostrarDetalle;
                    if (mostrar)
                    {
                        datos.Where(a => a.ID_IngresoInventario == ID_Ingreso).FirstOrDefault().MostrarDetalle = false;
                    }
                    else
                    {
                        datos.Where(a => a.ID_IngresoInventario == ID_Ingreso).FirstOrDefault().MostrarDetalle = true;
                    }
                    fillIngresosHuevosporFecha();
                }
            }
        }

        protected void rp_IngresosPorFecha_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater detalle = (Repeater)e.Item.FindControl("rp_IngresosPorFecha");
                HiddenField ID_IngresoHSC_HF = (HiddenField)e.Item.FindControl("id_Ingreso_HF");

                int ID_Ingreso = int.Parse(ID_IngresoHSC_HF.Value);

                DetalleIngresoHSC_obj detalleIngreso = new DetalleIngresoHSC_obj();
                detalle.DataSource = detalleIngreso.getAllDetallesXIDIngreso(ID_Ingreso);
                detalle.DataBind();

                //---------------------------------------------------------------
                //LinkButton link_Detalle = (LinkButton)e.Item.FindControl("link_Detalles");
                //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(link_Detalle);
            }
            catch (Exception)
            {
                //La primer fila no contiene tablas ni elementos ocultos por tanto nos genera error
            }
        }

        protected void btn_CargarIngresos_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}