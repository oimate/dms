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
using UDP_RXTX;

namespace dmspl // to z dzis :)
{
    public partial class fMain : Form
    {
        fImportERP ImportPopUp;
        fMfpTab MfpDTPopUp;
        fErpTab ErpDTPopUp;

        IDataStorage datastorage;
        DataSimulator ds;
        List<int> datatosend;
        int currentdata;

        UDPComm comm;

        #region Constructor
        public fMain()
        {
            InitializeComponent();

            datatosend = new List<int>(new int[400]);
            datastorage = DataStorageFactory.CreateStorage(DataStorageType.SQL);
            datastorage.StartThread();

            comm = new UDPComm(9001);
            comm.ReferencjaDoFunkcjiWyswietlajacejText = refdofwystekst;
            comm.DataStorage = datastorage;

            datastorage.UdpComm = comm;

            this.FormClosed += FormGui_FormClosed;

            //datastorage.DataStorageImportUpdate = DataStorageImportUpdate;
            datastorage.DataStorageImportResult = DataStorageImportResult;
            datastorage.DataStorageStateReport = DataStorageStateReport;
            datastorage.DataStorageModeReport = DataStorageModeReport;
            datastorage.DataStorageMfpUpdateEvent = DataStorageMfpUpdateEvent;
            datastorage.DataStorageErpUpdateEvent = DataStorageErpUpdateEvent;

            //ds = new DataSimulator(datastorage); //moved to DataStorageModeReport;
        }

        #endregion

        private void refdofwystekst(string d)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.SetTextDel(refdofwystekst), d);
            else
            { }
        }

        void FormGui_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ds != null) ds.Dispose();
            if (datastorage != null) ((IDisposable)datastorage).Dispose();
        }

        private void btLoadProdPlan_Click(object sender, EventArgs e)
        {
            if (ImportPopUp != null && !ImportPopUp.IsDisposed) return;
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.CheckFileExists = true;
            openfile.CheckPathExists = true;
            openfile.InitialDirectory = Application.StartupPath;
            openfile.Filter = "Text files (*.txt)|*.txt|Excel files (*.xlsx)|*.xlsx";
            if (openfile.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            string filepath = openfile.FileName;
            ImportPopUp = new fImportERP(filepath, ImporterType.txt, datastorage);
            if (ErpDTPopUp != null && !ErpDTPopUp.IsDisposed) ErpDTPopUp.Hide();
            ImportPopUp.StartPosition = FormStartPosition.Manual;
            ImportPopUp.Top = this.Top;
            ImportPopUp.Left = this.Left + this.Width;
            ImportPopUp.Show(this);
        }

        private void DataStorageImportUpdate(IDataStorage datastorage, DelegateCollection.Classes.ImportUpdateData data)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageImportUpdate(DataStorageImportUpdate), datastorage, data);
            else
            { }
        }

        private void DataStorageImportResult(IDataStorage datastorage, string msg, int items, int duplicates)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageImportResult(DataStorageImportResult), datastorage, msg, items, duplicates);
            else
            { }
        }

        private void DataStorageStateReport(IDataStorage datastorage, ConnectionState oldstate, ConnectionState newstate)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageStateReport(DataStorageStateReport), datastorage, oldstate, newstate);
            else
            { }
        }

        private void DataStorageMfpUpdateEvent(DataTable mfpdt)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageMfpUpdateEvent(DataStorageMfpUpdateEvent), mfpdt);
            else
            { }
        }

        private void DataStorageErpUpdateEvent(DataTable erpdt)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageErpUpdateEvent(DataStorageErpUpdateEvent), erpdt);
            else
            { }
        }

        private void DataStorageModeReport(IDataStorage whosend, DataStorageMode oldstate, DataStorageMode newstate)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.DataStorageModeReport(DataStorageModeReport), datastorage, oldstate, newstate);
            else
            { }
        }
    }
}
