using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace ultil
{
    public static class Boostrap4Utill
    {
        public static string getBloc_Alert(TiposAdvertenciasBoostrap tipoAvertencia, string Titulo, string Mensanje)
        {
            string blok = "";
            switch (tipoAvertencia)
            {
                case TiposAdvertenciasBoostrap.INFORMACION:
                    blok = "<div class=\"alert alert-info alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"icon fas fa-info\"></i> " + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                case TiposAdvertenciasBoostrap.EXITO:
                    blok = "<div class=\"alert alert-success alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"icon fas fa-check\"></i> " + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                case TiposAdvertenciasBoostrap.ADVERTENCIA:
                    blok = "<div class=\"alert alert-warning alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"icon fas fa-exclamation-triangle\"></i>" + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                case TiposAdvertenciasBoostrap.ERROR:
                    blok = "<div class=\"alert alert-danger alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"icon fas fa-ban\"></i>" + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                case TiposAdvertenciasBoostrap.PREGUNTA:
                    blok = "<div class=\"alert bg-navy alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"fa fa-question-circle\"></i>" + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                default:
                    break;
            }
            return blok;
        }
        public static void setBloc_Alert(TiposAdvertenciasBoostrap tipoAvertencia, string Titulo, string Mensanje, HtmlGenericControl control)
        {
            string blok = "";
            switch (tipoAvertencia)
            {
                case TiposAdvertenciasBoostrap.INFORMACION:
                    blok = "<div class=\"alert alert-info alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"icon fas fa-info\"></i> " + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                case TiposAdvertenciasBoostrap.EXITO:
                    blok = "<div class=\"alert alert-success alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"icon fas fa-check\"></i> " + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                case TiposAdvertenciasBoostrap.ADVERTENCIA:
                    blok = "<div class=\"alert alert-warning alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"icon fas fa-exclamation-triangle\"></i>" + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                case TiposAdvertenciasBoostrap.ERROR:
                    blok = "<div class=\"alert alert-danger alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"icon fas fa-ban\"></i>" + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                case TiposAdvertenciasBoostrap.PREGUNTA:
                    blok = "<div class=\"alert bg-navy alert-dismissible\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><h5><i class=\"fa fa-question-circle\"></i>" + Titulo + "</h5><marquee class=\"news-content\">" + Mensanje + "</marquee></div>";
                    break;
                default:
                    break;
            }
            control.InnerHtml = blok;
        }
    }
}
