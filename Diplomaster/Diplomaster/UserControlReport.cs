using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Collections;

namespace Diplomaster
{
    public partial class UserControlReport : UserControl
    {
        int TabNumber;
        public FormReportFilter FormFilter;
        public Hashtable FilterData = new Hashtable();// Or HashSet?
        private void InitTree() {
            OwnTreeView.Initialize(treeView1);
            foreach (string name in Global.DocSchema) {
                OwnTreeNode node = new OwnTreeNode();
                node.Name = name;

                switch (name)
                {
                    case "Номер":
                        node.hideCheck = true;
                        node.Text = name;
                    //    node.Name = name;
                    //    node.Text = name;
                    //    node.Checked = true;
                    //    //node.ForeColor = Color.Gray;
                    //    //node.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
                        break;
                    case "Генеральный заказчик_id":
                        node.Text = "Генеральный заказчик";
                        break;
                    default:
                        node.Text = name;
                        //node.Checked = false;
                        break;
                }
                
                treeView1.Nodes.Add(node);
            }

            
        }

        public UserControlReport(int tabnum)
        {
            InitializeComponent();
            TabNumber = tabnum;

            InitTree();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string args = "[Номер],";
                bool joingen = false;
                
                //args += "[Номер],";
                
                foreach (TreeNode node in treeView1.Nodes)
                {
                    if (node.Checked)
                        switch (node.Name) {
                            case "Генеральный заказчик_id":
                                joingen = true;
                                args += "gen.[Название]as[Генеральный заказчик],";
                                break;
                            default: 
                                args += "[" + node.Name + "],";
                                break;
                        }
                        
                    
                }



                //if (!String.IsNullOrEmpty(args))
                {

                    //args = args.Remove(args.Length-1);
                    //MessageBox.Show(args);
                    string selectCommand = "SELECT " + args.Remove(args.Length - 1) + " FROM [Договор] AS d";

                    if (joingen)
                        selectCommand += " LEFT OUTER JOIN [Юридическое лицо] AS gen ON d.[Генеральный заказчик_id]=gen.Id";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand, Global.ConnectionString);

                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                    DataTable table = new DataTable();
                    //table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    dataAdapter.Fill(table);
                    dataGridView1.DataSource = table;

                    //Filter
                    //foreach (DataGridViewRow dr in dataGridView1.Rows)
                    //{
                    //    if (dr.Cells["Дополнительное соглашение"].Value.isnull())
                    //    {
                    //        //dr.Visible = false;
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView1.SelectedNode = null;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool ch = checkBox1.Checked;
            foreach (TreeNode node in treeView1.Nodes)
            {
                node.Checked = ch;
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (FormFilter == null || FormFilter.IsDisposed)
            {
                FormFilter = new FormReportFilter(FilterData);
                if (TabNumber == -1)
                    FormFilter.Text = "Фильтр ----";
                else
                    FormFilter.Text = "Фильтр отчёта " + TabNumber.ToString();

                FormFilter.Show();
            }
            else
                FormFilter.Activate();
        }

        public void CloseFormFilter()
        {
            if (!(FormFilter == null || FormFilter.IsDisposed))
                FormFilter.Close();
        }
    }
}
