using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


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

        /*
        public static void AddListParameters<T>(this SqlCommand cmd, string name, IEnumerable<T> values)
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
        */

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

        public static void AddUserControl(this SqlParameterCollection Param, UserControlFileEdit uc, int i)
        {
            string itos = i.ToString();

            Param.AddWithValue("@ID" + itos, uc.Id);

            Param.Add("@FILE" + itos, SqlDbType.Image);
            if (uc.Id == -1)
                Param["@FILE" + itos].Value = CheckNull.File(uc.Data);
            else
                Param["@FILE" + itos].Value = DBNull.Value;

            Param.AddWithValue("@FILENAME" + itos, uc.GetFileName());
            Param.AddWithValue("@ORD" + itos, i);
        }

        public static void AddUserControl(this SqlParameterCollection Param, UserControlFormStage uc, int i)
        {
            string itos = i.ToString();

            /*
            ([Номер], [Начало работ], [Окончание работ], [Количество], 
            [Цена], [Модель цены], [Заключение], [Аванс], [Расчёт], [Плановая трудоёмкость], 
            [Фактическая трудоёмкость], [Текущее состояние], [Номер акта], [Номер удостоверения])
            */
            /*
            return "@ID" + itos + ",@NUM,@NUMBER" + itos + ",@SDATE" + itos + ",@EDATE" + itos + ",@COUNT" + itos +
                ",@PRICE" + itos + ",@MODEL" + ",@ZAK" + itos + ",@AVANS" + itos + ",@RAS" + itos +
                ",@PTRUD" + itos + ",@FTRUD" + itos + ",@STATE" + itos + ",@ACT" + itos + ",@UD" + itos;
            */
            Param.AddWithValue("@ID" + itos, uc.Id);
            Param.AddWithValue("@NUMBER" + itos, CheckNull.Int(uc.number));
            Param.AddWithValue("@SDATE" + itos, CheckNull.DateTime(uc.date1));
            Param.AddWithValue("@EDATE" + itos, CheckNull.DateTime(uc.date2));
            Param.AddWithValue("@COUNT" + itos, CheckNull.Int(uc.count));
            Param.AddWithValue("@PRICE" + itos, CheckNull.Decimal(uc.price));
            Param.AddWithValue("@MODEL" + itos, CheckNull.Int(uc.model));
            Param.AddWithValue("@ZAK" + itos, CheckNull.String(uc.zak));
            Param.AddWithValue("@AVANS" + itos, CheckNull.Decimal(uc.avans));
            Param.AddWithValue("@RAS" + itos, CheckNull.Decimal(uc.raschet));
            Param.AddWithValue("@PTRUD" + itos, CheckNull.Float(uc.trud1));
            Param.AddWithValue("@FTRUD" + itos, CheckNull.Float(uc.trud2));
            Param.AddWithValue("@STATE" + itos, CheckNull.Int(uc.state));
            Param.AddWithValue("@ACT" + itos, CheckNull.Int(uc.act));
            Param.AddWithValue("@UD" + itos, CheckNull.Int(uc.ud));
        }


    }
}
