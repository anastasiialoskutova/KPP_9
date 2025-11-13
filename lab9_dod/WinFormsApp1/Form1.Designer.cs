using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    partial class Form1
    {
        private IContainer components = null;
        private Button button1;
        private Label label1;
        private Label label2;

        /// <summary>
        ///  Очистка ресурсів
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, згенерований конструктором форм

        private void InitializeComponent()
        {
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(346, 12);
            button1.Name = "button1";
            button1.Size = new Size(200, 40);
            button1.TabIndex = 0;
            button1.Text = "Запустити приклад";
            button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(12, 64);
            label1.Name = "label1";
            label1.Size = new Size(420, 420);
            label1.TabIndex = 1;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(438, 64);
            label2.Name = "label2";
            label2.Size = new Size(420, 420);
            label2.TabIndex = 2;
            // 
            // Form1
            // 
            ClientSize = new Size(870, 500);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "LAA Demo: Індексатор (клумба квітів)";
            ResumeLayout(false);
        }

        #endregion
    }
}
