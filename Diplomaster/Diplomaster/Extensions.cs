using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

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


        public static void SaveFile(string path, byte[] data)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                fs.Write(data, 0, data.Length);
            }
        }

        public static byte[] LoadFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;
                byte[] data = new byte[fs.Length];

                while (numBytesToRead > 0)
                {
                    int n = fs.Read(data, numBytesRead, numBytesToRead);
                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                return data;
            }
        }


        //public static string GetDefaultApplication(string extension)
        //{
        //    if (String.IsNullOrEmpty(extension))
        //        return null;

        //    var typefileReg = Registry.ClassesRoot.OpenSubKey(extension);

        //    if (typefileReg == null)
        //        return null;

        //    var typefile = typefileReg.GetValue(null);
        //    if (typefile == null)
        //        return null;
            
        //    var commandReg = Registry.ClassesRoot.OpenSubKey(typefile.ToString() + @"\shell\open\command");
        //    if (commandReg == null)
        //        return null;

        //    var command = commandReg.GetValue(null);
        //    if (command == null)
        //        return null;

        //    return command.ToString().Replace("%1", "");
        //}

    }
}
