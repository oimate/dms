using dmspl.common;
using dmspl.datahandler;
using dmspl.datastorage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDP_RXTX;
using dmspl.common.log;
using System.IO;

namespace dmspl
{
    public partial class fMain : Form
    {
        IDataStorage datastorage;
        UDPComm comm;
        TCP.TCP_MasterSlave serverinfo;

        public string bTime { set { button1.Text = value; } }

        #region Constructor
        public fMain()
        {
            InitializeComponent();

            InitDms();

            dmspl.gui.Properties.Settings.Default.Save();

            this.FormClosed += fMain_FormClosed;
            startupapp = DateTime.Now;
            timer1.Start();
        }

        private void InitTcpIp()
        {
            try
            {
                serverinfo = new TCP.TCP_MasterSlave(TCP.ServerType.Server, "0.0.0.0", 4242, "dmsTcpSrv");
                serverinfo.ReceiveData = TCP.TCP_RequestHandler.ReceiveData;
                TCP.TCP_RequestHandler.ServerInfo = serverinfo;
                serverinfo.Open();
            }
            catch (Exception ex)
            {
                DataLog.Log(Module.TcpIp, EvType.Error, Level.Debug, ex.Message);
            }
        }

        private void InitDms()
        {
            StartLog();

            InitTcpIp();

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

        void fMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (datastorage != null) ((IDisposable)datastorage).Dispose();
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serverinfo != null) serverinfo.Close();
            serverinfo = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var a = (DateTime.Now - startupapp);
            bTime = string.Format("Uptime: {0} days, {1:D2}h {2:D2}min {3:D2}sec", a.Days, a.Hours, a.Minutes, a.Seconds);
        }


        public DateTime startupapp { get; set; }
    }
}
