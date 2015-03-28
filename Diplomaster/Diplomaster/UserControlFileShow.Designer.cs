namespace Diplomaster
{
    partial class UserControlFileShow
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonImg1_2 = new System.Windows.Forms.Button();
            this.buttonImg1_3 = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonImg1_2
            // 
            this.buttonImg1_2.Enabled = false;
            this.buttonImg1_2.Location = new System.Drawing.Point(262, 11);
            this.buttonImg1_2.Name = "buttonImg1_2";
            this.buttonImg1_2.Size = new System.Drawing.Size(75, 23);
            this.buttonImg1_2.TabIndex = 21;
            this.buttonImg1_2.Text = "Экспорт";
            this.buttonImg1_2.UseVisualStyleBackColor = true;
            // 
            // buttonImg1_3
            // 
            this.buttonImg1_3.Enabled = false;
            this.buttonImg1_3.Location = new System.Drawing.Point(343, 11);
            this.buttonImg1_3.Name = "buttonImg1_3";
            this.buttonImg1_3.Size = new System.Drawing.Size(75, 23);
            this.buttonImg1_3.TabIndex = 20;
            this.buttonImg1_3.Text = "Просмотр";
            this.buttonImg1_3.UseVisualStyleBackColor = true;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.buttonImg1_2);
            this.groupBox.Controls.Add(this.buttonImg1_3);
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(427, 40);
            this.groupBox.TabIndex = 23;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Название";
            // 
            // UserControlFileShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "UserControlFileShow";
            this.Size = new System.Drawing.Size(432, 44);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonImg1_2;
        private System.Windows.Forms.Button buttonImg1_3;
        private System.Windows.Forms.GroupBox groupBox;
    }
}
