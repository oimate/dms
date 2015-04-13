namespace dmspl
{
    partial class fErpTab
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
            this.dgv_ErpTab = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ErpTab)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ErpTab
            // 
            this.dgv_ErpTab.AllowUserToAddRows = false;
            this.dgv_ErpTab.AllowUserToDeleteRows = false;
            this.dgv_ErpTab.AllowUserToOrderColumns = true;
            this.dgv_ErpTab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ErpTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ErpTab.Location = new System.Drawing.Point(0, 23);
            this.dgv_ErpTab.Name = "dgv_ErpTab";
            this.dgv_ErpTab.ReadOnly = true;
            this.dgv_ErpTab.Size = new System.Drawing.Size(292, 247);
            this.dgv_ErpTab.TabIndex = 0;
            // 
            // bUpdate
            // 
            this.bUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.bUpdate.Location = new System.Drawing.Point(0, 0);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(292, 23);
            this.bUpdate.TabIndex = 1;
            this.bUpdate.Text = "update";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // fMfpTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 270);
            this.Controls.Add(this.dgv_ErpTab);
            this.Controls.Add(this.bUpdate);
            this.Name = "fMfpTab";
            this.Text = "fMFPTable";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ErpTab)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ErpTab;
        private System.Windows.Forms.Button bUpdate;
    }
}