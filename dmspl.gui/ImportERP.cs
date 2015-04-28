using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dmspl.common;
using dmspl.datahandler;

namespace dmspl
{
    public partial class ImportERP : Form
    {
        IDataStorage datastorage;

        public ImportERP(string filepath, ImporterType it, IDataStorage ds)
        {
            InitializeComponent();

            pgb.Value = 0;
            pgb.Maximum = 1000;

            datastorage = ds;
            ds.DataStorageImportUpdate = DataStorageImportUpdate;

            ProductionPlan.UpdateProductionPlan(filepath, it, datastorage);
        }

        private void DataStorageImportUpdate(IDataStorage datastorage, DelegateCollection.Classes.ImportUpdateData data)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageImportUpdate(DataStorageImportUpdate), datastorage, data);
            else
            {
                lOK.Text = data.OK.ToString();
                lNOK.Text = data.NOK.ToString();
                lIST.Text = data.IST.ToString();
                lALL.Text = data.ALL.ToString();
                pgb.Maximum = data.ALL;
                pgb.Value = data.IST;
            }
        }

        private void ImportERP_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (datastorage != null && datastorage.DataStorageImportUpdate != null)
                datastorage.DataStorageImportUpdate = null;
        }
    }
}
