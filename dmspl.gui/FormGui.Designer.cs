namespace dmspl
{
    partial class FormGui
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
            this.btConnect = new System.Windows.Forms.Button();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.lbBoard = new System.Windows.Forms.ListBox();
            this.btLoadProdPlan = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btConnect
            // 
            this.btConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.btConnect.Location = new System.Drawing.Point(0, 0);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(292, 23);
            this.btConnect.TabIndex = 1;
            this.btConnect.Text = "connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.btDisconnect.Location = new System.Drawing.Point(0, 23);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(292, 23);
            this.btDisconnect.TabIndex = 2;
            this.btDisconnect.Text = "disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // lbBoard
            // 
            this.lbBoard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbBoard.FormattingEnabled = true;
            this.lbBoard.Location = new System.Drawing.Point(0, 92);
            this.lbBoard.Name = "lbBoard";
            this.lbBoard.ScrollAlwaysVisible = true;
            this.lbBoard.Size = new System.Drawing.Size(292, 181);
            this.lbBoard.TabIndex = 3;
            // 
            // btLoadProdPlan
            // 
            this.btLoadProdPlan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btLoadProdPlan.Location = new System.Drawing.Point(0, 46);
            this.btLoadProdPlan.Name = "btLoadProdPlan";
            this.btLoadProdPlan.Size = new System.Drawing.Size(292, 23);
            this.btLoadProdPlan.TabIndex = 4;
            this.btLoadProdPlan.Text = "load production plan";
            this.btLoadProdPlan.UseVisualStyleBackColor = true;
            this.btLoadProdPlan.Click += new System.EventHandler(this.btLoadProdPlan_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbProgress.Location = new System.Drawing.Point(0, 69);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(292, 23);
            this.pbProgress.TabIndex = 5;
            this.pbProgress.Visible = false;
            // 
            // FormGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.lbBoard);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.btLoadProdPlan);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.btConnect);
            this.Name = "FormGui";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.ListBox lbBoard;
        private System.Windows.Forms.Button btLoadProdPlan;
        private System.Windows.Forms.ProgressBar pbProgress;
    }
}

