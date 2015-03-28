namespace Diplomaster
{
    partial class UserControlFileEdit
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
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.textBoxImg1N = new System.Windows.Forms.TextBox();
            this.labelImg = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonShow = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(341, 11);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 21;
            this.buttonExport.Text = "Экспорт";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(422, 11);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 20;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // textBoxImg1N
            // 
            this.textBoxImg1N.Location = new System.Drawing.Point(69, 13);
            this.textBoxImg1N.Name = "textBoxImg1N";
            this.textBoxImg1N.Size = new System.Drawing.Size(139, 20);
            this.textBoxImg1N.TabIndex = 18;
            // 
            // labelImg
            // 
            this.labelImg.AutoSize = true;
            this.labelImg.Location = new System.Drawing.Point(6, 16);
            this.labelImg.Name = "labelImg";
            this.labelImg.Size = new System.Drawing.Size(57, 13);
            this.labelImg.TabIndex = 17;
            this.labelImg.Text = "Название";
            this.labelImg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.groupBox_MouseClick);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.buttonShow);
            this.groupBox.Controls.Add(this.buttonExport);
            this.groupBox.Controls.Add(this.buttonDelete);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.labelImg);
            this.groupBox.Controls.Add(this.textBoxImg1N);
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(510, 45);
            this.groupBox.TabIndex = 22;
            this.groupBox.TabStop = false;
            this.groupBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.groupBox_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Расширение";
            this.label1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.groupBox_MouseClick);
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(260, 11);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(75, 23);
            this.buttonShow.TabIndex = 21;
            this.buttonShow.Text = "Просмотр";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // UserControlFileEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "UserControlFileEdit";
            this.Size = new System.Drawing.Size(510, 45);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.TextBox textBoxImg1N;
        private System.Windows.Forms.Label labelImg;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonShow;
    }
}
