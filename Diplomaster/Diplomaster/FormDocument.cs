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

        //public bool RemoveImage1 = false;
        //public bool RemoveImage2 = false;
        //public int FlowSelected = -1;
        public UserControlFileEdit FlowSelected;
        
        public void ReadCombo(ComboBox combo, string where, string what, string arg1 = null, object val1 = null)
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

        public void ReadList(ListBox list, string where, string what, string arg1 = null, object val1 = null)
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

        /*
        public void LoadList(int number)
        {
            string query = "SELECT [id], [Название] FROM [Заказчик] WHERE [Иностранный] = @F";// WHERE [Иностранный]=0
            
            if (number == -1) {
                using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@F", true);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (rdr.Read())
                            {
                                BoxItem item = new BoxItem();

                                item.Text = rdr.GetString(1);
                                item.Value = rdr.GetInt32(0);

                                listBox1.Items.Add(item);
                            }
                        }
                        finally
                        {
                            rdr.Close();
                            conn.Close();
                        }
                    }
                }
            } else {
                string query2 = "SELECT [Заказчик] FROM [Инозаказчик] WHERE [Номер Договора]=@NUM";

                Hashtable hash = new Hashtable();

                using (SqlConnection conn = new SqlConnection(Global.ConnectionString)) {
                    conn.Open();

                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@NUM", number);

                    using (SqlDataReader rdr = cmd2.ExecuteReader()) {
                        try {
                            while (rdr.Read())
                                hash.Add(rdr.GetInt32(0), true);
                        }
                        finally
                        {
                            rdr.Close();
                        }
                    }



                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@F", true);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            int ind, i = 0;
                            while (rdr.Read())
                            {
                                BoxItem item = new BoxItem();

                                ind = rdr.GetInt32(0);
                                item.Text = rdr.GetString(1);
                                item.Value = ind;

                                listBox1.Items.Add(item);

                                if (hash[ind] != null)
                                    listBox1.SetSelected(i, true);
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
        }
        */

        public void ReadDoc()
        {
            string query = "SELECT * FROM [Договор] WHERE [Номер]=@NUM";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NUM", DocNumber);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                //DATA
                                //MessageBox.Show(Convert.ToString(rdr.FieldCount));
                                //string str = "";
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    //str += "[" + rdr.GetName(i) + "] " + Convert.ToString(rdr.GetValue(i))+"\n";
                                    DATA.Add(rdr.GetName(i), rdr.GetValue(i));
                                    
                                    //if (rdr.GetName(i) == "Изображение1")
                                        //MessageBox.Show(rdr.GetName(i) + " = " + Encoding.Default.GetString((byte[])rdr.GetValue(i)));
                                    //if (rdr.GetName(i) == "Начало Работ")
                                    //MessageBox.Show(rdr.GetName(i) + " = " + rdr.GetValue(i).ToString());
                                    //MessageBox.Show("["+rdr.GetName(i)+"]" + " = " + DATA[rdr.GetName(i)].ToString());
                                    //MessageBox.Show("[" + rdr.GetName(i) + "]" + "(" + DATA[rdr.GetName(i)].GetType()+")"+" = " + DATA[rdr.GetName(i)].ToString());
                                }

                                //MessageBox.Show(str);
                                //MessageBox.Show(Convert.ToString(DATA["Изображение1"].GetType()));
                                //ReadElement(rdr);
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

        public void FillTextBox(TextBox textbox, object val)
        {
            if (val.isnull())
                textbox.Text = "";
            else
                textbox.Text = (string)val;
        }

        public void FillTextBoxInt(TextBox textbox, object val)
        {
            if (val.isnull())
                textbox.Text = "";
            else
                textbox.Text = Convert.ToString((int)val);
        }

        public void FillTextBoxFloat(TextBox textbox, object val)
        {
            if (val.isnull())
                textbox.Text = "";
            else
                textbox.Text = Convert.ToString((float)Convert.ToDecimal(val));
        }

        public void FillTextBoxDecimal(TextBox textbox, object val)
        {
            if (val.isnull())
                textbox.Text = "";
            else
                textbox.Text = Convert.ToString((decimal)val);
        }
        
        public void FillDateTimePicker(DateTimePicker picker, object val)
        {
            if (val.isnull())
            {
                picker.Value = Global.MinDate;
                picker.Format = DateTimePickerFormat.Custom;
                picker.CustomFormat = " ";
            }
            else
                picker.Value = (DateTime)val;
        }

        //public void FillImageSet(TextBox textBoxName, TextBox textBoxPath, Button chooseButton, Button downloadButton, Button deleteButton, object data, object filename)
        //{

        //    if (data.isnull()) {
        //        textBoxName.Text = "";
        //        downloadButton.Enabled = false;
        //        deleteButton.Enabled = false;
        //    } else {
        //        textBoxName.Text = (string)filename;
        //        downloadButton.Enabled = true;
        //        deleteButton.Enabled = true;
        //    }
        //}

        public void FillAll()
        {
            textBox1.Text = Convert.ToString(DocNumber);

            SelectCombo(comboBox1, DATA["Генеральный заказчик_id"]);
            SelectList(listBox1, DATA["Иностранный заказчик"]);
            SelectList(listBox2, DATA["Исполнитель договора"]);

            FillTextBox(textBox16, DATA["Дополнительное соглашение"]);
            FillTextBox(textBox2, DATA["Вид работ"]);
            FillTextBox(textBox5, DATA["Тема"]);
            FillTextBox(textBox6, DATA["Наименование работ"]);
            FillTextBoxInt(textBox7, DATA["Количество"]);
            FillTextBoxDecimal(textBox8, DATA["Цена"]);
            FillTextBoxDecimal(textBox9, DATA["Цена за единицу"]);
            FillTextBoxInt(textBox10, DATA["Модель цены"]);
            FillTextBoxFloat(textBox11, DATA["Объём собственной работы"]);
            FillTextBoxFloat(textBox12, DATA["Объём КА"]);
            FillTextBoxFloat(textBox13, DATA["Плановая трудоёмкость"]);
            FillTextBoxFloat(textBox14, DATA["Фактическая трудоёмкость"]);
            FillTextBoxInt(textBox15, DATA["Страница"]);
            FillTextBox(textBox3, DATA["Ведущий"]);
            FillTextBox(textBox4, DATA["Примечание"]);

            //FillImageSet(textBoxImg1N, textBoxImg1, buttonImg1_1, buttonImg1_2, buttonImg1_3, DATA["Изображение1"], DATA["Имя изображения1"]);
            //FillImageSet(textBoxImg2N, textBoxImg2, buttonImg2_1, buttonImg2_2, buttonImg2_3, DATA["Изображение2"], DATA["Имя изображения2"]);

            FillDateTimePicker(dateTimePicker1, DATA["Начало работ"]);
            FillDateTimePicker(dateTimePicker2, DATA["Окончание работ"]);
        }

        public int[] ReadToArray(SqlCommand cmd)
        {
            int i = 0;
            int[] arr = { };
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Array.Resize(ref arr, arr.Length + 1);
                    arr[i] = (Convert.ToInt32(rdr.GetInt32(0)));
                    i++;
                }

                rdr.Close();
            }
            return arr;
        }

        public int[] GetListBoxSelected(ListBox listBox)
        {
            int[] Set = { };
            int i = 0, c = 0;
            foreach (BoxItem element in listBox.Items)
            {
                if (listBox.GetSelected(i))
                {
                    Array.Resize(ref Set, Set.Length + 1);
                    Set[c] = (Convert.ToInt32(element.Value));
                    c++;
                }
                i++;
            }
            return Set;
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
                        DATA[where] = ReadToArray(cmd);
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

        public void FillFlowFiles()
        {
            string query = "SELECT [Id],[Название] FROM [Файл договора] WHERE [Договор_id]=@NUM ORDER BY [Порядок] ASC";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NUM", DocNumber);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            int i = 0;
                            while (rdr.Read())
                            {
                                UserControlFileEdit uc = new UserControlFileEdit(this);
                                //uc.Name = "UserControlFileEdit" + i.ToString();
                                uc.AddImageSet(rdr.GetInt32(0), rdr.GetString(1));
                                flowLayoutPanel1.Controls.Add(uc);
                                i++;
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

        }


        public FormDocument(FormStart form1, int num = -1)
        {
            //Flow width = user control width + 27
            InitializeComponent();
            DocNumber = num;
            FormParent = form1;

            ReadCombo(comboBox1, "Юридическое лицо", "Название", "Иностранный", false);
            ReadList(listBox1, "Юридическое лицо", "Название", "Иностранный", true);
            ReadList(listBox2, "Юридическое лицо", "Название");

            if (DocNumber != -1) {
                Text = "Редактирование Договора №" + DocNumber;

                ReadDoc();

                ReadFrom("Иностранный заказчик", "Юридическое лицо_id", "Договор_id");
                ReadFrom("Исполнитель договора", "Юридическое лицо_id", "Договор_id");

                FillAll();

                FillFlowFiles();

                textBox1.ReadOnly = true;
                textBox1.BackColor = Global.ColorReadOnly;
            } else {
                FillDateTimePicker(dateTimePicker1, null);
                FillDateTimePicker(dateTimePicker2, null);
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


        private void SaveManyMany(SqlConnection conn, int NUM, string where, ListBox list, string arg1, string arg2)
        {
            int[] Sel = { };
            int[] Insert = { };
            int[] Delete = { };

            Sel = GetListBoxSelected(list);

            if (DocNumber == -1)
                Insert = Sel;
            else {
                Insert = Sel.Except((int[])DATA[where]).ToArray();
                Delete = ((int[])DATA[where]).Except(Sel).ToArray();
            }

            
            //MessageBox.Show("DATA[\"Инозаказчик\"] ---> " + ((int[])DATA["Инозаказчик"]).ToString2());
            //MessageBox.Show("Insert ---> " + Insert.ToString2());
            //MessageBox.Show("Delete ---> " + Delete.ToString2());

            if (Insert.Length > 0)
            {
                string query2 = "INSERT INTO [" + where + "] ([" + arg1 + "], [" + arg2 + "]) VALUES (@NUM, @VAL)";

                using (SqlCommand cmd = new SqlCommand(query2, conn))
                {
                    cmd.Parameters.AddWithValue("@NUM", NUM);

                    foreach (int val in Insert)
                    {
                        if (cmd.Parameters.Contains("@VAL"))
                            cmd.Parameters["@VAL"].Value = val;
                        else
                            cmd.Parameters.AddWithValue("@VAL", val);

                        try
                        {
                            conn.Open();
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

            if (Delete.Length > 0)
            {
                string query3 = "DELETE FROM [" + where + "] WHERE [" + arg1 + "] = @NUM AND [" + arg2 + "]= @VAL";

                using (SqlCommand cmd = new SqlCommand(query3, conn))
                {
                    cmd.Parameters.AddWithValue("@NUM", DocNumber);

                    foreach (int val in Delete)
                    {
                        if (cmd.Parameters.Contains("@VAL"))
                            cmd.Parameters["@VAL"].Value = val;
                        else
                            cmd.Parameters.AddWithValue("@VAL", val);

                        try
                        {
                            conn.Open();
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



        //public byte[] LoadFile(string path) {
        //    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
        //        int numBytesToRead = (int)fs.Length;
        //        int numBytesRead = 0;
        //        byte[] data = new byte[fs.Length];

        //        while (numBytesToRead > 0) {
        //            int n = fs.Read(data, numBytesRead, numBytesToRead);
        //            if (n == 0)
        //                break;

        //            numBytesRead += n;
        //            numBytesToRead -= n;
        //        }
        //        return data;
        //    }
        //}

        //public void SaveFile(string path, byte[] data)
        //{
        //    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        //    {
        //        fs.Write(data, 0, data.Length);
        //    }
        //}

        private bool CheckUniqueNumber(int number)
        {
            int count = 0;

            string query = "SELECT COUNT (*) FROM [Договор] WHERE [Номер] = @NUM";

            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NUM", number);

                count = (Int32)cmd.ExecuteScalar();
            }

            return count==0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool Check = true;

            int NUM;

            if (DocNumber == -1)
            {
                Check &= Validator.Apply(label1, textBox1, typeof(uint));
                if (Check)
                    Check &= Validator.Apply(label1, textBox1, CheckUniqueNumber(Convert.ToInt32(textBox1.Text)));
            }
            Check &= Validator.Apply(label2, comboBox1);
            Check &= Validator.Apply(labelDate1, dateTimePicker1);
            Check &= Validator.Apply(labelDate2, dateTimePicker2, dateTimePicker1);
            
            //if (!RemoveImage1)
            //    Check &= Validator.Apply(labelImg1, textBoxImg1, typeof(File));
            //if (!RemoveImage2)
            //    Check &= Validator.Apply(labelImg2, textBoxImg2, typeof(File));

            Check &= Validator.Apply(label10, textBox7, typeof(uint), true);
            Check &= Validator.Apply(label18, textBox15, typeof(uint), true);

            Check &= Validator.Apply(label13, textBox10, typeof(int), true);

            Check &= Validator.Apply(label11, textBox8, typeof(decimal), true);
            Check &= Validator.Apply(label12, textBox9, typeof(decimal), true);
            

            Check &= Validator.Apply(label14, textBox11, typeof(float), true);
            Check &= Validator.Apply(label15, textBox12, typeof(float), true);
            Check &= Validator.Apply(label16, textBox13, typeof(float), true);
            Check &= Validator.Apply(label17, textBox14, typeof(float), true);
            
            //Check = false;
            if (Check)
            {
                if (DocNumber == -1)
                    NUM = Convert.ToInt32(textBox1.Text);
                else
                    NUM = DocNumber;
                using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    string query;

                    if (DocNumber == -1)
                        query = "INSERT INTO [Договор] VALUES (@NUM, @DOP, @GEN, @VID, @TEMA, @NAIM, " +
                            "@DATE1, @DATE2, @COUNT, @PRICE, @PRICE2, @MODEL, @VOL, @VOL2, @TRUD, @TRUD2, @PAGE, "+
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

                            
                            //if (RemoveImage1)
                            //{
                            //    cmd.Parameters.Add("@IMG1", SqlDbType.Image);
                            //    cmd.Parameters["@IMG1"].Value = DBNull.Value;
                            //    cmd.Parameters.AddWithValue("@IMGNAME1", DBNull.Value);

                            //}
                            //else
                            //{
                            //    if (textBoxImg1.Text == "")
                            //    {
                            //        if (textBoxImg1N.Text == "")
                            //        {
                            //            cmd.Parameters.Add("@IMG1", SqlDbType.Image);
                            //            cmd.Parameters["@IMG1"].Value = DBNull.Value;
                            //            cmd.Parameters.AddWithValue("@IMGNAME1", DBNull.Value);
                            //        }
                            //        else
                            //        {
                            //            cmd.Parameters.AddWithValue("@IMG1", DATA["Изображение1"]);
                            //            cmd.Parameters.AddWithValue("@IMGNAME1", DATA["Имя изображения1"]);
                            //        }
                            //    }
                            //    else
                            //    {
                            //        byte[] Image1 = LoadFile(textBoxImg1.Text);
                            //        cmd.Parameters.AddWithValue("@IMG1", CheckNull.File(Image1));
                            //        cmd.Parameters.AddWithValue("@IMGNAME1", CheckNull.FileName(Image1, Path.GetFileName(textBoxImg1.Text)));
                            //    }
                            //}



                            //if (RemoveImage2)
                            //{
                            //    cmd.Parameters.Add("@IMG2", SqlDbType.Image);
                            //    cmd.Parameters["@IMG2"].Value = DBNull.Value;
                            //    cmd.Parameters.AddWithValue("@IMGNAME2", DBNull.Value);
                            //}
                            //else
                            //{
                            //    if (textBoxImg2.Text == "")
                            //    {
                            //        if (textBoxImg2N.Text == "")
                            //        {
                            //            cmd.Parameters.Add("@IMG2", SqlDbType.Image);
                            //            cmd.Parameters["@IMG2"].Value = DBNull.Value;
                            //            cmd.Parameters.AddWithValue("@IMGNAME2", DBNull.Value);
                            //        }
                            //        else
                            //        {
                            //            cmd.Parameters.AddWithValue("@IMG2", DATA["Изображение2"]);
                            //            cmd.Parameters.AddWithValue("@IMGNAME2", DATA["Имя изображения2"]);
                            //        }
                            //    }
                            //    else
                            //    {
                            //        byte[] Image2 = LoadFile(textBoxImg2.Text);
                            //        cmd.Parameters.AddWithValue("@IMG2", CheckNull.File(Image2));
                            //        cmd.Parameters.AddWithValue("@IMGNAME2", CheckNull.FileName(Image2, Path.GetFileName(textBoxImg2.Text)));
                            //    }
                            //}

                            
                            //cmd.Parameters.Add("@IMG1", SqlDbType.Image);
                            //cmd.Parameters["@IMG1"].Value = DBNull.Value;
                            //cmd.Parameters.AddWithValue("@IMGNAME1", DBNull.Value);

                            //cmd.Parameters.Add("@IMG2", SqlDbType.Image);
                            //cmd.Parameters["@IMG2"].Value = DBNull.Value;
                            //cmd.Parameters.AddWithValue("@IMGNAME2", DBNull.Value);

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

                    SaveManyMany(conn, NUM, "Иностранный заказчик", listBox1, "Договор_id", "Юридическое лицо_id");
                    SaveManyMany(conn, NUM, "Исполнитель договора", listBox2, "Договор_id", "Юридическое лицо_id");

                }
                this.Close();
            }
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

        //private void buttonImg1_1_Click(object sender, EventArgs e)
        //{
        //    if (textBoxImg1.Text == "")
        //    {
        //        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        //        openFileDialog1.InitialDirectory = Global.LastDirectoryPath;
        //        openFileDialog1.Filter = "All files (*.*)|*.*";
        //        openFileDialog1.RestoreDirectory = true;

        //        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //        {
        //            try
        //            {
        //                if (openFileDialog1.FileName != null)
        //                {
        //                    textBoxImg1.Text = openFileDialog1.FileName;
        //                    buttonImg1_1.Text = "Отмена";
        //                }
        //                Global.LastDirectoryPath = Path.GetDirectoryName(openFileDialog1.FileName);
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        textBoxImg1.Text = "";
        //        buttonImg1_1.Text = "Обзор";
        //    }
        //}

        //private void buttonImg1_2_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        //    saveFileDialog1.InitialDirectory = Global.LastDirectoryPath;
        //    saveFileDialog1.Filter = "All files (*.*)|*.*";
        //    saveFileDialog1.RestoreDirectory = true;
        //    saveFileDialog1.FileName = (string)DATA["Имя изображения1"];

        //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        try
        //        {
        //            SaveFile(saveFileDialog1.FileName, (byte[])DATA["Изображение1"]);
        //            Global.LastDirectoryPath = Path.GetDirectoryName(saveFileDialog1.FileName);

        //            var result = MessageBox.Show("Открыть папку с файлом?",
        //                                 "Показать файл",
        //                                 MessageBoxButtons.YesNo,
        //                                 MessageBoxIcon.Question);

        //            if (result == DialogResult.Yes)
        //            {
        //                ProcessStartInfo info = new ProcessStartInfo();
        //                info.FileName = "explorer";
        //                info.Arguments = string.Format("/e, /select, \"{0}\"", saveFileDialog1.FileName);
        //                Process.Start(info);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}

        //private void buttonImg1_3_Click(object sender, EventArgs e)
        //{
        //    RemoveImage1 = !RemoveImage1;
        //    if (RemoveImage1)
        //    {
        //        buttonImg1_3.Text = "Восстановить";
        //        buttonImg1_1.Enabled = false;
        //        buttonImg1_2.Enabled = false;
        //        textBoxImg1.Enabled = false;
        //        textBoxImg1N.Enabled = false;
        //    }
        //    else
        //    {
        //        buttonImg1_3.Text = "Удалить";
        //        buttonImg1_1.Enabled = true;
        //        buttonImg1_2.Enabled = true;
        //        textBoxImg1.Enabled = true;
        //        textBoxImg1N.Enabled = true;
        //    }
        //}

        //private void buttonImg2_1_Click(object sender, EventArgs e)
        //{
        //    if (textBoxImg2.Text == "")
        //    {
        //        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        //        openFileDialog1.InitialDirectory = Global.LastDirectoryPath;
        //        openFileDialog1.Filter = "All files (*.*)|*.*";
        //        openFileDialog1.RestoreDirectory = true;

        //        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //        {
        //            try
        //            {
        //                if (openFileDialog1.FileName != null)
        //                {
        //                    textBoxImg2.Text = openFileDialog1.FileName;
        //                    buttonImg2_1.Text = "Отмена";
        //                }
        //                Global.LastDirectoryPath = Path.GetDirectoryName(openFileDialog1.FileName);

        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        textBoxImg2.Text = "";
        //        buttonImg2_1.Text = "Обзор";
        //    }
        //}

        //private void buttonImg2_2_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        //    saveFileDialog1.InitialDirectory = Global.LastDirectoryPath;
        //    saveFileDialog1.Filter = "All files (*.*)|*.*";
        //    saveFileDialog1.RestoreDirectory = true;
        //    saveFileDialog1.FileName = (string)DATA["Имя изображения2"];

        //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        try
        //        {
        //            SaveFile(saveFileDialog1.FileName, (byte[])DATA["Изображение2"]);
        //            Global.LastDirectoryPath = Path.GetDirectoryName(saveFileDialog1.FileName);

        //            var result = MessageBox.Show("Открыть папку с файлом?",
        //                                 "Показать файл",
        //                                 MessageBoxButtons.YesNo,
        //                                 MessageBoxIcon.Question);

        //            if (result == DialogResult.Yes)
        //            {
        //                ProcessStartInfo info = new ProcessStartInfo();
        //                info.FileName = "explorer";
        //                info.Arguments = string.Format("/e, /select, \"{0}\"", saveFileDialog1.FileName);
        //                Process.Start(info);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}

        //private void buttonImg2_3_Click(object sender, EventArgs e)
        //{
        //    RemoveImage2 = !RemoveImage2;
        //    if (RemoveImage2)
        //    {
        //        buttonImg2_3.Text = "Восстановить";
        //        buttonImg2_1.Enabled = false;
        //        buttonImg2_2.Enabled = false;
        //        textBoxImg2.Enabled = false;
        //        textBoxImg2N.Enabled = false;
        //    }
        //    else
        //    {
        //        buttonImg2_3.Text = "Удалить";
        //        buttonImg2_1.Enabled = true;
        //        buttonImg2_2.Enabled = true;
        //        textBoxImg2.Enabled = true;
        //        textBoxImg2N.Enabled = true;
        //    }
        //}


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
                if (selectedIndex < flowLayoutPanel1.Controls.Count)
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
                        AddFile(openFileDialog1.FileName);
                    }
                    Global.LastDirectoryPath = Path.GetDirectoryName(openFileDialog1.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void AddFile(string path)
        {
            UserControlFileEdit newControl = new UserControlFileEdit(this, true);
            newControl.Data = Extensions.LoadFile(path);
            newControl.AddImageSet(-1, Path.GetFileName(path));
            flowLayoutPanel1.Controls.Add(newControl);
        }

        

    }
}