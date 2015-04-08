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
        int Id = -1;

        public UserControlFormStage(Hashtable data)
        {
            InitializeComponent();
            DATA = data;

            if (DATA != null) {
                Id = (int)DATA["Id"];
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
    }
}
