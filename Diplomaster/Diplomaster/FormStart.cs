using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplomaster
{
    public partial class FormStart : Form
    {
        //TabPage firstTabPage = new TabPage("Все");
        TabPage lastTabPage = new TabPage("+");
        int TabNumber = 1;
        //TreeNode needload = new TreeNode("Loading is soooo boring");

        public void RefreshDocs()
        {
            RefreshListbox();
            RefreshTree();
        }

        public void RefreshListbox()
        {
            this.listBox1.Items.Clear();

            foreach (int element in SQL.GetOneOrder("Договор", "Номер", true))
                listBox1.Items.Add(element.ToString());
        }

        public void RefreshTree()
        {
            treeView1.Nodes.Clear();

            int minyear, maxyear;
            SQL.SelectMinMaxYears(out minyear, out maxyear);

            //{
            //    Tuple<int, int> tuple = SQL.SelectMinMaxYears(minyear, maxyear);
            //    minyear = tuple.Item1;
            //    maxyear = tuple.Item2;
            //}

            string y;
            for (int i = minyear; i <= maxyear; ++i)
            {
                //MessageBox.Show("Min = " + minyear.ToString() + " Max = " + maxyear.ToString());
                //MessageBox.Show(i.ToString());
                //TreeNode node = new TreeNode("11");
                y = i.ToString();
                TreeNode node = new TreeNode(y);
                node.Name = y;
                //node.Checked = true;
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

        public void CheckOpenForm(int number) {
            
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
            tabC.Controls.Add(CreateNewReportPage());
            tabC.Controls.Add(lastTabPage);
        }

        public TabPage CreateNewReportPage(bool getnum = true)
        {
            TabPage page;
            //if (number == -1) 
            //    page = new TabPage("----");
            //else
            //    page = new TabPage("Отчёт " + number.ToString());


            UserControl myUserControl;


            if (getnum)
            {
                page = new TabPage("Отчёт " + TabNumber.ToString());
                myUserControl = new UserControlReport(TabNumber);
                TabNumber++;
            }
            else
            {
                page = new TabPage("----");
                myUserControl = new UserControlReport(-1);
            }

            myUserControl.Dock = DockStyle.Fill;
            page.Controls.Add(myUserControl);

            return page;
        }

        //public TabPage CreateNewSearchPage(string title = null)
        //{
        //    TabPage page;
        //    if (String.IsNullOrEmpty(title))
        //        page = new TabPage("----");
        //    else
        //        page = new TabPage(title);

        //    UserControl myUserControl = new UserControlSearch();
        //    myUserControl.Dock = DockStyle.Fill;
        //    page.Controls.Add(myUserControl);

        //    return page;
        //}

        

        public FormStart()
        {
            //MessageBox.Show("BEGIN INIT");
            InitializeComponent();
            InitializeTabControl(tabControl1);
            RefreshDocs();
            //MessageBox.Show(Controls["button3"].Text);
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != -1)
                CheckOpenForm(Convert.ToInt32(this.listBox1.Items[index]));
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
            RefreshDocs();
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
                        {
                            ((UserControlReport)tab.Controls[0]).CloseFormFilter();
                            tabControl1.TabPages.Remove(tab);
                        }
                        //this.contextMenuStrip1.Show(this.tabControl1, e.Location);
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == lastTabPage)
            {
                TabPage page = CreateNewReportPage();

                tabControl1.TabPages.Insert(tabControl1.SelectedIndex, page);
                tabControl1.SelectedTab = page;
            }
            
        }

        private void AddYearNodes(TreeNode node, int year)
        {
            int beginsCount = SQL.GetBeginsYearCount(year);
            int continuesCount = SQL.GetContinuesYearCount(year);
            int endsCount = SQL.GetEndsYearCount(year);

            TreeNode begins = new TreeNode("Начинающиеся (" + beginsCount.ToString() + ")");
            TreeNode continues = new TreeNode("Действующие (" + continuesCount.ToString() + ")");
            TreeNode ends = new TreeNode("Заканчивающиеся (" + endsCount.ToString() + ")");

            begins.Name = "begins";
            continues.Name = "continues";
            ends.Name = "ends";

            if (beginsCount > 0)
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

        private void AddBeginsNodes(TreeNode parent, int year)
        {
            foreach (Tuple<int, DateTime> tuple in SQL.GetBeginsNodes(year))
            {
                string num = tuple.Item1.ToString();
                TreeNode node = new TreeNode("[" + num + "] с " + tuple.Item2.ToString("dd MMMM"));
                node.Name = num;

                parent.Nodes.Add(node);
            }
        }

        private void AddContinuesNodes(TreeNode parent, int year)
        {
            foreach (Tuple<int, DateTime, DateTime> tuple in SQL.GetContinuesNodes(year))
            {
                string num = tuple.Item1.ToString();
                TreeNode node = new TreeNode("[" + num + "] с " + tuple.Item2.ToString("dd MMMM yyyy") + " до " + tuple.Item3.ToString("dd MMMM yyyy"));
                node.Name = num;

                parent.Nodes.Add(node);
            }
        }

        private void AddEndsNodes(TreeNode parent, int year)
        {
            foreach (Tuple<int, DateTime> tuple in SQL.GetEndsNodes(year))
            {
                string num = tuple.Item1.ToString();
                TreeNode node = new TreeNode("[" + num + "] с " + tuple.Item2.ToString("dd MMMM"));
                node.Name = num;

                parent.Nodes.Add(node);
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            
            //MessageBox.Show(node.Text);
            //MessageBox.Show(node.Level.ToString());

            if (node.Level == 0)
            {
                if (node.Nodes.Count == 1 && node.Nodes[0].Text == Global.LoadNodeName)
                {
                    int year = Convert.ToInt32(node.Text);
                    //MessageBox.Show(node.Text);
                    node.Nodes.Clear();
                    AddYearNodes(node, year);
                }
            }
            else if (node.Level == 1)
            {
                if (node.Nodes.Count == 1 && node.Nodes[0].Text == Global.LoadNodeName)
                {
                    //int year = Convert.ToInt32(node.Text);
                    ////MessageBox.Show(node.Text);
                    //node.Nodes.Clear();
                    //AddYearNodes(node, year);
                    int year = Convert.ToInt32(node.Parent.Text);

                    node.Nodes.Clear();
                    if (node.Name == "begins")
                        AddBeginsNodes(node, year);
                    if (node.Name == "continues")
                        AddContinuesNodes(node, year);
                    if (node.Name == "ends")
                        AddEndsNodes(node, year);
                }
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level==2)
                CheckOpenForm(Convert.ToInt32(e.Node.Name));
        }
    }
}
