using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;
using System.IO;
//using System.Diagnostics;

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
            //MessageBox.Show(Process.EnableRaisingEvents.ToString());
            LoadDocSchema();

            Application.Run(new FormStart());
        }

        private static void LoadDocSchema()
        {
            List<string> list = new List<string>();
            string query = "SELECT TOP 0 * FROM [Договор]";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        rdr.Read();
                        for (int i = 0; i < rdr.FieldCount; i++)
                            list.Add(rdr.GetName(i));
                    }
                    finally
                    {
                        rdr.Close();
                        conn.Close();
                    }
                }

                conn.Close();
            }

            Global.DocSchema = list.ToArray();
            //MessageBox.Show(Global.DocSchema.ToString2());
        }

        
    }
}