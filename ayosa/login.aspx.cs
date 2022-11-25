using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POULTRY 
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.GC.Collect();//limpiar memoria ram
            }
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (User.Identity.IsAuthenticated)
            //    {
            //        if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            //        {
            Response.Redirect("~/Admin-gerencial/home-admin.aspx");
            //        }
            //    }

            //}
            //try
            //{

            //    if (Roles.IsUserInRole(Login1.UserName, "Admin") || Roles.IsUserInRole(Login1.UserName, "CEO"))
            //    //if (Roles.IsUserInRole(User.Identity.Name, "Admin"))
            //    {
            //        if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]) && !(!Request.QueryString["ReturnUrl"].Equals("%2f") || !Request.QueryString["ReturnUrl"].Equals("/")))
            //        {
            //            Response.Redirect(Request.QueryString["ReturnUrl"]);
            //        }
            //        else
            //        {
            //            Response.Redirect("Admin-gerencial/home-admin.aspx");
            //        }
            //    }
            //    else if (Roles.IsUserInRole(Login1.UserName, "ENCARGADOCLASIFICADORAHUEVO"))
            //    {
            //        if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]) && !(!Request.QueryString["ReturnUrl"].Equals("%2f") || !Request.QueryString["ReturnUrl"].Equals("/")))
            //        {
            //            Response.Redirect(Request.QueryString["ReturnUrl"]);
            //        }
            //        else
            //        {
            //            Response.Redirect("~/ModuloClasificadoraHuevos/homebodegahuevo.aspx");
            //        }
            //    }
            //    else if (Roles.IsUserInRole(Login1.UserName, "EncargadoGranja"))
            //    {
            //        if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]) && !(!Request.QueryString["ReturnUrl"].Equals("%2f") || !Request.QueryString["ReturnUrl"].Equals("/")))
            //        {
            //            Response.Redirect(Request.QueryString["ReturnUrl"]);
            //        }
            //        else
            //        {
            //            Response.Redirect("~/GranjaSanFrancisco/homeGranja.aspx");
            //        }
            //    }
            //    else
            //    {
            //        Response.Redirect("~/publicas/index.aspx");
            //    }

            //}
            //catch (HttpException)
            //{
            //    return;
            //}
        }
    }
}