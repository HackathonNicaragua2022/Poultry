using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ultil
{
    public class setCssEstadoTextbox
    {
        private TextBox textbox;
        public setCssEstadoTextbox(TextBox text)
        {
            this.textbox = text;
        }
        public void setValid_FormControl()
        {
            this.textbox.Attributes["class"] = "form-control is-valid";
        }
        public void setInValid_FormControl()
        {
            this.textbox.Attributes["class"] = "form-control is-invalid";
        }
        public void setFormControl()
        {
            this.textbox.Attributes["class"] = "form-control";
        }
    }      
}
