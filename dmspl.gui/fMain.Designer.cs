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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btLoadProdPlan = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_MfpTab = new System.Windows.Forms.DataGridView();
            this.bUpdateMFP = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv_ErpTab = new System.Windows.Forms.DataGridView();
            this.bUpdateERP = new System.Windows.Forms.Button();
            this.btLogin = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MfpTab)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ErpTab)).BeginInit();
            this.SuspendLayout();
            // 
            // btLoadProdPlan
            // 
            this.btLoadProdPlan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btLoadProdPlan.Location = new System.Drawing.Point(0, 23);
            this.btLoadProdPlan.Name = "btLoadProdPlan";
            this.btLoadProdPlan.Size = new System.Drawing.Size(792, 23);
            this.btLoadProdPlan.TabIndex = 4;
            this.btLoadProdPlan.Text = "login";
            this.btLoadProdPlan.UseVisualStyleBackColor = true;
            this.btLoadProdPlan.Click += new System.EventHandler(this.btLoadProdPlan_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(792, 524);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_MfpTab);
            this.tabPage1.Controls.Add(this.bUpdateMFP);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(784, 498);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MFP";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_MfpTab
            // 
            this.dgv_MfpTab.AllowUserToAddRows = false;
            this.dgv_MfpTab.AllowUserToDeleteRows = false;
            this.dgv_MfpTab.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_MfpTab.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_MfpTab.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_MfpTab.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgv_MfpTab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_MfpTab.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_MfpTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_MfpTab.Location = new System.Drawing.Point(3, 26);
            this.dgv_MfpTab.MultiSelect = false;
            this.dgv_MfpTab.Name = "dgv_MfpTab";
            this.dgv_MfpTab.ReadOnly = true;
            this.dgv_MfpTab.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_MfpTab.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_MfpTab.Size = new System.Drawing.Size(778, 469);
            this.dgv_MfpTab.TabIndex = 1;
            // 
            // bUpdateMFP
            // 
            this.bUpdateMFP.Dock = System.Windows.Forms.DockStyle.Top;
            this.bUpdateMFP.Location = new System.Drawing.Point(3, 3);
            this.bUpdateMFP.Name = "bUpdateMFP";
            this.bUpdateMFP.Size = new System.Drawing.Size(778, 23);
            this.bUpdateMFP.TabIndex = 2;
            this.bUpdateMFP.Text = "update mfp";
            this.bUpdateMFP.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_ErpTab);
            this.tabPage2.Controls.Add(this.bUpdateERP);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(784, 521);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ERP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv_ErpTab
            // 
            this.dgv_ErpTab.AllowUserToAddRows = false;
            this.dgv_ErpTab.AllowUserToDeleteRows = false;
            this.dgv_ErpTab.AllowUserToOrderColumns = true;
            this.dgv_ErpTab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ErpTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ErpTab.Location = new System.Drawing.Point(3, 26);
            this.dgv_ErpTab.Name = "dgv_ErpTab";
            this.dgv_ErpTab.ReadOnly = true;
            this.dgv_ErpTab.Size = new System.Drawing.Size(778, 492);
            this.dgv_ErpTab.TabIndex = 1;
            // 
            // bUpdateERP
            // 
            this.bUpdateERP.Dock = System.Windows.Forms.DockStyle.Top;
            this.bUpdateERP.Location = new System.Drawing.Point(3, 3);
            this.bUpdateERP.Name = "bUpdateERP";
            this.bUpdateERP.Size = new System.Drawing.Size(778, 23);
            this.bUpdateERP.TabIndex = 3;
            this.bUpdateERP.Text = "update erp";
            this.bUpdateERP.UseVisualStyleBackColor = true;
            // 
            // btLogin
            // 
            this.btLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.btLogin.Location = new System.Drawing.Point(0, 0);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(792, 23);
            this.btLogin.TabIndex = 6;
            this.btLogin.Text = "load production plan";
            this.btLogin.UseVisualStyleBackColor = true;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btLoadProdPlan);
            this.Controls.Add(this.btLogin);
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MfpTab)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ErpTab)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btLoadProdPlan;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_MfpTab;
        private System.Windows.Forms.DataGridView dgv_ErpTab;
        private System.Windows.Forms.Button bUpdateMFP;
        private System.Windows.Forms.Button bUpdateERP;
        private System.Windows.Forms.Button btLogin;
    }
}

