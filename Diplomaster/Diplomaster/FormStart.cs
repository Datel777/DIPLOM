using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace Diplomaster
{
    public partial class FormStart : Form
    {
        //TabPage firstTabPage = new TabPage("Все");
        TabPage lastTabPage = new TabPage("+");
        //TreeNode needload = new TreeNode("Loading is soooo boring");


        public void RefreshListbox()
        {
            this.listBox1.Items.Clear();

            string query = "SELECT [Номер] FROM [Договор] ORDER BY [Номер] ASC"; //, [Генеральный Заказчик]
            
            //SqlCommand cmd = new SqlCommand(query, conn);

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

        public void RefreshTree()
        {
            int minyear, maxyear;
            treeView1.Nodes.Clear();

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

            

            for (int i = minyear; i <= maxyear; ++i)
            {
                //MessageBox.Show("Min = " + minyear.ToString() + " Max = " + maxyear.ToString());
                //MessageBox.Show(i.ToString());
                //TreeNode node = new TreeNode("11");
                
                TreeNode node = new TreeNode(i.ToString());
                node.Name = i.ToString();
                node.Nodes.Add(Global.LoadNodeName);
                treeView1.Nodes.Add(node);
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

        public void InitializeTabControl(TabControl tabC)
        {
            //UserControl myUserControl = new UserControlSearch();
            //myUserControl.Dock = DockStyle.Fill;
            //firstTabPage.Controls.Add(myUserControl);
            //tabC.Controls.Add(firstTabPage);
            tabC.Controls.Add(CreateNewPage("Какой-то поиск"));
            tabC.Controls.Add(lastTabPage);
        }

        public TabPage CreateNewPage(string title = null)
        {
            TabPage page;
            if (String.IsNullOrEmpty(title)) 
                page = new TabPage("----");
            else 
                page = new TabPage(title);
            
            UserControl myUserControl = new UserControlSearch();
            myUserControl.Dock = DockStyle.Fill;
            page.Controls.Add(myUserControl);

            return page;
        }

        public FormStart()
        {
            //MessageBox.Show("BEGIN INIT");
            InitializeComponent();
            InitializeTabControl(tabControl1);
            RefreshListbox();
            RefreshTree();
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
            RefreshTree();
        }


        private void tabControl1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                for (int i = 0; i < tabControl1.TabCount; ++i)
                {
                    if (tabControl1.GetTabRect(i).Contains(e.Location))
                    {
                        TabPage tab = tabControl1.TabPages[i];

                        //if (tab!=firstTabPage && tab!=lastTabPage)
                        if (tab != lastTabPage)
                            tabControl1.TabPages.Remove(tab);
                        //this.contextMenuStrip1.Show(this.tabControl1, e.Location);

                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == lastTabPage)
            {
                TabPage page = CreateNewPage("Поиск "+ tabControl1.SelectedIndex.ToString());

                tabControl1.TabPages.Insert(tabControl1.SelectedIndex, page);
                tabControl1.SelectedTab = page;
            }
            
        }

        private int GetBeginsYearCount(int year)
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

        private int GetContinuesYearCount(int year)
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

        private int GetEndsYearCount(int year)
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

        private void AddYearNodes(TreeNode node, int year)
        {
            int beginsCount = GetBeginsYearCount(year);
            int continuesCount = GetContinuesYearCount(year);
            int endsCount = GetEndsYearCount(year);

            TreeNode begins = new TreeNode("Начинающиеся(" + beginsCount.ToString() + ")");
            TreeNode continues = new TreeNode("Действующие(" + continuesCount.ToString() + ")");
            TreeNode ends = new TreeNode("Заканчивающиеся(" + endsCount.ToString() + ")");

            begins.Name = "begins";
            continues.Name = "continues";
            ends.Name = "ends";

            if (beginsCount>0)
                begins.Nodes.Add(Global.LoadNodeName);
            if (continuesCount > 0)
                continues.Nodes.Add(Global.LoadNodeName);
            if (endsCount > 0)
                ends.Nodes.Add(Global.LoadNodeName);

            node.Nodes.Add(begins);
            node.Nodes.Add(continues);
            node.Nodes.Add(ends);
            //node.Parent.Name;
        }


        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            
            //MessageBox.Show(node.Text);
            //MessageBox.Show(node.Level.ToString());
            if (node.Level==0)
                if (node.Nodes.Count==1 && node.Nodes[0].Text==Global.LoadNodeName)
                {
                    int year = Convert.ToInt32(node.Text);
                    //MessageBox.Show(node.Text);
                    node.Nodes.Clear();
                    AddYearNodes(node, year);
                }

        }

        //MessageBox.Show("----");
        //if (e.Button == MouseButtons.Left)
        //{
        //    for (int i = 0; i < tabControl1.TabCount; ++i)
        //    {
        //        if (tabControl1.GetTabRect(i).Contains(e.Location))
        //        {
        //            TabPage tab = tabControl1.TabPages[i];

        //            if (tab == lastTabPage)
        //            {
        //                TabPage page = CreateNewPage();

        //                tabControl1.TabPages.Insert(tabControl1.SelectedIndex, page);
        //                tabControl1.SelectedTab = page;
        //            }
        //        }
        //    }
        //}



    }
}
