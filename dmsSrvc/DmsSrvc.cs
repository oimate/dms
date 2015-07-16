using dmspl.common;
using dmspl.common.log;
using dmspl.datastorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using UDP_RXTX;

namespace dmsSrvc
{
    public partial class DmsSrvc : ServiceBase
    {
        public DmsSrvc(string[] args)
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            DmsSrvcStatus serviceStatus = new DmsSrvcStatus();
            //serviceStatus.dwCurrentState = DmsSrvcState.SERVICE_START_PENDING;
            //serviceStatus.dwWaitHint = 100000;
            //SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Update the service state to Running.
            serviceStatus.dwCurrentState = DmsSrvcState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            InitDms();
        }

        protected override void OnStop()
        {
            // Update the service state to Stop Pending.
            DmsSrvcStatus serviceStatus = new DmsSrvcStatus();
            //serviceStatus.dwCurrentState = DmsSrvcState.SERVICE_STOP_PENDING;
            //serviceStatus.dwWaitHint = 100000;
            //SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Update the service state to Stopped.
            serviceStatus.dwCurrentState = DmsSrvcState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnContinue()
        {
            EventLog.WriteEntry("OnContinue");
            base.OnContinue();
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref DmsSrvcStatus serviceStatus);

        UDPComm comm;
        IDataStorage datastorage;
        private void InitDms()
        {
            StartLog();
            //datatosend = new List<int>(new int[400]);

            comm = InitCommunication();

            if (comm != null)
            {
                datastorage = InitDatabaseStorage(comm);
                comm.DataStorage = datastorage;
            }
        }

        void StartLog()
        {
            IDataLog _log = new HTMLLog("D:\\random.name");
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
            //if (comObj == null)
            //    throw new ArgumentNullException("comObj");
            IDataStorage ret;
            try
            {
                ret = DataStorageFactory.CreateStorage(DataStorageType.SQL);
                //ret.StartThread();
                ret.UdpComm = comObj;
                //ret.DataStorageImportResult = DataStorageImportResult;
                //ret.DataStorageStateReport = DataStorageStateReport;
                //ret.DataStorageStateReport = DataStorageModeReport;
                //ret.DataStorageMfpUpdateEvent = DataStorageMfpUpdateEvent;
                //ret.DataStorageErpUpdateEvent = DataStorageErpUpdateEvent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                ret = null;
            }
            return ret;
        }

        private UDPComm InitCommunication()
        {
            UDPComm ret = null;
            try
            {
                ret = new UDPComm(2002);
                //ret.ReferencjaDoFunkcjiWyswietlajacejText = refdofwystekst;
                ret.DataStorage = datastorage;
                //ret.StatusChanged += StatChanged;
                //ret.DataRecv += DataRecv;
            }
            catch (Exception ex)
            {
                if (ret != null)
                    ret.Dispose();
                ret = null;
            }
            return ret;
        }
    }
}
