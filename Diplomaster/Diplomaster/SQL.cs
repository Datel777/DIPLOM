using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;


namespace Diplomaster
{
    static class SQL
    {
        //SELECTS
        public static string[] LoadSchema(string where)
        {
            List<string> list = new List<string>();
            string query = "SELECT TOP 0 * FROM [" + where + "]";

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

            return list.ToArray();
            //MessageBox.Show(Global.DocSchema.ToString2());
        }

        public static List<object> GetOneOrder(string where, string what, bool asc = true)
        {
            List<object> result = new List<object>();

            string query = "SELECT [" + what + "] FROM [" + where + "] ORDER BY [" + what + "] ";

            query += asc ? "ASC" : "DESC";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rdr.Read())
                        {
                            result.Add(rdr.GetValue(0));
                        }
                    }
                    finally
                    {
                        rdr.Close();
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public static List<Tuple<object, object>> GetTwoOrder(string where, string what1, string what2, string order, bool asc = true, string arg1 = null, object val1 = null)
        {
            List<Tuple<object, object>> result = new List<Tuple<object, object>>();

            string query = "SELECT [" + what1 + "],[" + what2 + "] FROM [" + where + "]";

            if (arg1 != null)
                query += " WHERE [" + arg1 + "] = @ARG1";

            query += " ORDER BY [" + order + "] ";
            query += asc ? "ASC" : "DESC";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                if (arg1 != null)
                    cmd.Parameters.AddWithValue("@ARG1", val1);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rdr.Read())
                        {
                            Tuple<object, object> turple = new Tuple<object, object>(rdr.GetValue(0), rdr.GetValue(1));
                            result.Add(turple);
                        }
                    }
                    finally
                    {
                        rdr.Close();
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public static object GetOneFirst(string where, string what, string arg1 = null, object val1 = null)
        {
            object result = null;

            string query = "SELECT ["+what+"] FROM ["+where+"]";

            if (arg1 != null)
                query += " WHERE [" + arg1 + "] = @ARG1";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (arg1 != null)
                        cmd.Parameters.AddWithValue("@ARG1", val1);

                    try
                    {
                        conn.Open();
                        result = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        //public static int[] SelectMinMaxYears(string where, string what1, string what2) {
        //    string query = "SELECT MIN(Year(["+what1+"])), MAX(Year(["+what2+"])) FROM ["+where+"]";
        public static void SelectMinMaxYears(out int minyear, out int maxyear) {
            string query = "SELECT MIN(Year([Начало работ])), MAX(Year([Окончание работ])) FROM [Договор]";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        rdr.Read();
                        minyear = rdr.GetInt32(0);
                        maxyear = rdr.GetInt32(1);
                    }
                    finally
                    {
                        rdr.Close();
                        conn.Close();
                    }
                }
            }
            //return new Tuple<int, int>(minyear,maxyear);
        }

        public static int GetBeginsYearCount(int year)
        {
            int count = 0;

            string query = "SELECT COUNT (*) FROM [Договор] WHERE Year([Начало работ]) = @YEAR";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@YEAR", year);

                count = (Int32)cmd.ExecuteScalar();
            }

