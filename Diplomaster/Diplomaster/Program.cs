using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Collections;
using System.IO;

namespace Diplomaster
{

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
            SqlCeEngine engine = new SqlCeEngine(Global.ConnectionString);
            if (engine.Verify()) {
                Application.Run(new FormStart());
            } else {
                MessageBox.Show("Database is corrupted.");
                engine.Repair(null, RepairOption.RecoverAllPossibleRows);
            }
        }
    }

    public class Global
    {
        public static Color ColorDefault = SystemColors.Window;
        public static Color ColorInactive = SystemColors.InactiveCaption;
        public static Color ColorReadOnly = SystemColors.Control;
        public static Color ColorError = Color.LightCoral;
        public static Color ColorTextNormal = SystemColors.WindowText;
        //public static Color ColorTextFile = Color.Red;
        public static DateTime MinDate = new DateTime(1753,1,1);
        public static string ConnectionString = @"Data Source = ..\..\Database.sdf";
    }

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
            else if (Obj.GetType() == typeof(DateTimePicker)) {
                if (Obj2 == null) {
                    if (res = ((DateTimePicker)Obj).Value != Global.MinDate)
                        label.ResetBackColor();
                    else
                        label.BackColor = Global.ColorError;
                } else {
                    if (Obj2.GetType() == typeof(DateTimePicker)) {
                        if (res = (((DateTimePicker)Obj).Value != Global.MinDate && ((DateTimePicker)Obj).Value >= ((DateTimePicker)Obj2).Value))
                            label.ResetBackColor();
                        else
                            label.BackColor = Global.ColorError;
                    }
                }
            }

            return res;
        }

        static public bool Apply(Control label, Control Obj, Type type, bool cannull = false) {
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
                res = str=="" || File.Exists(str);

            if (res)
                label.ResetBackColor();
            else
                label.BackColor = Global.ColorError;

            return res;
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
            Regex regex = new Regex(@"^-?(([1-9]\d*)|0)(\.\d+)?$");
            return regex.Match(strim).Success;
        }
        static public bool Decimal(string str, bool cannull = false)
        {
            string strim = str.Trim();
            if (cannull && strim == "")
                return true;
            Regex regex = new Regex(@"^-?(([1-9]\d*)|0)(\.\d{1,2})?$");
            return regex.Match(strim).Success;
        }
        
        /*
        static public bool Null(string str)
        {
            return str == string.Empty;
        }
        */
    }

    public static class Extensions
    {
        public static string ToString2(this Array target)
        {
            bool check = false;
            string str = "[";
            foreach (var item in target) {
                str += item.ToString() + ", ";
                check = true;
            }
            if (check) {
                str = str.Remove(str.Length - 2);
            }
            str += "]";
            return str;
        }

        public static bool isnull(this object target)
        {
            return target == null || target.GetType() == typeof(System.DBNull);
        }

    }
}

