using accesoDatos;
using accesoDatos.Comedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class menuComedorTrabajadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //***********************************                        
            try
            {
                tbl_trabajadores trabajador = new Tbl_Trabajador_obj().findById((int)Session["IDTrabajador"]);
                lbl_Usuario.Text = trabajador.Nombre_1;
                lbl_nombreUsuario.Text = trabajador.Nombre_1;
                lbl_Cargo.Text = trabajador.Cat_Cargo.NombreCargo;


                //********* VERIFICAR EL MENU DEL DIA
                MenuDelDia_Obj menu = new MenuDelDia_Obj();
                Cat_MenuDia menuDelDia = menu.getMenuDeldia();
                if (menuDelDia != null)
                {
                    lbl_Desayuno.Text = menuDelDia.Desayuno;
                    lbl_Almuerzo.Text = menuDelDia.Almuerzo;
                    lbl_Cena.Text = menuDelDia.Cena;
                }
                else
                {
                    lbl_Desayuno.Text = "Esperando..";
                    lbl_Almuerzo.Text = "Esperando..";
                    lbl_Cena.Text = "Esperando..";
                }
                //----------------------------------------------
                // TOTALES
                Tbl_FaturaComedor_obj facturas = new Tbl_FaturaComedor_obj();
                lbl_TotalFacturaContado.Text = String.Format("{0:N}", facturas.getTotalFacturasContadoxTrabajador((int)Session["IDTrabajador"]));
                lbl_TotalFacturasCredito.Text = String.Format("{0:N}", facturas.getTotalFacturasCreditoxTrabajador((int)Session["IDTrabajador"]));
            }
            catch (Exception)
            {
                Response.Redirect("/mobilnavs/loginUsuariosComedor.aspx");
            }
            //***********************************                        
        }

        protected void btn_CerrarSession_Click(object sender, EventArgs e)
        {
            Session["IDTrabajador"] = -1;
            Response.Redirect("/mobilnavs/loginUsuariosComedor.aspx");
        }

        protected void txt_BuscarFactura_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session["IDFacturaFiltroComedor"] = int.Parse(txt_BuscarFactura.Text);
                Response.Redirect("/mobilnavs/ResultadoFiltroFacturaCom.aspx");
            }
            catch (Exception)
            {
            }
        }
    }
}