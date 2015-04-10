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
using System.Diagnostics;
using System.Collections;
using System.IO;

namespace Diplomaster
{
    public partial class FormDocument : Form
    {
        public int DocNumber;
        public FormStart FormParent;
        public Hashtable DATA = new Hashtable();

        OwnTabPage lastTabPage = new OwnTabPage("+");

        public UserControlFileEdit FlowSelected;

        public TabPage CreateNewStagePage(Hashtable data = null)
        {
            OwnTabPage page;
            UserControl myUserControl;
            
            if (data == null)
                page = new OwnTabPage(Global.EmptyStageText);
            else
                page = new OwnTabPage(Global.StageTextPrefix + data["Номер"].ToString());
            myUserControl = new UserControlFormStage(this, data);

            myUserControl.Dock = DockStyle.Fill;
            page.Controls.Add(myUserControl);

            return page;
        }
        
        public void SelectCombo(ComboBox combo, object val)
        {
            //MessageBox.Show(val.ToString());
            if (val.isnull())
                return;
            int number = (int)val;
            int i = 0;
            foreach (BoxItem item in combo.Items)
            {
                if ((int)item.Value == number)
                {
                    combo.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        public void SelectList(ListBox list, object val)
        {
            int[] numbers = (int[])val;
            List<int> indexes = new List<int>();
            int i = 0;
            foreach (BoxItem item in list.Items)
            {
                if (Array.IndexOf(numbers, (int)item.Value) != -1)
                {
                    indexes.Add(i);
                    //MessageBox.Show("Selected [" + Convert.ToString(i) + "] " + item.Text);
                }
                i++;
            }
            foreach (int id in indexes)
                list.SetSelected(id, true);
        }

        public void FillAll()
        {
            //textBox1.Text = Convert.ToString(DocNumber);
            Fill.TextBoxInt(textBox1, DocNumber);

            SelectCombo(comboBox1, DATA["Генеральный заказчик_id"]);
            SelectList(listBox1, DATA["Иностранный заказчик"]);
            SelectList(listBox2, DATA["Исполнитель договора"]);

            Fill.TextBox(textBox16, DATA["Дополнительное соглашение"]);
            Fill.TextBox(textBox2, DATA["Вид работ"]);
            Fill.TextBox(textBox5, DATA["Тема"]);
            Fill.TextBox(textBox6, DATA["Наименование работ"]);
            Fill.TextBoxInt(textBox7, DATA["Количество"]);
            Fill.TextBoxDecimal(textBox8, DATA["Цена"]);
            Fill.TextBoxDecimal(textBox9, DATA["Цена за единицу"]);
            Fill.TextBoxInt(textBox10, DATA["Модель цены"]);
            Fill.TextBoxFloat(textBox11, DATA["Объём собственной работы"]);
            Fill.TextBoxFloat(textBox12, DATA["Объём КА"]);
            Fill.TextBoxFloat(textBox13, DATA["Плановая трудоёмкость"]);
            Fill.TextBoxFloat(textBox14, DATA["Фактическая трудоёмкость"]);
            Fill.TextBoxInt(textBox15, DATA["Страница"]);
            Fill.TextBox(textBox3, DATA["Ведущий"]);
            Fill.TextBox(textBox4, DATA["Примечание"]);

            Fill.DateTimePicker(dateTimePicker1, DATA["Начало работ"]);
            Fill.DateTimePicker(dateTimePicker2, DATA["Окончание работ"]);
        }

        /*
        public string GetListBoxSet(ListBox listBox)
        {
            return string.Join(",", GetListBoxSelected(listBox));
        }

        public string GetListBoxInsertSet(ListBox listBox, int num)
        {
            string str = "";
            string numstr = Convert.ToString(num);
            int [] Set = GetListBoxSelected(listBox);

            foreach (int element in Set) {
                str += "(" + numstr + ", " + Convert.ToString(element) +"), ";
            }
            if (Set.Length > 0)
            {
                str = str.Remove(str.Length - 2);
            }
            return str;
        }
        */
        /*
        public string GetCombine(int[] arr, int num)
        {
            string str = "";
            string numstr = Convert.ToString(num);

            foreach (int element in arr) {
                str += "(" + numstr + ", " + Convert.ToString(element) + "), ";
            }
            if (arr.Length > 0) {
                str = str.Remove(str.Length - 2);
            }
            //MessageBox.Show(str);
            return str;
        }
        */


        /*
        public void ReadFrom(string where, string what, string arg1)
        {
            string query = "SELECT [" + what + "] FROM [" + where + "] WHERE [" + arg1 + "] = @NUM";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NUM", DocNumber);
                    try
                    {
                        conn.Open();
                        DATA[where] = ReadIdsToArray(cmd);
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

        public int[] ReadIdsToArray(SqlCommand cmd)
        {
            List<int> result = new List<int>();

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                    result.Add(rdr.GetInt32(0));

                rdr.Close();
            }
            return result.ToArray();
        }
        */


        public void ReadFlowFiles()
        {
            List<int> ids = new List<int>();
            int id;

            foreach (Tuple<object, object> turple in SQL.GetTwoOrder("Файл договора", "Id", "Название", "Порядок", true, "Договор_id", DocNumber))
            {
                id = (int)turple.Item1;
                UserControlFileEdit uc = new UserControlFileEdit(this, (string)turple.Item2, id);
                ids.Add(id);
                flowLayoutPanel1.Controls.Add(uc);
            }

            DATA["Файл договора"] = ids.ToArray();
        }

        public void ReadStages()
        {
            List<int> ids = new List<int>();

            foreach (Hashtable data in SQL.ReadAllMultiple("Этап договора", "Договор_id", DocNumber, "Номер"))
            {
                ids.Add((int)data["Id"]);
                //MessageBox.Show("lll = " + data["Id"].ToString());
                tabControlStages.Controls.Add(CreateNewStagePage(data));
            }

            if (ids.Count == 0)
                tabControlStages.Controls.Add(CreateNewStagePage(null));

            tabControlStages.Controls.Add(lastTabPage);

            DATA["Этап договора"] = ids.ToArray();
            //MessageBox.Show(((Array)DATA["Этап договора"]).ToString2());
        }

        public void NewStages()
        {
            tabControlStages.Controls.Add(CreateNewStagePage(null));
            tabControlStages.Controls.Add(lastTabPage);
        }

        public void ReadManyToMany(string where, string what, string arg1, int id1, string orderby = null)
        {
            DATA[where] = SQL.ReadManyToMany(where, what, arg1, id1, orderby);
        }

        public FormDocument(FormStart form1, int num = -1)
        {
            //Flow width = user control width + 27
            InitializeComponent();
            DocNumber = num;
            FormParent = form1;

            OwnTabControl.Initialize(tabControlMain);
            OwnTabControl.Initialize(tabControlStages);
            
            SQL.ReadToCombo(comboBox1, "Юридическое лицо", "Название", "Иностранный", false);
            SQL.ReadToList(listBox1, "Юридическое лицо", "Название", "Иностранный", true);
            SQL.ReadToList(listBox2, "Юридическое лицо", "Название");

            if (DocNumber != -1) {
                Text = "Редактирование Договора №" + DocNumber;

                DATA = SQL.ReadAll("Договор", "Номер", DocNumber);

                ReadManyToMany("Иностранный заказчик", "Юридическое лицо_id", "Договор_id", DocNumber);
                ReadManyToMany("Исполнитель договора", "Юридическое лицо_id", "Договор_id", DocNumber);
                //ReadManyToMany("Этап договора", "Id", "Договор_id", DocNumber, "Номер");

                FillAll();

                ReadFlowFiles();
                ReadStages();

                textBox1.ReadOnly = true;
                textBox1.BackColor = Global.ColorReadOnly;
            } else {
                Fill.DateTimePicker(dateTimePicker1, null);
                Fill.DateTimePicker(dateTimePicker2, null);
                NewStages();
                button2.Hide();
            }
        }

        public void SelectFileEdit(UserControlFileEdit uc)
        {
            if (uc == null)
            {
                if (FlowSelected != null)
                    FlowSelected.Highlight(false);
                FlowSelected = null;
                SetActiveMoveButtons(false);
            }
            else
            {
                //MessageBox.Show("111");
                //MessageBox.Show("FileId = " + uc.Id.ToString());
                if (FlowSelected != uc)
                {
                    //MessageBox.Show("FlowSelected != uc");
                    if (FlowSelected==null)
                        SetActiveMoveButtons(true);
                    else
                        FlowSelected.Highlight(false);
                    FlowSelected = uc;
                    uc.Highlight(true);
                }
                
            }

        }

        private void SaveManyToMany(int NUM, string where, int[] Sel, string arg1, string arg2)
        {
            if (DocNumber == -1)
                SQL.InsertManyToMany(NUM, where, Sel, arg1, arg2);
            else
            {
                //Insert = Sel.Except((int[])DATA[where]).ToArray();
                SQL.InsertManyToMany(NUM, where, Sel.Except((int[])DATA[where]).ToArray(), arg1, arg2);

                //Delete = ((int[])DATA[where]).Except(Sel).ToArray();
                SQL.DeleteManyToMany(NUM, where, ((int[])DATA[where]).Except(Sel).ToArray(), arg1, arg2);
            }
        }

        private void SaveFlowFiles(int NUM)
        {
            /*
            MERGE INTO [Файл договора]
                USING (
                        VALUES (4, 1, NULL, 'TESTER1SSS.txt', 1), 
                                (-1, 1, NULL, 'TESTER3.txt', 2)
                        ) AS source ([Id], [Договор_id], [Файл], [Название], [Порядок])
                    ON [Файл договора].[Id] = source.[Id]
            WHEN MATCHED THEN
                UPDATE SET [Название] = source.[Название], [Порядок] = source.[Порядок]
            WHEN NOT MATCHED THEN
                INSERT ([Договор_id], [Файл], [Название], [Порядок]) 
                    VALUES ([Договор_id], [Файл], [Название], [Порядок]);
            */

            List<int> Dels = new List<int>();

            int length = 0, i;

            foreach (UserControlFileEdit uc in flowLayoutPanel1.Controls)
            {
                if (uc.Remove)
                {
                    if (uc.Id != -1)
                        Dels.Add(uc.Id);
                }
                else
                    length++;
            }

            if (length > 0)
            {
                string query = "MERGE INTO [Файл договора] USING (";
                //"VALUES (@ID0,@NUM,@FILE0,@FILENAME0,@ORD0)";
                //string itos;

                i = 0;
                foreach (UserControlFileEdit uc in flowLayoutPanel1.Controls)
                {
                    if (!uc.Remove)
                    {
                        //if (i == 0)
                        //    query += "VALUES (@ID0,@NUM,@FILE0,@FILENAME0,@ORD0)";
                        //else
                        //{
                        //    itos = i.ToString();
                        //    query += ", (@ID" + itos + ",@NUM,@FILE" + itos + ",@FILENAME" + itos + ",@ORD" + itos + ")";
                        //}

                        if (i == 0)
                            query += "VALUES(";
                        else
                            query += ",(";

                        query += UserControlFileEdit.GetValueString(i) + ")";

                        i++;
                    }
                }

                query += ")AS source([Id],[Договор_id],[Файл],[Название],[Порядок]) " +
                    "ON [Файл договора].[Id]=source.[Id] " +
                    "WHEN MATCHED THEN " +
                    "UPDATE SET [Название] = source.[Название],[Порядок] = source.[Порядок] " +
                    "WHEN NOT MATCHED THEN " +
                    "INSERT([Договор_id],[Файл],[Название],[Порядок]) " +
                        "VALUES([Договор_id],[Файл],[Название],[Порядок]);";
                if (Dels.Count > 0)
                    query += "DELETE FROM [Файл договора] WHERE [Id] IN @DELS;";

                using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NUM", NUM);

                    if (Dels.Count > 0)
                        cmd.AddArrayParameters("@DELS", Dels);
                    
                    i = 0;
                    foreach (UserControlFileEdit uc in flowLayoutPanel1.Controls)
                    {
                        if (!uc.Remove)
                        {
                            //itos = i.ToString();

                            //cmd.Parameters.AddWithValue("@ID" + itos, uc.Id);

                            //cmd.Parameters.Add("@FILE" + itos, SqlDbType.Image);
                            //if (uc.Id==-1)
                            //    cmd.Parameters["@FILE" + itos].Value = CheckNull.File(uc.Data);
                            //else
                            //    cmd.Parameters["@FILE" + itos].Value = DBNull.Value;

                            //cmd.Parameters.AddWithValue("@FILENAME" + itos, uc.GetFileName());
                            //cmd.Parameters.AddWithValue("@ORD" + itos, i);

                            cmd.Parameters.AddUserControl(uc, i);

                            i++;
                        }
                    }

                    try
                    {
                        cmd.ExecuteNonQuery().ToString();
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
            else /* ------------- ELSE -------------*/
            {
                if (Dels.Count > 0)
                {
                    string query = "DELETE FROM [Файл договора] WHERE [Id] IN @DELS;";

                    using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.AddArrayParameters("@DELS", Dels);
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

        private void SaveStages(int NUM)
        {
            /*
            MERGE INTO [Этап договора]
                USING (
                        VALUES (4, 1, NULL, 'TESTER1SSS.txt', 1), 
                                (-1, 1, NULL, 'TESTER3.txt', 2)
                        ) AS source ([Id], [Договор_id], [Номер], [Начало работ], [Окончание работ], [Количество], 
                                    [Цена], [Модель цены], [Заключение], [Аванс], [Расчёт], [Плановая трудоёмкость], 
                                    [Фактическая трудоёмкость], [Текущее состояние], [Номер акта], [Номер удостоверения])
                    ON [Этап договора].[Id] = source.[Id]
            WHEN MATCHED THEN
                UPDATE SET [Номер] = source.[Номер], [Начало работ] = source.[Начало работ], [Окончание работ] = source.[Окончание работ], 
                           [Количество] = source.[Количество], [Цена] = source.[Цена], [Модель цены] = source.[Модель цены], 
                           [Заключение] = source.[Заключение], [Аванс] = source.[Аванс], [Расчёт] = source.[Расчёт], 
                           [Плановая трудоёмкость] = source.[Плановая трудоёмкость], [Фактическая трудоёмкость] = source.[Фактическая трудоёмкость], 
                           [Текущее состояние] = source.[Текущее состояние], [Номер акта] = source.[Номер акта], 
                           [Номер удостоверения] = source.[Номер удостоверения]
            WHEN NOT MATCHED THEN
                INSERT ([Договор_id], [Номер], [Начало работ], [Окончание работ], [Количество], 
                        [Цена], [Модель цены], [Заключение], [Аванс], [Расчёт], [Плановая трудоёмкость], 
                        [Фактическая трудоёмкость], [Текущее состояние], [Номер акта], [Номер удостоверения])
                    VALUES ([Договор_id], [Номер], [Начало работ], [Окончание работ], [Количество], 
                            [Цена], [Модель цены], [Заключение], [Аванс], [Расчёт], [Плановая трудоёмкость], 
                            [Фактическая трудоёмкость], [Текущее состояние], [Номер акта], [Номер удостоверения]);
            */

            int length = 0, i;

            List<int> Dels = ((IEnumerable)DATA["Этап договора"]).OfType<int>().ToList();

            foreach (OwnTabPage tab in tabControlStages.TabPages)
            {
                if (tab != lastTabPage)
                {
                    UserControlFormStage uc = (UserControlFormStage)tab.Controls[0];
                    if (uc.Id != -1)
                        Dels.Remove(uc.Id);
                    length++;
                }
            }

            if (length > 0)
            {
                string query = "MERGE INTO [Этап договора] USING (";

                i = 0;

                foreach (TabPage tab in tabControlStages.TabPages)
                {
                    if (tab != lastTabPage) 
                    {
                        UserControlFormStage uc = (UserControlFormStage)tab.Controls[0];
                        if (i == 0)
                            query += "VALUES(";
                        else
                            query += ",(";

                        query += UserControlFormStage.GetValueString(i) + ")";

                        i++;
                    }
                }

                query += ")AS source([Id],[Договор_id],[Номер],[Начало работ],[Окончание работ],[Количество]," +
                                    "[Цена],[Модель цены],[Заключение],[Аванс],[Расчёт],[Плановая трудоёмкость]," + 
                                    "[Фактическая трудоёмкость],[Текущее состояние],[Номер акта],[Номер удостоверения])" +
                    "ON [Этап договора].[Id]=source.[Id] " +
                    "WHEN MATCHED THEN UPDATE SET [Номер]=source.[Номер],[Начало работ]=source.[Начало работ],[Окончание работ]=source.[Окончание работ]," +
                           "[Количество]=source.[Количество],[Цена]=source.[Цена],[Модель цены]=source.[Модель цены]," +
                           "[Заключение]=source.[Заключение],[Аванс]=source.[Аванс],[Расчёт]=source.[Расчёт]," +
                           "[Плановая трудоёмкость]=source.[Плановая трудоёмкость],[Фактическая трудоёмкость]=source.[Фактическая трудоёмкость]," +
                           "[Текущее состояние]=source.[Текущее состояние],[Номер акта]=source.[Номер акта]," +
                           "[Номер удостоверения] = source.[Номер удостоверения] " +
                "WHEN NOT MATCHED THEN INSERT([Договор_id],[Номер],[Начало работ],[Окончание работ],[Количество]," +
                        "[Цена],[Модель цены],[Заключение],[Аванс],[Расчёт],[Плановая трудоёмкость]," +
                        "[Фактическая трудоёмкость],[Текущее состояние],[Номер акта],[Номер удостоверения])" +
                    "VALUES([Договор_id],[Номер],[Начало работ],[Окончание работ],[Количество]," +
                            "[Цена],[Модель цены],[Заключение],[Аванс],[Расчёт],[Плановая трудоёмкость]," +
                            "[Фактическая трудоёмкость],[Текущее состояние],[Номер акта],[Номер удостоверения]);";
                if (Dels.Count > 0)
                    query += "DELETE FROM [Этап договора] WHERE [Id] IN @DELS;";

                using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NUM", NUM);

                    if (Dels.Count > 0)
                        cmd.AddArrayParameters("@DELS", Dels);

                    i = 0;

                    foreach (TabPage tab in tabControlStages.TabPages)
                    {
                        if (tab != lastTabPage)
                        {
                            UserControlFormStage uc = (UserControlFormStage)tab.Controls[0];
                            cmd.Parameters.AddUserControl(uc, i);
                            i++;
                        }
                    }
                    try
                    {
                        cmd.ExecuteNonQuery().ToString();
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
            else // ------------- ELSE -------------
            {
                if (Dels.Count > 0)
                {
                    string query = "DELETE FROM [Этап договора] WHERE [Id] IN @DELS;";

                    using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.AddArrayParameters("@DELS", Dels);
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

        private bool ValidateDoc(out int NUM)
        {
            bool Check = true;

            if (DocNumber == -1)
            {
                Check &= Validator.Apply(label1, textBox1, typeof(uint));
                if (Check)
                {
                    NUM = Convert.ToInt32(textBox1.Text);
                    Check &= Validator.Apply(label1, textBox1, SQL.CheckUnique("Договор", "Номер", NUM));
                }
                else
                    NUM = -1;
            }
            else
                NUM = DocNumber;

            Check &= Validator.Apply(label2, comboBox1);
            Check &= Validator.Apply(labelDate1, dateTimePicker1);
            Check &= Validator.Apply(labelDate2, dateTimePicker2, dateTimePicker1);

            Check &= Validator.Apply(label10, textBox7, typeof(uint), true);
            Check &= Validator.Apply(label18, textBox15, typeof(uint), true);

            Check &= Validator.Apply(label13, textBox10, typeof(int), true);

            Check &= Validator.Apply(label11, textBox8, typeof(decimal), true);
            Check &= Validator.Apply(label12, textBox9, typeof(decimal), true);


            Check &= Validator.Apply(label14, textBox11, typeof(float), true);
            Check &= Validator.Apply(label15, textBox12, typeof(float), true);
            Check &= Validator.Apply(label16, textBox13, typeof(float), true);
            Check &= Validator.Apply(label17, textBox14, typeof(float), true);

            return Check;
        }

        private bool ValidateStages()
        {
            bool Check = true;

            foreach (OwnTabPage tab in tabControlStages.TabPages)
            {
                if (tab != lastTabPage)
                {
                    UserControlFormStage uc = (UserControlFormStage)tab.Controls[0];
                    Check &= Validator.Apply(tab, uc.ValidateStage());
                }
            }

            return Check;
        }

        private bool ValidateArchive()
        {
            bool Check = true;

            foreach (UserControlFileEdit uc in flowLayoutPanel1.Controls)
            {
                Check &= uc.ValidateFile();
            }

            return Check;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool Check = true;

            int NUM;

            Check &= Validator.Apply(tabContract, ValidateDoc(out NUM));
            Check &= Validator.Apply(tabStages, ValidateStages());
            Check &= Validator.Apply(tabArchive, ValidateArchive());

            //Refresh tabs after else

            //Check = false;
            if (Check)
            {
                using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    string query;

                    if (DocNumber == -1)
                        query = "INSERT INTO [Договор] VALUES (@NUM, @DOP, @GEN, @VID, @TEMA, @NAIM, " +
                            "@DATE1, @DATE2, @COUNT, @PRICE, @PRICE2, @MODEL, @VOL, @VOL2, @TRUD, @TRUD2, @PAGE, " +
                            "@VED, @PRI, " +
                            //"@IMG1, @IMGNAME1, @IMG2, @IMGNAME2, " + 
                            "@EDIT)";
                    else//
                        query = "UPDATE [Договор] SET " +
                            "[Дополнительное Соглашение]=@DOP, [Генеральный заказчик_id]=@GEN, " +
                            "[Вид работ]=@VID, [Тема]=@TEMA, [Наименование работ]=@NAIM, " +
                            "[Начало работ]=@DATE1, [Окончание работ]=@DATE2, " +
                            "[Количество]=@COUNT, [Цена]=@PRICE, [Цена за единицу]=@PRICE2, [Модель цены]=@MODEL, " +
                            "[Объём собственной работы]=@VOL, [Объём КА]=@VOL2, " +
                            "[Плановая трудоёмкость]=@TRUD, [Фактическая трудоёмкость]=@TRUD2, " +
                            "[Страница]=@PAGE, [Ведущий]=@VED, [Примечание]=@PRI, " +
                            //"[Изображение1]=@IMG1, [Имя изображения1]=@IMGNAME1, [Изображение2]=@IMG2, [Имя изображения2]=@IMGNAME2, " +
                            "[Редактируется]=@EDIT WHERE [Номер]=@NUM";

                    /*
                    query = "UPDATE [Договор] SET " +
                        "[Генеральный Заказчик_id]=@GEN, [Ведущий]=@VED, [Примечание]=@PRI, " +
                        "[Вид Работ]=@VID, [Тема]=@TEMA, [Наименование Работ]=@NAIM, " +
                        "[Начало Работ]=@DATE1, [Окончание Работ]=@DATE2, " +
                        "[Количество]=@COUNT, [Цена]=@PRICE, [Цена За Единицу]=@PRICE2, [Модель Цены]=@MODEL, " +
                        "[Объём Собственной Работы]=@VOL, [Объём КА]=@VOL2, " +
                        "[Плановая Трудоёмкость]=@TRUD, [Фактическая Трудоёмкость]=@TRUD2, [Страница]=@PAGE, " +
                        "[Имя Изображения1]=@IMGNAME1, [Имя Изображения2]=@IMGNAME2, [Изображение1]=@IMG1, [Изображение2]=@IMG2, " +
                        "[Дополнительное Соглашение]=@DOP " +
                        "WHERE [Номер]=@NUM";
                    */


                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@NUM", NUM);
                            cmd.Parameters.AddWithValue("@DOP", CheckNull.String(textBox16.Text));
                            cmd.Parameters.AddWithValue("@GEN", CheckNull.Combo(comboBox1));
                            cmd.Parameters.AddWithValue("@VID", CheckNull.String(textBox2.Text));
                            cmd.Parameters.AddWithValue("@TEMA", CheckNull.String(textBox5.Text));
                            cmd.Parameters.AddWithValue("@NAIM", CheckNull.String(textBox6.Text));
                            cmd.Parameters.AddWithValue("@DATE1", CheckNull.DateTime(dateTimePicker1.Value));
                            cmd.Parameters.AddWithValue("@DATE2", CheckNull.DateTime(dateTimePicker2.Value));
                            cmd.Parameters.AddWithValue("@COUNT", CheckNull.Int(textBox7.Text));
                            //cmd.Parameters.Add("@COUNT", SqlDbType.Int);
                            //cmd.Parameters["@COUNT"].Value = CheckNull.UInt(textBox7.Text);
                            cmd.Parameters.AddWithValue("@PRICE", CheckNull.Decimal(textBox8.Text));
                            cmd.Parameters.AddWithValue("@PRICE2", CheckNull.Decimal(textBox9.Text));
                            cmd.Parameters.AddWithValue("@MODEL", CheckNull.Int(textBox10.Text));
                            cmd.Parameters.AddWithValue("@VOL", CheckNull.Float(textBox11.Text));
                            cmd.Parameters.AddWithValue("@VOL2", CheckNull.Float(textBox12.Text));
                            cmd.Parameters.AddWithValue("@TRUD", CheckNull.Float(textBox13.Text));
                            cmd.Parameters.AddWithValue("@TRUD2", CheckNull.Float(textBox14.Text));
                            cmd.Parameters.AddWithValue("@PAGE", CheckNull.Int(textBox15.Text));
                            cmd.Parameters.AddWithValue("@VED", CheckNull.String(textBox3.Text));
                            cmd.Parameters.AddWithValue("@PRI", CheckNull.String(textBox4.Text));



                            cmd.Parameters.AddWithValue("@EDIT", false);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                            FormParent.RefreshDocs();
                        }
                    }
                }
                SaveManyToMany(NUM, "Иностранный заказчик", listBox1.GetListBoxSelected(), "Договор_id", "Юридическое лицо_id");
                SaveManyToMany(NUM, "Исполнитель договора", listBox2.GetListBoxSelected(), "Договор_id", "Юридическое лицо_id");


                SaveFlowFiles(NUM);
                SaveStages(NUM);


                this.Close();
            }
            else
                tabControlMain.Refresh();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить договор?",
                                         "Удаление",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM [Договор] WHERE [Номер]=@ID";

                using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", DocNumber);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                FormParent.RefreshDocs();
                Close();
            }
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Long;
        }

        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value == Global.MinDate)
            {
                dateTimePicker1.Value = DateTime.Now;
                SendKeys.Send("%{DOWN}");
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Long;
        }

        private void dateTimePicker2_DropDown(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value == Global.MinDate)
            {
                dateTimePicker2.Value = DateTime.Now;
                SendKeys.Send("%{DOWN}");
            }
        }

        

        private void SetActiveMoveButtons(bool b)
        {
            buttonUp.Enabled = b;
            buttonDown.Enabled = b;
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            if (FlowSelected != null)
                FlowSelected.Highlight(false);
            FlowSelected = null;
            SetActiveMoveButtons(false);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (FlowSelected!=null) {
                int selectedIndex = flowLayoutPanel1.Controls.GetChildIndex(FlowSelected); ;
                if (selectedIndex > 0) {
                    flowLayoutPanel1.Controls.SetChildIndex(flowLayoutPanel1.Controls[selectedIndex - 1], selectedIndex);
                }
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (FlowSelected != null)
            {
                int selectedIndex = flowLayoutPanel1.Controls.GetChildIndex(FlowSelected); ;
                if (selectedIndex < flowLayoutPanel1.Controls.Count-1)
                {
                    flowLayoutPanel1.Controls.SetChildIndex(flowLayoutPanel1.Controls[selectedIndex + 1], selectedIndex);
                }
            }
        }

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Global.LastDirectoryPath;
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog1.FileName != null)
                    {
                        AddFlowFile(openFileDialog1.FileName);
                    }
                    Global.LastDirectoryPath = Path.GetDirectoryName(openFileDialog1.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddFlowFile(string path)
        {
            UserControlFileEdit newControl = new UserControlFileEdit(this, Path.GetFileName(path));
            newControl.Data = Extensions.LoadFile(path);
            //newControl.AddImageSet(-1, Path.GetFileName(path));
            flowLayoutPanel1.Controls.Add(newControl);
        }


        private void tabControlStages_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (tabControlStages.TabCount > 2)
                    for (int i = 0; i < tabControlStages.TabCount; ++i)
                    {
                        if (tabControlStages.GetTabRect(i).Contains(e.Location))
                        {
                            TabPage tab = tabControlStages.TabPages[i];

                            if (tab != lastTabPage)
                            {
                                var result = MessageBox.Show("Вы действительно хотите удалить \"" + tab.Text + "\"?",
                                         "Удаление этапа",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                    tabControlStages.TabPages.Remove(tab);
                            }
                        }
                    }
            }
        }

        private void tabControlStages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlStages.SelectedTab == lastTabPage)
            {
                TabPage page = CreateNewStagePage();

                tabControlStages.TabPages.Insert(tabControlStages.SelectedIndex, page);
                tabControlStages.SelectedTab = page;
            }

        }

        public void ReorderStageTab(TabPage insertTab, int num)
        {
            if (tabControlStages.TabCount > 2)
            {
                int i = 0;
                int c = 0;

                foreach (TabPage tab in tabControlStages.TabPages)
                {
                    if (tab == lastTabPage)
                    {
                        tabControlStages.TabPages.Remove(insertTab);
                        tabControlStages.TabPages.Insert(c, insertTab);
                        tabControlStages.SelectTab(insertTab);
                        break;
                    }
                    else
                    {
                        if (insertTab != tab)
                        {
                            UserControlFormStage uc = (UserControlFormStage)tab.Controls[0];
                            if (uc.validnum)
                            {
                                c = i;
                                if (Convert.ToInt32(uc.number) >= num)
                                {
                                    tabControlStages.TabPages.Remove(insertTab);
                                    tabControlStages.TabPages.Insert(c, insertTab);
                                    tabControlStages.SelectTab(insertTab);
                                    break;
                                }
                                else
                                    c++;
                            }
                            i++;
                        }
                    }

                }
            }
        }

    }
}