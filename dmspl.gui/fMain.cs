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
using dmspl.common.log;

namespace dmspl
{
    public partial class fMain : Form
    {
        IDataStorage datastorage;
        UDPComm comm;

        public string bTime { set { button1.Text = value; } }

        #region Constructor
        public fMain()
        {
            InitializeComponent();
            InitDms();

            dmspl.gui.Properties.Settings.Default.Save();

            this.FormClosed += FormGui_FormClosed;

            timer1.Start();
        }

        private void InitDms()
        {
            StartLog();

            comm = InitCommunication();

            if (comm != null)
            {
                datastorage = InitDatabaseStorage(comm);
                comm.DataStorage = datastorage;
            }
        }

        void StartLog()
        {
            IDataLog _log = new HTMLLog("costam");
            _log.Start();
            _log.SetLevel(Module.Appl, Level.Debug);
            _log.SetLevel(Module.DataBase, Level.Debug);
            _log.SetLevel(Module.RXTXComm, Level.Debug);
            _log.EnabledModules = Module.All;
            _log.EnabledEventType = EvType.All;
            DataLog.SetDefautLog(_log);
        }

        private IDataStorage InitDatabaseStorage(UDPComm comObj)
        {
            IDataStorage datastorage = null;
            try
            {
                datastorage = DataStorageFactory.CreateStorage(DataStorageType.SQL);
                datastorage.UdpComm = comObj;
                return datastorage;
            }
            catch (Exception ex)
            {
                DataLog.Log(Module.Appl, EvType.Error, Level.Primary, ex.Message);

            }
            return datastorage;
        }

        private UDPComm InitCommunication()
        {
            UDPComm ret = null;
            try
            {
                ret = new UDPComm(2002);
                ret.DataStorage = datastorage;
            }
            catch (Exception ex)
            {
                DataLog.Log(Module.Appl, EvType.Error, Level.Primary, ex.Message);
                if (ret != null)
                    ret.Dispose();
                ret = null;
            }
            return ret;
        }

        #endregion

        void FormGui_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (datastorage != null) ((IDisposable)datastorage).Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
