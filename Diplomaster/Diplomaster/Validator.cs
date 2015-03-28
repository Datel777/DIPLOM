using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Diplomaster
{
    static class Validator
    {
        static public bool Apply(Control label, Control Obj, Control Obj2 = null)
        {
            bool res = false;

            if (Obj.GetType() == typeof(ComboBox))
                if (res = ((ComboBox)Obj).SelectedIndex != -1)
                    label.ResetBackColor();
                else
                    label.BackColor = Global.ColorError;
            else if (Obj.GetType() == typeof(DateTimePicker))
            {
                if (Obj2 == null)
                {
                    if (res = ((DateTimePicker)Obj).Value != Global.MinDate)
                        label.ResetBackColor();
                    else
                        label.BackColor = Global.ColorError;
                }
                else
                {
                    if (Obj2.GetType() == typeof(DateTimePicker))
                    {
                        if (res = (((DateTimePicker)Obj).Value != Global.MinDate && ((DateTimePicker)Obj).Value >= ((DateTimePicker)Obj2).Value))
                            label.ResetBackColor();
                        else
                            label.BackColor = Global.ColorError;
                    }
                }
            }

            return res;
        }

        static public bool Apply(Control label, Control Obj, Type type, bool cannull = false)
        {
            bool res = false;
            string str = null;

            if (Obj.GetType() == typeof(TextBox))
                str = Obj.Text;


            if (type == typeof(int))
                res = Validator.Int(str, cannull);
            else if (type == typeof(uint))
                res = Validator.UInt(str, cannull);
            else if (type == typeof(float))
                res = Validator.Float(str, cannull);
            else if (type == typeof(decimal))
                res = Validator.Decimal(str, cannull);
            else if (type == typeof(File))
                res = str == "" || File.Exists(str);
            else if (type == typeof(string))
                res = Validator.Filename(str, cannull);

            if (res)
                label.ResetBackColor();
            else
                label.BackColor = Global.ColorError;

            return res;
        }

        static public bool Apply(Control label, Control Obj, bool bull)
        {
            if (bull)
                label.ResetBackColor();
            else
                label.BackColor = Global.ColorError;

            return bull;
        }


        static public bool UInt(string str, bool cannull = false)
        {
            string strim = str.Trim();
            if (cannull && strim == "")
                return true;
            Regex regex = new Regex(@"^(([1-9]\d*)|0)$");//(([1-9]\d*)|0)
            return regex.Match(strim).Success;
        }

        static public bool Int(string str, bool cannull = false)
        {
            string strim = str.Trim();
            if (cannull && strim == "")
                return true;
            Regex regex = new Regex(@"^-?([1-9]\d*)|0$");
            return regex.Match(strim).Success;
        }
        static public bool Float(string str, bool cannull = false)
        {
            string strim = str.Trim();
            if (cannull && strim == "")
                return true;
            Regex regex = new Regex(@"^-?(([1-9]\d*)|0)(,\d+)?$");
            return regex.Match(strim).Success;
        }
        static public bool Decimal(string str, bool cannull = false)
        {
            string strim = str.Trim();
            if (cannull && strim == "")
                return true;
            Regex regex = new Regex(@"^-?(([1-9]\d*)|0)(,\d{1,4})?$");
            return regex.Match(strim).Success;
        }

        static public bool Filename(string str, bool cannull = false)
        {
            //string strim = str.Trim();
            if (cannull && str == "")
                return true;
            Regex regex = new Regex("^(.*(?!(\\|/|:|\\*|\\?|\\\"|<|>|\\|)).*)$");
            return regex.Match(str).Success;
        }

        /*
        static public bool Null(string str)
        {
            return str == string.Empty;
        }
        */
    }
}
