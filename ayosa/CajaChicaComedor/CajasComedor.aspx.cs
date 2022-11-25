using POULTRY.Reportes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.CajaChicaComedor
{
    public partial class CajasComedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                todaslasCajas();
            }
            else
            {
                try
                {
                    int value = int.Parse(lista_Opciones.SelectedValue);
                    if (value == 2)
                    {
                        fechaDiv.Visible = true;
                    }
                    else
                    {
                        fechaDiv.Visible = false;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        protected void btn_recargarRepo_Click(object sender, EventArgs e)
        {
            try
            {
                int value = int.Parse(lista_Opciones.SelectedValue);
                if (value == 1)
                {
                    todaslasCajas();
                }
                else
                {
                    string dt = Request.Form[txt_fechaRegistro.UniqueID];
                    //string dateString = txt_fechaRegistro.Text;
                    string dateString = dt;
                    var cultureInfo = new CultureInfo("es-ES");  
                    DateTime fechaUsuario = DateTime.Parse(dateString, cultureInfo);
                    if (dt.Length > 0)
                    {
                        txt_fechaRegistro.Text = dt;                                              
                        CajaxFecha(fechaUsuario);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "mensajealerta", " <script type = 'text/javascript>function fun() {alert ('Seleccione la fecha para el reporte');}</script> ", true);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void todaslasCajas()
        {
            TodasLasCajas caja = new TodasLasCajas();
            ASPxDocumentViewer1.Report = caja;
            ASPxDocumentViewer1.ReportTypeName = "POULTRY.Reportes.TodasLasCajas";
            ASPxDocumentViewer1.DataBind();
        }
        public void CajaxFecha(DateTime fecha)
        {
            CajaDelDia caja = new CajaDelDia();
            // LOS PARAMETROS TIENE CASO SENSITIVE, RESPONDEN A MAYUSCULAS O MINUSCULAS
            caja.Parameters["Fecha"].Value = fecha;
            ASPxDocumentViewer1.Report = caja;
            ASPxDocumentViewer1.ReportTypeName = "POULTRY.Reportes.CajaDelDia";

            ASPxDocumentViewer1.DataBind();
        }
    }
}