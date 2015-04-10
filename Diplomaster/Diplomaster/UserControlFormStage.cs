using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;

namespace Diplomaster
{

    public partial class UserControlFormStage : UserControl
    {
        Hashtable DATA = new Hashtable();
        public int Id = -1;

        public bool validnum = false;

        FormDocument FormDocParent;
        private string oldnumber;
        public string number {get { return textBox1.Text; }}
        public DateTime date1 { get { return dateTimePicker1.Value; } }
        public DateTime date2 { get { return dateTimePicker2.Value; } }
        public string count { get { return textBox7.Text; } }
        public string price { get { return textBox8.Text; } }
        public string model { get { return textBox10.Text; } }
        public string zak { get { return textBox4.Text; } }
        public string avans { get { return textBox2.Text; } }
        public string raschet { get { return textBox3.Text; } }
        public string trud1 { get { return textBox13.Text; } }
        public string trud2 { get { return textBox14.Text; } }
        public string state { get { return textBox5.Text; } }
        public string act { get { return textBox6.Text; } }
        public string ud { get { return textBox9.Text; } }

        public UserControlFormStage(FormDocument parent, Hashtable data)
        {
            InitializeComponent();
            DATA = data;
            FormDocParent = parent;

            if (DATA != null) {
                Id = (int)DATA["Id"];
                validnum = true;
                Fill.TextBoxInt(textBox1, DATA["Номер"]);
                Fill.DateTimePicker(dateTimePicker1, DATA["Начало работ"]);
                Fill.DateTimePicker(dateTimePicker2, DATA["Окончание работ"]);
                Fill.TextBoxInt(textBox7, DATA["Количество"]);
                Fill.TextBoxDecimal(textBox8, DATA["Цена"]);
                Fill.TextBoxInt(textBox10, DATA["Модель цены"]);
                Fill.TextBox(textBox4, DATA["Заключение"]);
                Fill.TextBoxDecimal(textBox2, DATA["Аванс"]);
                Fill.TextBoxDecimal(textBox3, DATA["Расчёт"]);
                Fill.TextBoxFloat(textBox13, DATA["Плановая трудоёмкость"]);
                Fill.TextBoxFloat(textBox14, DATA["Фактическая трудоёмкость"]);
                Fill.TextBoxInt(textBox5, DATA["Текущее состояние"]);
                Fill.TextBoxInt(textBox6, DATA["Номер акта"]);
                Fill.TextBox(textBox9, DATA["Номер удостоверения"]);
            } else {
                Fill.DateTimePicker(dateTimePicker1, null);
                Fill.DateTimePicker(dateTimePicker2, null);
            }
            oldnumber = number;
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


        public bool ValidateStage()
        {
            bool Check = true;

            //NUM = Convert.ToInt32(textBox1.Text);
            //Check &= Validator.Apply(label1, textBox1, SQL.CheckUnique("Договор", "Номер", NUM));

            Check &= Validator.Apply(label1, textBox1, typeof(uint));

            Check &= Validator.Apply(labelDate1, dateTimePicker1);
            Check &= Validator.Apply(labelDate2, dateTimePicker2, dateTimePicker1);

            Check &= Validator.Apply(label10, textBox7, typeof(uint), true);

            Check &= Validator.Apply(label13, textBox10, typeof(int), true);
            Check &= Validator.Apply(label5, textBox5, typeof(int), true);
            Check &= Validator.Apply(label6, textBox6, typeof(int), true);

            Check &= Validator.Apply(label11, textBox8, typeof(decimal), true);
            Check &= Validator.Apply(label2, textBox2, typeof(decimal), true);
            Check &= Validator.Apply(label3, textBox3, typeof(decimal), true);

            Check &= Validator.Apply(label16, textBox13, typeof(float), true);
            Check &= Validator.Apply(label17, textBox14, typeof(float), true);

            return Check;
        }

        public static string GetValueString(int i)
        {
            string itos = i.ToString();
            /*
            ([Id], [Договор_id], [Номер], [Начало работ], [Окончание работ], [Количество], 
            [Цена], [Модель цены], [Заключение], [Аванс], [Расчёт], [Плановая трудоёмкость], 
            [Фактическая трудоёмкость], [Текущее состояние], [Номер акта], [Номер удостоверения])
            */

            return "@ID" + itos + ",@NUM,@NUMBER" + itos + ",@SDATE" + itos + ",@EDATE" + itos +",@COUNT" + itos +
                ",@PRICE" + itos + ",@MODEL" + itos + ",@ZAK" + itos + ",@AVANS" + itos + ",@RAS" + itos + 
                ",@PTRUD" + itos + ",@FTRUD" + itos + ",@STATE" + itos + ",@ACT" + itos + ",@UD" + itos;
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            if (oldnumber != number)
            {
                oldnumber = number;
                if (validnum = Validator.Apply(label1, textBox1, typeof(uint)))
                {
                    ((TabPage)Parent).Text = Global.StageTextPrefix + textBox1.Text;
                    FormDocParent.ReorderStageTab((TabPage)Parent, Convert.ToInt32(textBox1.Text));
                }
                else
                    ((TabPage)Parent).Text = Global.EmptyStageText;
            }
        }


    }
}