            return count;
        }

        public static int GetContinuesYearCount(int year)
        {
            int count = 0;

            string query = "SELECT COUNT (*) FROM [Договор] WHERE Year([Начало работ]) < @YEAR AND Year([Окончание работ]) > @YEAR";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@YEAR", year);

                count = (Int32)cmd.ExecuteScalar();
            }

            return count;
        }

        public static int GetEndsYearCount(int year)
        {
            int count = 0;

            string query = "SELECT COUNT (*) FROM [Договор] WHERE Year([Окончание работ]) = @YEAR";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@YEAR", year);

                count = (Int32)cmd.ExecuteScalar();
            }

            return count;
        }

        public static List<Tuple<int, DateTime>> GetBeginsNodes(int year)
        {
            string query = "SELECT [Номер], [Начало работ] FROM [Договор] WHERE Year([Начало работ]) = @YEAR ORDER BY [Начало работ]"; // MONTH([Начало работ])

            List<Tuple<int, DateTime>> result = new List<Tuple<int, DateTime>>();
            
            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@YEAR", year);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rdr.Read())
                        {
                            //int num = rdr.GetInt32(0);
                            //DateTime date = rdr.GetDateTime(1);
                            result.Add(new Tuple<int, DateTime>(rdr.GetInt32(0), rdr.GetDateTime(1)));
                        }
                    }
                    finally
                    {
                        rdr.Close();
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public static List<Tuple<int, DateTime, DateTime>> GetContinuesNodes(int year)
        {
            string query = "SELECT [Номер], [Начало работ], [Окончание работ] FROM [Договор] WHERE Year([Начало работ]) < @YEAR AND Year([Окончание работ]) > @YEAR ORDER BY [Начало работ]"; // MONTH([Начало работ])

            List<Tuple<int, DateTime, DateTime>> result = new List<Tuple<int, DateTime, DateTime>>();

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@YEAR", year);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rdr.Read())
                        {
                            //int num = rdr.GetInt32(0);
                            //DateTime date1 = rdr.GetDateTime(1);
                            //DateTime date2 = rdr.GetDateTime(2);
                            result.Add(new Tuple<int, DateTime, DateTime>(rdr.GetInt32(0), rdr.GetDateTime(1), rdr.GetDateTime(2)));
                        }
                    }
                    finally
                    {
                        rdr.Close();
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public static List<Tuple<int, DateTime>> GetEndsNodes(int year)
        {
            string query = "SELECT [Номер], [Окончание работ] FROM [Договор] WHERE Year([Окончание работ]) = @YEAR ORDER BY [Окончание работ]"; // MONTH([Начало работ])

            List<Tuple<int, DateTime>> result = new List<Tuple<int, DateTime>>();

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@YEAR", year);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rdr.Read())
                        {
                            //int num = rdr.GetInt32(0);
                            //DateTime date = rdr.GetDateTime(1);

                            result.Add(new Tuple<int, DateTime>(rdr.GetInt32(0), rdr.GetDateTime(1)));
                        }
                    }
                    finally
                    {
                        rdr.Close();
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public static Hashtable ReadAll(string where, string arg1, object val1)
        {
            Hashtable data = new Hashtable();

            string query = "SELECT * FROM [" + where + "] WHERE [" + arg1 + "]=@ARG1";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ARG1", val1);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                for (int i = 0; i < rdr.FieldCount; i++)
                                    data.Add(rdr.GetName(i), rdr.GetValue(i));
                            }
                            rdr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
            return data;
        }

        public static List<Hashtable> ReadAllMultiple(string where, string arg1, object val1)
        {
            List<Hashtable> result = new List<Hashtable>();

            string query = "SELECT * FROM [" + where + "] WHERE [" + arg1 + "]=@ARG1";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ARG1", val1);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                Hashtable data = new Hashtable();
                                for (int i = 0; i < rdr.FieldCount; i++)
                                    data.Add(rdr.GetName(i), rdr.GetValue(i));
                                result.Add(data);
                            }
                            rdr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
            return result;
        }

        public static int[] ReadManyToMany(string where, string what, string arg1, int id1, string orderby = null)
        {
            List<int> result = new List<int>();
            
            string query = "SELECT [" + what + "] FROM [" + where + "] WHERE [" + arg1 + "] = @ID";

            if (orderby != null)
                query += " ORDER BY [" + orderby + "] ASC";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id1);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                                result.Add(rdr.GetInt32(0));

                            rdr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return result.ToArray();
        }

        public static bool CheckUnique(string where, string what, object arg1)
        {
            string query = "SELECT COUNT (*) FROM [" + where + "] WHERE [" + what + "] = @ARG";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ARG", arg1);

                return (Int32)cmd.ExecuteScalar() == 0;
            }
        }

        public static void ReadToCombo(ComboBox combo, string where, string what, string arg1 = null, object val1 = null)
        {
            string query = "SELECT [Id], [" + what + "] FROM [" + where + "]";

            if (arg1 != null)
                query += " WHERE [" + arg1 + "] = @ARG1";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                if (arg1 != null)
                    cmd.Parameters.AddWithValue("@ARG1", val1);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rdr.Read())
                        {
                            BoxItem item = new BoxItem();

                            item.Text = rdr.GetString(1);
                            item.Value = rdr.GetInt32(0);

                            combo.Items.Add(item);
                        }
                    }
                    finally
                    {
                        rdr.Close();
                        conn.Close();
                    }
                }
            }
        }

        public static void ReadToList(ListBox list, string where, string what, string arg1 = null, object val1 = null)
        {
            string query = "SELECT [id], [" + what + "] FROM [" + where + "]";

            if (arg1 != null)
                query += " WHERE [" + arg1 + "] = @ARG1";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                if (arg1 != null)
                    cmd.Parameters.AddWithValue("@ARG1", val1);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rdr.Read())
                        {
                            BoxItem item = new BoxItem();

                            item.Text = rdr.GetString(1);
                            item.Value = rdr.GetInt32(0);

                            list.Items.Add(item);
                        }
                    }
                    finally
                    {
                        rdr.Close();
                        conn.Close();
                    }
                }
            }
        }
        
        //INSERTS
        public static void InsertManyToMany(int NUM, string where, int[] ids, string arg1, string arg2)
        {
            int length;

            if ((length = ids.Length) > 0)
            {
                string query = "INSERT INTO [" + where + "] ([" + arg1 + "], [" + arg2 + "])VALUES(@NUM,@VAL0)";
                int i;

                if (length > 1)
                {
                    for (i = 1; i < length; i++)
                        query += ",(@NUM,@VAL" + i.ToString() + ")";
                }


                using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NUM", NUM);
                    for (i = 0; i < length; i++)
                        cmd.Parameters.AddWithValue("@VAL" + i.ToString(), ids[i]);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        //UPDATES

        //DELETES
        public static void DeleteManyToMany(int NUM, string where, int[] ids, string arg1, string arg2)
        {
            int length;

            if ((length = ids.Length) > 0)
            {
                string query = "DELETE FROM [" + where + "] WHERE [" + arg1 + "] = @NUM AND [" + arg2 + "] IN (@VAL0";
                int i;

                if (length > 1)
                {
                    for (i = 1; i < length; i++)
                        query += ",@VAL" + i.ToString();
                }

                query += ")";

                using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NUM", NUM);
                    for (i = 0; i < length; i++)
                        cmd.Parameters.AddWithValue("@VAL" + i.ToString(), ids[i]);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        


    }
}
