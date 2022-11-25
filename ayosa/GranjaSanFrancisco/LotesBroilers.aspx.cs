using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoDatos.ProduccionPollos.Configuracion;
using accesoDatos.ProduccionPollos.ControlLotes;
namespace POULTRY.LOTES
{
    public partial class LotesBroilers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillGranja();

            }
        }
        /// <summary>
        /// Carga Todas las Granjas del Sistema en la lista desplegable
        /// </summary>
        public void fillGranja()
        {
            dr_GranjaSeleccionada.DataSource = new Tbl_Granjas_obj().getAllGranjas_Rel();
            dr_GranjaSeleccionada.DataTextField = "Nombre";
            dr_GranjaSeleccionada.DataValueField = "ID_Granjas";
            dr_GranjaSeleccionada.DataBind();
            fillGalerasxGranja();
        }
        public void fillGalerasxGranja()
        {

            if (dr_GranjaSeleccionada.Items.Count > 0)
            {
                int idGranja = int.Parse(dr_GranjaSeleccionada.SelectedValue.ToString());
                ControlGaleras cGaleras = new ControlGaleras();
                List<accesoDatos.ProduccionPollos.ControlLotes.ControlGaleras.Tbl_gelera_vista> result = cGaleras.getAllGalerasxGranjaSinProduccion(idGranja, true);
                lbl_totalPolloEnPie.Text = "  " + string.Format("{0:n}", result.Sum(a => a.PolloEnPie)) + " Aves";
                R_Galeras.DataSource = result;
                R_Galeras.DataBind();
                if (result.Count > 0)
                {
                    id_galerasSinProduccion.Visible = false;
                }
                else
                {
                    id_galerasSinProduccion.Visible = true;
                }
            }

        }

        protected void R_Galeras_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idGalera = int.Parse(e.CommandArgument.ToString());
            System.Console.WriteLine("ID Galera Seleccionada " + idGalera);
            if (e.CommandName == "LoteActivo")
            {
                /*
                   string col1 = ((TextBox)e.Item.FindControl("col1")).Text;
            string col2 = ((TextBox)e.Item.FindControl("col2")).Text;


            string allKeys = Convert.ToString(e.CommandArgument);

            string[] arrKeys = new string[2];
            char[] splitter = { '|' };
            arrKeys = allKeys.Split(splitter);                
                 */
                //REdirecciona a una nueva pagina de inventario con el valor de la galera seleccionada
              
                Response.Redirect("InventarioLote.aspx?idgalera=" + idGalera, true);
            }
            if (e.CommandName == "VerActivo")
            {
                Response.Redirect("resumenLoteCrecimiento.aspx?idgalera=" + idGalera, true);
            }
        }

        protected void dr_GranjaSeleccionada_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGalerasxGranja();
        }
    }
}