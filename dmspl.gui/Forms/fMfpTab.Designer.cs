namespace dmspl.Forms
{
    partial class fMfpTab
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
            this.dgv_MfpTab = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MfpTab)).BeginInit();
            this.SuspendLayout();
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
            this.dgv_MfpTab.Location = new System.Drawing.Point(0, 23);
            this.dgv_MfpTab.MultiSelect = false;
            this.dgv_MfpTab.Name = "dgv_MfpTab";
            this.dgv_MfpTab.ReadOnly = true;
            this.dgv_MfpTab.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_MfpTab.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_MfpTab.Size = new System.Drawing.Size(292, 247);
            this.dgv_MfpTab.TabIndex = 0;
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
            this.Controls.Add(this.dgv_MfpTab);
            this.Controls.Add(this.bUpdate);
            this.Name = "fMfpTab";
            this.Text = "fMFPTable";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MfpTab)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_MfpTab;
        private System.Windows.Forms.Button bUpdate;
    }
}