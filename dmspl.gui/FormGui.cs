using dmspl.common;
using dmspl.datahandler;
using dmspl.dataimporter;
using dmspl.dataprovider;
using dmspl.datastorage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dmspl
{
    public partial class FormGui : Form
    {
        IDataStorage datastorage;
        DataSimulator ds;
        List<int> datatosend;
        int currentdata;

        public FormGui()
        {
            InitializeComponent();

            datatosend = new List<int>(400);
            for (int i = 0; i < 400; i++)
            {
                datatosend.Add(0);
            }

            datastorage = DataStorageFactory.CreateStorage(DataStorageType.SQL);

            this.FormClosed += FormGui_FormClosed;

            //datastorage.DataStorageImportUpdate = DataStorageImportUpdate;
            datastorage.DataStorageImportResult = DataStorageImportResult;
            datastorage.DataStorageStateReport = DataStorageStateReport;
            datastorage.DataStorageModeReport = DataStorageModeReport;

            ds = new DataSimulator(datastorage);
        }

        void FormGui_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ds != null) ds.Dispose();
            if (datastorage != null) datastorage.Dispose();
        }

        private async void btConnect_Click(object sender, EventArgs e)
        {
            int r = new Random().Next(0, 9999);
            datatosend[currentdata] = r;
            await datastorage.UpdateMFPAsync(datatosend);
            System.Diagnostics.Debug.WriteLine("datatosend[{0:D3}] = {1:D4}", currentdata, r);
            currentdata = (++currentdata) % 400;
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(session.Disconnect);
            //t.Start();

            datastorage.PauseResumeThread();
            ds.PauseResumeThread();
        }

        private void btLoadProdPlan_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.CheckFileExists = true;
            openfile.CheckPathExists = true;
            openfile.InitialDirectory = Application.StartupPath;
            openfile.Filter = "Text files (*.txt)|*.txt|Excel files (*.xlsx)|*.xlsx";
            if (openfile.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            pbProgress.Visible = true;

            string filepath = openfile.FileName;

            //if (tmu == null) tmu = new TestManyUpdates();

            ImportERP importpopup = new ImportERP(filepath, ImporterType.txt, datastorage);
            importpopup.StartPosition = FormStartPosition.CenterParent;
            importpopup.ShowDialog(this);

            //ProductionPlan.UpdateProductionPlan(filepath, ImporterType.txt, datastorage);
        }

        private void DataStorageImportUpdate(IDataStorage datastorage, DelegateCollection.Classes.ImportUpdateData data)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageImportUpdate(DataStorageImportUpdate), datastorage, data);
            else
            {
                if (lbBoard.Items.Count > 10000) lbBoard.Items.Clear();
                lbBoard.Items.Insert(0, data.Msg);
            }
        }

        private void DataStorageImportResult(IDataStorage datastorage, string msg, int items, int duplicates)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageImportResult(DataStorageImportResult), datastorage, msg, items, duplicates);
            else
            {
                if (lbBoard.Items.Count > 10000) lbBoard.Items.Clear();
                lbBoard.Items.Insert(0, msg);
            }
        }

        private void DataStorageStateReport(IDataStorage datastorage, ConnectionState oldstate, ConnectionState newstate)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageStateReport(DataStorageStateReport), datastorage, oldstate, newstate);
            else
            {
                lbBoard.Items.Add(string.Format("state changed from {0} to {1}", oldstate, newstate));
                //lbStatus.Text = string.Format("connection state = {0}", newstate);
            }
        }

        private void DataStorageModeReport(IDataStorage whosend, DataStorageSourceMode oldstate, DataStorageSourceMode newstate)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageModeReport(DataStorageModeReport), datastorage, oldstate, newstate);
            else
            {
                lbBoard.Items.Add(string.Format("state changed from {0} to {1}", oldstate, newstate));
            }
        }
    }
}
