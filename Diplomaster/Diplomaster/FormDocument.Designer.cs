namespace Diplomaster
{
    partial class FormDocument
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabArchive = new Diplomaster.OwnTabPage();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabStages = new Diplomaster.OwnTabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabContract = new Diplomaster.OwnTabPage();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.labelDate2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDate1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabArchive.SuspendLayout();
            this.tabStages.SuspendLayout();
            this.tabContract.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(239, 526);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(395, 526);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Удалить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabArchive
            // 
            this.tabArchive.BackColor = System.Drawing.SystemColors.Control;
            this.tabArchive.Controls.Add(this.buttonAddFile);
            this.tabArchive.Controls.Add(this.buttonDown);
            this.tabArchive.Controls.Add(this.buttonUp);
            this.tabArchive.Controls.Add(this.flowLayoutPanel1);
            this.tabArchive.Location = new System.Drawing.Point(4, 22);
            this.tabArchive.Name = "tabArchive";
            this.tabArchive.Padding = new System.Windows.Forms.Padding(3);
            this.tabArchive.Size = new System.Drawing.Size(900, 494);
            this.tabArchive.TabColor = System.Drawing.SystemColors.Control;
            this.tabArchive.TabIndex = 1;
            this.tabArchive.Text = "Архив";
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Location = new System.Drawing.Point(599, 6);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFile.TabIndex = 21;
            this.buttonAddFile.Text = "Добавить";
            this.buttonAddFile.UseVisualStyleBackColor = true;
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddFile_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Enabled = false;
            this.buttonDown.Font = new System.Drawing.Font("Wingdings 3", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonDown.Location = new System.Drawing.Point(563, 42);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(30, 30);
            this.buttonDown.TabIndex = 20;
            this.buttonDown.Text = "q";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Enabled = false;
            this.buttonUp.Font = new System.Drawing.Font("Wingdings 3", 16F);
            this.buttonUp.Location = new System.Drawing.Point(563, 6);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(30, 30);
            this.buttonUp.TabIndex = 19;
            this.buttonUp.Text = "p";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(537, 494);
            this.flowLayoutPanel1.TabIndex = 18;
            this.flowLayoutPanel1.Click += new System.EventHandler(this.flowLayoutPanel1_Click);
            // 
            // tabStages
            // 
            this.tabStages.BackColor = System.Drawing.SystemColors.Control;
            this.tabStages.Controls.Add(this.tabControl2);
            this.tabStages.Location = new System.Drawing.Point(4, 22);
            this.tabStages.Name = "tabStages";
            this.tabStages.Padding = new System.Windows.Forms.Padding(3);
            this.tabStages.Size = new System.Drawing.Size(900, 494);
            this.tabStages.TabColor = System.Drawing.SystemColors.Control;
            this.tabStages.TabIndex = 2;
            this.tabStages.Text = "Этапы";
            // 
            // tabControl2
            // 
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(588, 320);
            this.tabControl2.TabIndex = 0;
            // 
            // tabContract
            // 
            this.tabContract.BackColor = System.Drawing.SystemColors.Control;
            this.tabContract.Controls.Add(this.dateTimePicker1);
            this.tabContract.Controls.Add(this.textBox1);
            this.tabContract.Controls.Add(this.textBox3);
            this.tabContract.Controls.Add(this.textBox2);
            this.tabContract.Controls.Add(this.textBox16);
            this.tabContract.Controls.Add(this.textBox5);
            this.tabContract.Controls.Add(this.textBox6);
            this.tabContract.Controls.Add(this.textBox7);
            this.tabContract.Controls.Add(this.textBox8);
            this.tabContract.Controls.Add(this.textBox9);
            this.tabContract.Controls.Add(this.textBox10);
            this.tabContract.Controls.Add(this.textBox11);
            this.tabContract.Controls.Add(this.textBox4);
            this.tabContract.Controls.Add(this.textBox12);
            this.tabContract.Controls.Add(this.textBox15);
            this.tabContract.Controls.Add(this.textBox13);
            this.tabContract.Controls.Add(this.textBox14);
            this.tabContract.Controls.Add(this.labelDate2);
            this.tabContract.Controls.Add(this.label1);
            this.tabContract.Controls.Add(this.labelDate1);
            this.tabContract.Controls.Add(this.label2);
            this.tabContract.Controls.Add(this.dateTimePicker2);
            this.tabContract.Controls.Add(this.label3);
            this.tabContract.Controls.Add(this.label7);
            this.tabContract.Controls.Add(this.label19);
            this.tabContract.Controls.Add(this.label8);
            this.tabContract.Controls.Add(this.label9);
            this.tabContract.Controls.Add(this.label10);
            this.tabContract.Controls.Add(this.label11);
            this.tabContract.Controls.Add(this.listBox2);
            this.tabContract.Controls.Add(this.label12);
            this.tabContract.Controls.Add(this.listBox1);
            this.tabContract.Controls.Add(this.label6);
            this.tabContract.Controls.Add(this.label13);
            this.tabContract.Controls.Add(this.label5);
            this.tabContract.Controls.Add(this.label14);
            this.tabContract.Controls.Add(this.comboBox1);
            this.tabContract.Controls.Add(this.label15);
            this.tabContract.Controls.Add(this.label4);
            this.tabContract.Controls.Add(this.label16);
            this.tabContract.Controls.Add(this.label18);
            this.tabContract.Controls.Add(this.label17);
            this.tabContract.Location = new System.Drawing.Point(4, 22);
            this.tabContract.Name = "tabContract";
            this.tabContract.Padding = new System.Windows.Forms.Padding(3);
            this.tabContract.Size = new System.Drawing.Size(900, 494);
            this.tabContract.TabColor = System.Drawing.SystemColors.Control;
            this.tabContract.TabIndex = 0;
            this.tabContract.Text = "Договор";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(170, 314);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(142, 20);
            this.dateTimePicker1.TabIndex = 16;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            this.dateTimePicker1.DropDown += new System.EventHandler(this.dateTimePicker1_DropDown);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(170, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(563, 240);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(203, 20);
            this.textBox3.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(170, 236);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(203, 20);
            this.textBox2.TabIndex = 3;
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(170, 33);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(203, 20);
            this.textBox16.TabIndex = 3;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(170, 262);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(203, 20);
            this.textBox5.TabIndex = 3;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(170, 288);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(203, 20);
            this.textBox6.TabIndex = 3;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(563, 6);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(203, 20);
            this.textBox7.TabIndex = 3;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(563, 32);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(203, 20);
            this.textBox8.TabIndex = 3;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(563, 58);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(203, 20);
            this.textBox9.TabIndex = 3;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(563, 84);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(203, 20);
            this.textBox10.TabIndex = 3;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(563, 110);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(203, 20);
            this.textBox11.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(563, 265);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(203, 64);
            this.textBox4.TabIndex = 4;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(563, 136);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(203, 20);
            this.textBox12.TabIndex = 3;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(563, 214);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(203, 20);
            this.textBox15.TabIndex = 3;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(563, 162);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(203, 20);
            this.textBox13.TabIndex = 3;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(563, 188);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(203, 20);
            this.textBox14.TabIndex = 3;
            // 
            // labelDate2
            // 
            this.labelDate2.AutoSize = true;
            this.labelDate2.Location = new System.Drawing.Point(65, 340);
            this.labelDate2.Name = "labelDate2";
            this.labelDate2.Size = new System.Drawing.Size(99, 13);
            this.labelDate2.TabIndex = 17;
            this.labelDate2.Text = "Окончание Работ*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер*";
            // 
            // labelDate1
            // 
            this.labelDate1.AutoSize = true;
            this.labelDate1.Location = new System.Drawing.Point(83, 316);
            this.labelDate1.Name = "labelDate1";
            this.labelDate1.Size = new System.Drawing.Size(81, 13);
            this.labelDate1.TabIndex = 17;
            this.labelDate1.Text = "Начало Работ*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 62);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Генеральный Заказчик*";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(170, 340);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(142, 20);
            this.dateTimePicker2.TabIndex = 16;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dateTimePicker2.DropDown += new System.EventHandler(this.dateTimePicker2_DropDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(505, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ведущий";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(105, 239);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Вид Работ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 36);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(158, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Дополнительное Соглашение";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(130, 265);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Тема";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(48, 291);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Наименование Работ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(491, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Количество";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(524, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Цена";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(170, 161);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox2.Size = new System.Drawing.Size(203, 69);
            this.listBox2.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(463, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Цена За Единицу";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(170, 86);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(203, 69);
            this.listBox1.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Исполнители Договора";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(480, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Модель Цены";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Иностранные Заказчики";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(405, 113);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(152, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Объём Собственной Работы";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Location = new System.Drawing.Point(170, 59);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(203, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(493, 139);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Объём К/А";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(487, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Примечание";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(424, 165);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(133, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Плановая Трудоёмкость";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(502, 217);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Страница";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(405, 191);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(152, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Фактическая Трудоёмкость";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabContract);
            this.tabControlMain.Controls.Add(this.tabStages);
            this.tabControlMain.Controls.Add(this.tabArchive);
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(908, 520);
            this.tabControlMain.TabIndex = 18;
            // 
            // FormDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 561);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FormDocument";
            this.Text = "ДОГОВОР";
            this.tabArchive.ResumeLayout(false);
            this.tabStages.ResumeLayout(false);
            this.tabContract.ResumeLayout(false);
            this.tabContract.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private OwnTabPage tabArchive;
        private OwnTabPage tabStages;
        private System.Windows.Forms.TabControl tabControl2;
        private OwnTabPage tabContract;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label labelDate2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelDate1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonAddFile;
    }
}