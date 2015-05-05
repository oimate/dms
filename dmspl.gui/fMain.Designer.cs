namespace dmspl
{
    partial class fMain
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
            this.btLogin = new System.Windows.Forms.Button();
            this.btLoadProdPlan = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lStatusDB = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btLogin
            // 
            this.btLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.btLogin.Location = new System.Drawing.Point(0, 23);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(792, 23);
            this.btLogin.TabIndex = 4;
            this.btLogin.Text = "login";
            this.btLogin.UseVisualStyleBackColor = true;
            // 
            // btLoadProdPlan
            // 
            this.btLoadProdPlan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btLoadProdPlan.Location = new System.Drawing.Point(0, 0);
            this.btLoadProdPlan.Name = "btLoadProdPlan";
            this.btLoadProdPlan.Size = new System.Drawing.Size(792, 23);
            this.btLoadProdPlan.TabIndex = 6;
            this.btLoadProdPlan.Text = "load production plan";
            this.btLoadProdPlan.UseVisualStyleBackColor = true;
            this.btLoadProdPlan.Click += new System.EventHandler(this.btLoadProdPlan_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lStatusDB});
            this.statusStrip1.Location = new System.Drawing.Point(0, 548);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 7;
            // 
            // lStatusDB
            // 
            this.lStatusDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lStatusDB.Name = "lStatusDB";
            this.lStatusDB.Size = new System.Drawing.Size(24, 17);
            this.lStatusDB.Text = "Init";
            this.lStatusDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btLogin);
            this.Controls.Add(this.btLoadProdPlan);
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btLogin;
        private System.Windows.Forms.Button btLoadProdPlan;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lStatusDB;
    }
}

