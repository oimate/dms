using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dmspl.Forms
{
    public partial class fErpTab : Form
    {
        DataTable erpdt;
        DataTable ErpDT { get { return erpdt; } set { SetErpDataTable(value); } }

        private void SetErpDataTable(DataTable value)
        {
            erpdt = value;
            dgv_ErpTab.DataSource = erpdt;
            dgv_ErpTab.Update();
        }

        public fErpTab(DataTable erpdt)
        {
            InitializeComponent();
            dgv_ErpTab.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            ErpDT = erpdt;
        }

        delegate void UpdateErpDTDelegate();

        public void UpdateErpDT()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new UpdateErpDTDelegate(UpdateErpDT));
            }
            else
            {
                //dgv_ErpTab.DataSource = erpdt;
                //dgv_ErpTab.Update();
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            UpdateErpDT();
        }
    }
}
