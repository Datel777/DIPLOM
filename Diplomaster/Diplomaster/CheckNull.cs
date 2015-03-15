using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Diplomaster
{
    public class CheckNull
    {
        public static object String(string val)
        {
            if (val == "")
                return DBNull.Value;
            else
                return val;
        }
        public static object Int(string val)
        {
            if (val == "")
                return DBNull.Value;
            else
                return Convert.ToInt32(val);
        }
        //public static object UInt(string val)
        //{
        //    if (val == "")
        //        return DBNull.Value;
        //    else
        //        return Convert.ToUInt32(val);
        //}
        public static object Float(string val)
        {
            if (val == "")
                return DBNull.Value;
            else
                return (float)Convert.ToDecimal(val.Trim());
        }
        public static object Decimal(string val)
        {
            if (val == "")
                return DBNull.Value;
            else
                return Convert.ToDecimal(val.Trim());
        }
        public static object DateTime(DateTime val)
        {
            if (val == Global.MinDate)
                return DBNull.Value;
            else
            {
                val.AddHours(-val.Hour);
                val.AddMinutes(-val.Minute);
                val.AddSeconds(-val.Second);
                return val;
            }
        }
        public static object Combo(ComboBox combo)
        {
            if (combo.SelectedIndex == -1)
                return DBNull.Value;
            else
                return ((BoxItem)combo.SelectedItem).Value;
        }
        public static object File(byte[] data)
        {
            if (data == null)
                return DBNull.Value;
            else
                return data;
        }
        public static object FileName(byte[] data, string name)
        {
            if (data == null)
                return DBNull.Value;
            else
                return name;
        }
    }
}
