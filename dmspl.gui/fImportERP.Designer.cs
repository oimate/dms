namespace dmspl
{
    partial class fImportERP
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
            this.pgb = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lOK = new System.Windows.Forms.Label();
            this.lNOK = new System.Windows.Forms.Label();
            this.lIST = new System.Windows.Forms.Label();
            this.lALL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pgb
            // 
            this.pgb.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgb.Location = new System.Drawing.Point(0, 0);
            this.pgb.Name = "pgb";
            this.pgb.Size = new System.Drawing.Size(292, 23);
            this.pgb.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(34, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "OK: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(42, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "NOK: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(50, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Current:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 104);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3);
            this.label4.Size = new System.Drawing.Size(40, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Total:";
            // 
            // lOK
            // 
            this.lOK.AutoSize = true;
            this.lOK.Location = new System.Drawing.Point(143, 29);
            this.lOK.Margin = new System.Windows.Forms.Padding(3);
            this.lOK.Name = "lOK";
            this.lOK.Padding = new System.Windows.Forms.Padding(3);
            this.lOK.Size = new System.Drawing.Size(100, 19);
            this.lOK.TabIndex = 5;
            this.lOK.Text = ".............................";
            // 
            // lNOK
            // 
            this.lNOK.AutoSize = true;
            this.lNOK.Location = new System.Drawing.Point(143, 54);
            this.lNOK.Margin = new System.Windows.Forms.Padding(3);
            this.lNOK.Name = "lNOK";
            this.lNOK.Padding = new System.Windows.Forms.Padding(3);
            this.lNOK.Size = new System.Drawing.Size(100, 19);
            this.lNOK.TabIndex = 2;
            this.lNOK.Text = ".............................";
            // 
            // lIST
            // 
            this.lIST.AutoSize = true;
            this.lIST.Location = new System.Drawing.Point(143, 79);
            this.lIST.Margin = new System.Windows.Forms.Padding(3);
            this.lIST.Name = "lIST";
            this.lIST.Padding = new System.Windows.Forms.Padding(3);
            this.lIST.Size = new System.Drawing.Size(100, 19);
            this.lIST.TabIndex = 3;
            this.lIST.Text = ".............................";
            // 
            // lALL
            // 
            this.lALL.AutoSize = true;
            this.lALL.Location = new System.Drawing.Point(143, 104);
            this.lALL.Margin = new System.Windows.Forms.Padding(3);
            this.lALL.Name = "lALL";
            this.lALL.Padding = new System.Windows.Forms.Padding(3);
            this.lALL.Size = new System.Drawing.Size(100, 19);
            this.lALL.TabIndex = 4;
            this.lALL.Text = ".............................";
            // 
            // fImportERP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 270);
            this.Controls.Add(this.lOK);
            this.Controls.Add(this.lALL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lIST);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lNOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pgb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "fImportERP";
            this.Text = "ImportERP";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImportERP_FormClosed);
            this.Shown += new System.EventHandler(this.fImportERP_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lOK;
        private System.Windows.Forms.Label lNOK;
        private System.Windows.Forms.Label lIST;
        private System.Windows.Forms.Label lALL;
    }
}