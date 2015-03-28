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
using System.IO;
using System.Diagnostics;

namespace Diplomaster
{
    public partial class UserControlFileEdit : UserControl
    {
        public int Id;
        //public int Position;
        public string FileName;
        public string FileExtension;
        public string TempPath;
        public Process TempProcess;
        public byte[] Data = null;
        public bool Remove = false;
        public bool FileChanged = false;
        public bool FileNew = false;
        FormDocument FormDocParent;

        public UserControlFileEdit(FormDocument parent, bool itsnew = false)
        {
            InitializeComponent();
            //MessageBox.Show(textBoxImg1N.Name);
            FileNew = itsnew;
            FormDocParent = parent;
        }

        public void AddImageSet(int id, string name)
        {
            Id = id;
            //Position = pos;

            int lastdot;

            lastdot = name.LastIndexOf('.');

            if (lastdot == -1)
            {
                FileName = name;
                FileExtension = null;
            }
            else
            {
                FileName = name.Substring(0, lastdot);
                FileExtension = name.Substring(lastdot);
            }

            textBoxImg1N.Text = FileName;
            label1.Text = FileExtension;
        }

        public void LoadFileData() 
        {
            string query = "SELECT [Файл] FROM [Файл договора] WHERE [Id]=@ID";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", Id);
                    try
                    {
                        conn.Open();
                        object dat = cmd.ExecuteScalar();
                        if (!dat.isnull())
                            Data = (byte[])dat;
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
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            if (Data.isnull())
                LoadFileData();

            if (TempPath == null)
            {
                if (FileExtension == null)
                    TempPath = Path.GetTempPath() + Guid.NewGuid().ToString();
                else
                    TempPath = Path.GetTempPath() + Guid.NewGuid().ToString() + FileExtension;

                MessageBox.Show(TempPath);
                Extensions.SaveFile(TempPath, Data);
            }

            TempProcess = Process.Start(TempPath);

            //if (TempProcess == null)
            //{

                //MessageBox.Show(Extensions.GetDefaultApplication(FileExtension));
                //ProcessStartInfo pInfo = new ProcessStartInfo();
                //pInfo.FileName = TempPath;
                //pInfo.UseShellExecute = false;

                //string ppp = Extensions.GetDefaultApplication(FileExtension) + ", \"" + TempPath + "\"";
                //MessageBox.Show(ppp);
                //TempProcess = Process.Start(ppp);
                //TempProcess.WaitForInputIdle();
                //TempProcess.EnableRaisingEvents = true;
                //TempProcess.Exited += new System.EventHandler(DeleteTmp);
            //}
            //else
            //{
            //    TempProcess.Kill();
            //}
        }

        //private void DeleteTmp(object sender, EventArgs e)
        //{
        //    MessageBox.Show("EXITED!!!!!");

        //    File.Delete(TempPath);
        //    //TempProcess.Close();
        //}

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (Data.isnull())
                LoadFileData();

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = Global.LastDirectoryPath;
            saveFileDialog1.Filter = "All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;

            if (FileExtension.isnull())
                saveFileDialog1.FileName = FileName;
            else
                saveFileDialog1.FileName = FileName + FileExtension;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Extensions.SaveFile(saveFileDialog1.FileName, Data);
                    Global.LastDirectoryPath = Path.GetDirectoryName(saveFileDialog1.FileName);

                    var result = MessageBox.Show("Открыть папку с файлом?",
                                         "Показать файл",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = "explorer";
                        info.Arguments = string.Format("/e, /select, \"{0}\"", saveFileDialog1.FileName);
                        Process.Start(info);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Remove = !Remove;
            if (Remove)
            {
                buttonDelete.Text = "Восстановить";
                buttonExport.Enabled = false;
                buttonShow.Enabled = false;
                textBoxImg1N.Enabled = false;
            }
            else
            {
                buttonDelete.Text = "Удалить";
                buttonExport.Enabled = true;
                buttonShow.Enabled = true;
                textBoxImg1N.Enabled = true;
            }
        }

        private void groupBox_MouseClick(object sender, MouseEventArgs e)
        {
            FormDocParent.SelectFileEdit(this);
        }

        public void Highlight(bool b)
        {
            if (b)
                groupBox.BackColor = Global.ColorSelected;
            else
                groupBox.BackColor = Global.ColorDefaultBackground;
        }

    }
}
