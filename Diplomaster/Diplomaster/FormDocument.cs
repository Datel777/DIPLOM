using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Diplomaster
{
    public partial class FormDocument : Form
    {
        public Int32 DocNumber;
        public Form1 FormParent;

        public void LoadCombo(Int32 number)
        {
            string connectionString = "Data Source=Database.sdf";
            string query = "SELECT [id], [Название] FROM [Заказчик]";// WHERE [Иностранный]=0

            using (SqlCeConnection conn = new SqlCeConnection(connectionString))
            {
                conn.Open();
                SqlCeCommand cmd = new SqlCeCommand(query, conn);
                using (SqlCeDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        
                        Int32 ind, i = 0;
                        while (rdr.Read())
                        {
                            ComboboxItem item = new ComboboxItem();

                            ind = rdr.GetInt32(0);
                            item.Text = rdr.GetString(1);
                            item.Value = ind;
                            
                            comboBox1.Items.Add(item);
                            //comboBox1.Items.Add(new Item(rdr.GetString(1), ind));

                            
                            if (ind == number)
                            {
                                comboBox1.SelectedIndex = i;
                            }
                            
                            i++;
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

        public void ReadElement(SqlCeDataReader rdr) {
            textBox1.Text = Convert.ToString(rdr.GetInt32(0));

            LoadCombo(rdr.GetInt32(1));

            object val;
            val = rdr.GetValue(2);
            if (val != null)
                textBox3.Text = val.ToString();
            val = rdr.GetValue(3);
            if (val != null)
                textBox4.Text = val.ToString();
        }

        public FormDocument(Form1 form1, Int32 num =-1)
        {
            InitializeComponent();
            DocNumber = num;
            FormParent = form1;

            if (DocNumber != -1)
            {
                this.Text = "Редактирование №" + DocNumber;

                string connectionString = "Data Source=Database.sdf";
                string query = "SELECT * FROM [Договор] WHERE [Номер]=" + Convert.ToString(DocNumber);

                using (SqlCeConnection conn = new SqlCeConnection(connectionString))
                {
                    conn.Open();
                    SqlCeCommand cmd = new SqlCeCommand(query, conn);
                    using (SqlCeDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            ReadElement(rdr);
                        }
                        rdr.Close();
                    }

                    conn.Close();
                }
                textBox1.ReadOnly = true;
            }
            else
            {
                LoadCombo(-1);
                button2.Hide();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Convert.ToString(DocNumber));

            string connectionString = "Data Source=Database.sdf";
            string query;

            using (SqlCeConnection conn = new SqlCeConnection(connectionString)) {
                conn.Open();

                if (DocNumber == -1)
                    query = "INSERT INTO [Договор] VALUES (@ID, @GEN, @VED, @PRI)";
                else
                    query = "UPDATE [Договор] SET [Генеральный Заказчик]=@GEN, [Ведущий]=@VED, [Примечание]=@PRI WHERE [Номер]=@ID";

                SqlCeCommand cmd = new SqlCeCommand(query, conn);
                if (DocNumber == -1)
                    cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                else
                    cmd.Parameters.AddWithValue("@ID", Convert.ToString(DocNumber));
                cmd.Parameters.AddWithValue("@GEN", ((ComboboxItem)comboBox1.SelectedItem).Value);
                cmd.Parameters.AddWithValue("@VED", textBox3.Text);
                cmd.Parameters.AddWithValue("@PRI", textBox4.Text);
                cmd.ExecuteNonQuery();
                FormParent.RefreshListbox();
                conn.Close();
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить договор?", 
                                         "Удаление",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string connectionString = "Data Source=Database.sdf";
                string query = "DELETE FROM [Договор] WHERE [Номер]=@ID";

                using (SqlCeConnection conn = new SqlCeConnection(connectionString))
                {
                    conn.Open();

                    SqlCeCommand cmd = new SqlCeCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", DocNumber);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                FormParent.RefreshListbox();
                Close();
            }
        }
    }
}

public class ComboboxItem
{
    public string Text { get; set; }
    public object Value { get; set; }

    public override string ToString()
    {
        return Text;
    }
}