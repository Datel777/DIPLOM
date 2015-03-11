namespace Diplomaster
{
    partial class UserControlSearch
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Начинающиеся");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Действующие");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Закрывающиеся");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("2015", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("2016");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Начинающиеся";
            treeNode1.Text = "Начинающиеся";
            treeNode2.Name = "Действующие";
            treeNode2.Text = "Действующие";
            treeNode3.Name = "Закрывающиеся";
            treeNode3.Text = "Закрывающиеся";
            treeNode4.Name = "2015";
            treeNode4.Text = "2015";
            treeNode5.Name = "2016";
            treeNode5.Text = "2016";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            this.treeView1.Size = new System.Drawing.Size(180, 343);
            this.treeView1.TabIndex = 6;
            // 
            // UserControlSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.treeView1);
            this.Name = "UserControlSearch";
            this.Size = new System.Drawing.Size(451, 378);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;

    }
}
