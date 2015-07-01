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
    public partial class fMfpTab : Form
    {
        DataTable mfpdt;
        DataTable MfpDT { get { return mfpdt; } set { SetMfpDataTable(value); } }

        private void SetMfpDataTable(DataTable value)
        {
            mfpdt = value;
            dgv_MfpTab.DataSource = mfpdt;
            dgv_MfpTab.Update();
        }

        public fMfpTab(DataTable mfpdt)
        {
            InitializeComponent();
            MfpDT = mfpdt;
        }

        delegate void UpdateMfpDTDelegate();
        public void UpdateMfpDT()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new UpdateMfpDTDelegate(UpdateMfpDT));
            }
            else
            {
                dgv_MfpTab.Update();
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            UpdateMfpDT();
        }
    }
}
