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
using System.Data.SqlClient;


namespace Diplomaster
{
    public partial class Form1 : Form
    {
        public void RefreshListbox()
        {
            this.listBox1.Items.Clear();

            string query = "SELECT [Номер] FROM Договор ORDER BY [Номер] ASC"; //, [Генеральный Заказчик]
            
            //SqlCeCommand cmd = new SqlCeCommand(query, conn);

            using (SqlCeConnection conn = new SqlCeConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCeCommand cmd = new SqlCeCommand(query, conn);
                using (SqlCeDataReader rdr = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rdr.Read())
                        {
                            string s;
                            s = Convert.ToString(rdr.GetInt32(0));
                            listBox1.Items.Add(s);
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
        public void OpenNewDocForm() {
            //string CommandText = "SELECT Фамилия, Имя, Отчество FROM Туристы"; 
            FormDocument Doc = new FormDocument(this);
            Doc.Text = "Новый договор";
            Doc.Show();
        }
        public void OpenEditDocForm(int index)
        {
            int number = Convert.ToInt32(this.listBox1.Items[index]);
            //MessageBox.Show(Convert.ToString(number));
            
            FormDocument Doc = new FormDocument(this, number);
            //MessageBox.Show(Convert.ToString(Doc.DocNumber));
            Doc.Show();
            
        }

        public void CheckOpenForm(int index) {
            int number = Convert.ToInt32(this.listBox1.Items[index]);
            //MessageBox.Show(Convert.ToString(number));
            bool notfound = true;

            foreach (FormDocument form in Application.OpenForms.OfType<FormDocument>())
            {
                //MessageBox.Show(Convert.ToString(form.DocNumber));
                if (form.DocNumber == number)
                {
                    form.Activate();
                    notfound = false;
                    break;
                }
            }

            if (notfound)
            {
                FormDocument Doc = new FormDocument(this, number);
                //MessageBox.Show(Convert.ToString(Doc.DocNumber));
                Doc.Show();
            }
        }

        public Form1()
        {
            InitializeComponent();
            RefreshListbox();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != -1)
                CheckOpenForm(index);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenNewDocForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(textBox1.Text + " --> " + listBox1.FindString(textBox1.Text));
            if (textBox1.Text != string.Empty)
            {
                int num;

                if ((num = listBox1.FindString(textBox1.Text)) != -1)
                    //OpenEditDocForm(num);
                    listBox1.SetSelected(num, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshListbox();
        }
        
    }
}
