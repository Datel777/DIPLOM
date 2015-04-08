using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Diplomaster
{
    static class Fill
    {
        public static void TextBox(TextBox textbox, object val)
        {
            if (val.isnull())
                textbox.Text = "";
            else
                textbox.Text = (string)val;
        }

        public static void TextBoxInt(TextBox textbox, object val)
        {
            if (val.isnull())
                textbox.Text = "";
            else
                textbox.Text = Convert.ToString((int)val);
        }

        public static void TextBoxFloat(TextBox textbox, object val)
        {
            if (val.isnull())
                textbox.Text = "";
            else
                textbox.Text = Convert.ToString((float)Convert.ToDecimal(val));
        }

        public static void TextBoxDecimal(TextBox textbox, object val)
        {
            if (val.isnull())
                textbox.Text = "";
            else
                textbox.Text = Convert.ToString((decimal)val);
        }

        public static void DateTimePicker(DateTimePicker picker, object val)
        {
            if (val.isnull())
            {
                picker.Value = Global.MinDate;
                picker.Format = DateTimePickerFormat.Custom;
                picker.CustomFormat = " ";
            }
            else
                picker.Value = (DateTime)val;
        }
    }
}
