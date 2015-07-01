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

namespace dmspl.Forms
{
    public partial class fImportERP : Form
    {
        IDataStorage datastorage;
        string filepath;
        ImporterType it;

        public fImportERP(string filepath, ImporterType it, IDataStorage ds)
        {
            InitializeComponent();

            pgb.Value = 0;
            pgb.Maximum = 1000;

            datastorage = ds;
            ds.DataStorageImportUpdate = DataStorageImportUpdate;
            ds.DataStorageImportResult = DataStorageImportResult;

            this.filepath = filepath;
            this.it = it;
        }

        private void DataStorageImportResult(IDataStorage whosend, string msg, int items, int duplicates)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageImportResult(DataStorageImportResult), whosend, msg, items, duplicates);
            else
            {
                this.Close();
            }
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
            if (datastorage != null && datastorage.DataStorageImportResult != null)
                datastorage.DataStorageImportResult = null;
        }

        async public Task StartUpdating()
        {
            await ProductionPlan.UpdateProductionPlan(filepath, it, datastorage);
        }

        private async void fImportERP_Shown(object sender, EventArgs e)
        {
            await StartUpdating();
        }
    }
}
