using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Data.SqlClient;


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

        public static int[] GetListBoxSelected(this ListBox listBox)
        {
            List<int> Set = new List<int>();

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                if (listBox.GetSelected(i))
                    Set.Add((int)((BoxItem)listBox.Items[i]).Value);
            }

            return Set.ToArray();
        }

        public static void AddArrayParameters<T>(this SqlCommand cmd, string name, IEnumerable<T> values)
        {
            var names = string.Join(", ", values.Select((value, i) =>
            {
                var paramName = name + i;
                cmd.Parameters.AddWithValue(paramName, value);
                return paramName;
            }));
            cmd.CommandText = cmd.CommandText.Replace(name, "(" + names + ")");
            //MessageBox.Show(names);
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
