using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomaster
{
    public static class Extensions
    {
        public static string ToString2(this Array target)
        {
            bool check = false;
            string str = "[";
            foreach (var item in target)
            {
                str += item.ToString() + ", ";
                check = true;
            }
            if (check)
            {
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
