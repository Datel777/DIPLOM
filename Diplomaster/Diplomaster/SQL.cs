using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Windows.Forms;

namespace Diplomaster
{
    static class SQL
    {
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

        public static int[] ReadManyToMany(string where, string what, string arg1, int id1)
        {
            List<int> result = new List<int>();
            
            string query = "SELECT [" + what + "] FROM [" + where + "] WHERE [" + arg1 + "] = @ID";

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
    }
}
