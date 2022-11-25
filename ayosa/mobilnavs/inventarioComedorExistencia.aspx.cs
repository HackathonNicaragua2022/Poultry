using accesoDatos;
using accesoDatos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY.mobilnavs
{
    public partial class inventarioComedorExistencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //***********************************                        
            try
            {
                tbl_trabajadores trabajador = new Tbl_Trabajador_obj().findById((int)Session["IDTrabajador"]);
                System.Console.WriteLine(trabajador.Nombre_1);
            }
            catch (Exception)
            {
              //  Response.Redirect("/mobilnavs/loginUsuariosComedor.aspx");
            }
            //***********************************  
            if (!IsPostBack)
            {
                LlenarCategorias();
            }
            CargarDatosInventario();
        }
        public void CargarDatosInventario()
        {
            Tbl_Inventario_obj invetario = new Tbl_Inventario_obj();
            int IDCategoria = 0;
            if (dr_Categorias.Items.Count > 0)
            {
                if (dr_Categorias.SelectedIndex != 0)
                {
                    IDCategoria = int.Parse(dr_Categorias.SelectedValue);
                }
            }
            gv_invetario.DataSource = invetario.getAllInventarioConExistencia(true, IDCategoria);
            gv_invetario.DataBind();
            if (gv_invetario.Rows.Count > 0)
            {
                this.gv_invetario.UseAccessibleHeader = true;
                TableCellCollection cells = gv_invetario.HeaderRow.Cells;


                gv_invetario.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //  cells[1].Attributes.Add("data-breakpoints", "all");
                cells[2].Attributes.Add("data-breakpoints", "xs sm md");
                //   cells[6].Attributes.Add("data-breakpoints", "xs sm md");
                //cells[4].Attributes.Add("data-breakpoints", "xs sm md");           
                gv_invetario.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        private void LlenarCategorias()
        {
            Cat_CategoriaProducto_obj categoriaP = new Cat_CategoriaProducto_obj();
            dr_Categorias.DataSource = categoriaP.getAllCategoriaProducto();
            dr_Categorias.DataTextField = "NombreCategoriaProducto";
            dr_Categorias.DataValueField = "ID_CategoriaProducto";
            dr_Categorias.DataBind();
            dr_Categorias.Items.Insert(0, "MOSTRAR TODO");
        }
        protected void dr_Categorias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}