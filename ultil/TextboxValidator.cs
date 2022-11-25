using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ultil
{
    public class TextboxValidator
    {
        public TextboxValidator()
        {
        }
        /// <summary>
        /// Devuelte true si todos los textbox, son mayor a 0, de lo contrario devuelte True
        /// </summary>
        /// <param name="textboxes"></param>
        /// <returns></returns>
        public bool validarLargoCadena_mayorCero(params TextBox[] textboxes)
        {
            foreach (var text_item in textboxes)
            {
                if (string.IsNullOrEmpty(text_item.Text))
                {
                    new setCssEstadoTextbox(text_item).setInValid_FormControl();
                    text_item.Attributes.Add("placeholder", "Complete este Campo");
                }
                else
                {
                    new setCssEstadoTextbox(text_item).setValid_FormControl();
                }
            }
            if (textboxes.Any(a => a.Text.Length <= 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Establece en form Control los textbox
        /// </summary>
        /// <param name="textboxes"></param>
        /// <returns></returns>
        public void setFormControl(params TextBox[] textboxes)
        {
            foreach (var text_item in textboxes)
            {
                new setCssEstadoTextbox(text_item).setFormControl();
            }
        }
        /// <summary>
        /// Valida que el control textbox contiene valores mayores a 0
        /// </summary>
        /// <param name="textbox"></param>
        /// <returns></returns>
        public bool validarLargoCadena(TextBox textbox)
        {
            if (textbox.Text.Length > 0) return true;
            return false;
        }
    }
}
