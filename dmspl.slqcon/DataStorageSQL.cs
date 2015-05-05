using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dmspl.common;
using dmspl.common.datamodels;
using System.Threading;
using System.Data;
using System.IO;
using System.Data.Objects;

namespace dmspl.datastorage
{
    class DataStorageSQL : IDataStorage, IDisposable
    {
        public IUDPComm UdpComm { get; set; }

        DataStorageState state;
        public DataStorageState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                OnDataStorageStateReport(state);
            }
        }

        DmsDataset dmsDataset;

        DmsDatasetTableAdapters.DMS_ERPTableAdapter erp_DataAdapter;
        DmsDatasetTableAdapters.DMS_MFPTableAdapter mfp_DataAdapter;

        DmsDatasetTableAdapters.DMS_ERPHistoricTableAdapter erphist_DataAdapter;
        DmsDatasetTableAdapters.DMS_MarriageTableAdapter marriage_DataAdapter;
        DmsDatasetTableAdapters.DMS_MFPHistoricTableAdapter mfphist_DataAdapter;

        bool cancelThread, pauseThread;

        Thread worker;

        AuthData ad;

        #region Constructor

        public DataStorageSQL()
        {
            ad = RegAuth.GetRegData();

            dmsDataset = new DmsDataset();

            pauseThread = true;

            worker = new Thread(Cycle);
            worker.Name = "dmsqw";
            worker.IsBackground = true;
            worker.Priority = ThreadPriority.Lowest;
            worker.Start();
        }

        #endregion

        public void Init()
        {
            pauseThread = false;
        }

        private void InitAdapters()
        {
            try
            {
                mfp_DataAdapter = new DmsDatasetTableAdapters.DMS_MFPTableAdapter();
                mfphist_DataAdapter = new DmsDatasetTableAdapters.DMS_MFPHistoricTableAdapter();
                erp_DataAdapter = new DmsDatasetTableAdapters.DMS_ERPTableAdapter();
                erphist_DataAdapter = new DmsDatasetTableAdapters.DMS_ERPHistoricTableAdapter();
                marriage_DataAdapter = new DmsDatasetTableAdapters.DMS_MarriageTableAdapter();

                mfp_DataAdapter.Fill(dmsDataset.DMS_MFP);
                State = DataStorageState.Ready;
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine("catched: {0}", sqlEx.Message, null);
                if (sqlEx.Class >= 20)
                {
                    mfp_DataAdapter.Dispose();
                    mfphist_DataAdapter.Dispose();
                    erp_DataAdapter.Dispose();
                    erphist_DataAdapter.Dispose();
                    marriage_DataAdapter.Dispose();
                    State = DataStorageState.Offline;
                }
                else
                {
                    State = DataStorageState.Ready;
                }
            }
            catch (Exception allEx)
            {
                System.Diagnostics.Debug.WriteLine("catched: {0}", allEx.Message, null);
            }
        }

        public void StoreProductionData(List<string> collection)
        {
            int all, ok, nok;
            all = collection.Count;
            ok = nok = 0;

            if (collection == null || collection.Count == 0)
            {
                OnDataStorageImportResult(string.Format("No Data To Import"));
                return;
            }

            DelegateCollection.Classes.ImportUpdateData iud = new DelegateCollection.Classes.ImportUpdateData("", 0, 0, 0, collection.Count);
            string sql = string.Empty;
            try
            {
                foreach (string datatoimport in collection)
                {
                    bool fail = false;
                    if (datatoimport.Length != 21) fail = true;

                    int ForeignSkid;
                    if (!int.TryParse(datatoimport.Substring(0, 4), out ForeignSkid)) fail = true;

                    int DerivativeCode;
                    if (!int.TryParse(datatoimport.Substring(4, 3), out DerivativeCode)) fail = true;

                    int Colour;
                    if (!int.TryParse(datatoimport.Substring(7, 3), out Colour)) fail = true;

                    int BSN;
                    if (!int.TryParse(datatoimport.Substring(10, 6), out BSN)) fail = true;

                    int Track;
                    if (!int.TryParse(datatoimport.Substring(16, 1), out Track)) fail = true;

                    int Roof;
                    if (!int.TryParse(datatoimport.Substring(17, 1), out Roof)) fail = true;

                    int HoD;
                    if (!int.TryParse(datatoimport.Substring(18, 1), out HoD)) fail = true;

                    int Spare;
                    if (!int.TryParse(datatoimport.Substring(19, 2), out Spare)) fail = true;

                    DateTime Timestamp = DateTime.Now;

                    long User = 0;

                    if (fail)
                    {
                        nok++;
                        continue;
                    }

                    int? checkedForeignSkid = erp_DataAdapter.CheckExistForeignSkid(ForeignSkid);

                    if (!checkedForeignSkid.HasValue)
                    {
                        erp_DataAdapter.Insert(
                            ForeignSkid,
                            DerivativeCode,
                            Colour,
                            BSN,
                            Track,
                            Roof,
                            HoD,
                            Spare,
                            Timestamp,
                            User,
                            null);
                        ok++;
                    }
                    else
                    {
                        nok++;
                    }
                    iud.Msg = datatoimport;
                    iud.OK = ok;
                    iud.NOK = nok;
                    iud.IST = ok + nok;
                    OnDataStorageImportUpdate(iud);
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine("catched: {0}", sqlEx.Message, null);
                if (sqlEx.Class >= 20)
                    State = DataStorageState.Offline;
            }
            catch (Exception allEx)
            {
                System.Diagnostics.Debug.WriteLine("catched: {0}", allEx.Message, null);
            }
            finally
            {
                OnDataStorageImportResult("");
            }
        }

        public void UpdateMFP(List<int> data)
        {
            if (state != DataStorageState.Ready)
            {
                System.Diagnostics.Debug.WriteLine("UpdateMFP() cannot complete, db connection is down");
                return;
            }

            var tab = mfp_DataAdapter.GetData();

            int mfpindex = 0;
            foreach (int skid in data)
            {
                if (tab.Rows.Contains(mfpindex))
                {
                    DmsDataset.DMS_MFPRow row = tab.FindByMfpPos(mfpindex);
                    if (row.fk_LocalSkidNr != skid)
                    {
                        row.fk_LocalSkidNr = skid;
                    }
                }
                else
                {
                    tab.Rows.Add(
                        mfpindex,
                        null,
                        skid);
                }
                mfpindex++;
            }

            try
            {
                mfp_DataAdapter.Update(tab);
                tab.Dispose();
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine("catched: {0}", sqlEx.Message, null);
                if (sqlEx.Class >= 20)
                    State = DataStorageState.Offline;
            }
            catch (Exception allEx)
            {
                System.Diagnostics.Debug.WriteLine("catched: {0}", allEx.Message, null);
            }
            //OnDataStorageMfpUpdateEvent();
        }

        private void Cycle()
        {
            while (!cancelThread)
            {
                if (!pauseThread)
                {
                    Thread.CurrentThread.IsBackground = false;
                    switch (state)
                    {
                        case DataStorageState.Init:
                            State = DataStorageState.Connecting;
                            break;
                        case DataStorageState.Connecting:
                            InitAdapters();
                            break;
                        case DataStorageState.Offline:
                            if (erp_DataAdapter.Connection != null) erp_DataAdapter.Connection.Dispose();
                            if (mfp_DataAdapter.Connection != null) mfp_DataAdapter.Connection.Dispose();
                            Thread.Sleep(2000);
                            State = DataStorageState.Connecting;
                            break;
                        case DataStorageState.Ready:
                            Thread.Sleep(100);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(100);
                }
            }
        }

        public void ProcessModel(DataModel ReceivedDataModel)
        {
            if (ReceivedDataModel is MFPDataModel)
                UpdateMFP(((MFPDataModel)ReceivedDataModel).Mfps);
            if (ReceivedDataModel is DataSetReqDataModel)
                ProcessDataSetBySkid(ReceivedDataModel as DataSetReqDataModel);
        }

        private void ProcessDataSetBySkid(DataSetReqDataModel dataSetReqDataModel)
        {
            try
            {
                var foreignSkidIdFromDb = erp_DataAdapter.CheckExistForeignSkid(dataSetReqDataModel.RequestForeignID);

                if (foreignSkidIdFromDb.HasValue)
                {
                    var row = erp_DataAdapter.GetDataByForeignSkid(dataSetReqDataModel.RequestForeignID).First();
                    var updatenr = marriage_DataAdapter.UpdateByLocalSkid(row.fk_ErpHistId, dataSetReqDataModel.RequestLocalID);
                    if (updatenr == 0)
                        marriage_DataAdapter.Insert(dataSetReqDataModel.RequestLocalID, row.fk_ErpHistId);
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine("catched: {0}", sqlEx.Message, null);
                if (sqlEx.Class >= 20)
                    State = DataStorageState.Offline;
            }
            catch (Exception allEx)
            {
                System.Diagnostics.Debug.WriteLine("catched: {0}", allEx.Message, null);
            }
        }

        private void ProcessDataSetByBSN(DataSetReqDataModel dataSetReqDataModel)
        {
            //try
            //{
            //    using (var erp = erp_DataAdapter.GetData())
            //    {
            //        var q = (from r in erp
            //                 where r.BSN == dataSetReqDataModel.RequestForeignID
            //                 select r);
            //        if (q.Count() != 1)
            //            dataSetReqDataModel.OnDataSetReceived(null);
            //        else
            //        {
            //            var row = q.First();
            //            dataSetReqDataModel.OnDataSetReceived(new ErpDataset()
            //            {
            //                BSN = row.BSN,
            //                Colour = row.Colour,
            //                DerivativeCode = row.DerivativeCode,
            //                HoD = row.Door,
            //                Roof = row.Roof,
            //                SkidID = row.SkidID,
            //                Track = row.Track,
            //                Spare = row.Spare
            //            }
            //            );
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine("{1}\r\n{0}", ex.Message, ex.Source);
            //    State = DataStorageState.Offline;
            //}
        }

        #region Delegates/Events

        private void OnDataStorageImportUpdate(DelegateCollection.Classes.ImportUpdateData updatedata)
        {
            if (DataStorageImportUpdate != null)
            {
                DataStorageImportUpdate(updatedata);
            }
        }
        public DelegateCollection.DataStorageImportUpdate DataStorageImportUpdate { get; set; }

        private void OnDataStorageImportResult(string msg, int items = 0, int duplicates = 0)
        {
            if (DataStorageImportResult != null)
            {
                DataStorageImportResult(msg, items, duplicates);
            }
        }
        public DelegateCollection.DataStorageImportResult DataStorageImportResult { get; set; }

        private void OnDataStorageStateReport(DataStorageState state)
        {
            if (DataStorageStateReport != null)
            {
                DataStorageStateReport(state);
            }
        }
        public DelegateCollection.DataStorageStateReport DataStorageStateReport { get; set; }

        private void OnDataStorageModeReport(DataStorageState oldstate, DataStorageState newstate)
        {
            if (DataStorageModeReport != null)
            {
                DataStorageModeReport(oldstate, newstate);
            }
        }
        public DelegateCollection.DataStorageModeReport DataStorageModeReport { get; set; }

        private void OnDataStorageMfpUpdateEvent()
        {
            if (DataStorageMfpUpdateEvent != null)
            {
                DataStorageMfpUpdateEvent(dmsDataset.DMS_MFP);
            }
        }
        public DelegateCollection.DataStorageMfpUpdateEvent DataStorageMfpUpdateEvent { get; set; }

        private void OnDataStorageErpUpdateEvent()
        {
            if (DataStorageErpUpdateEvent != null)
            {
                DataStorageErpUpdateEvent(dmsDataset.DMS_ERP);
            }
        }
        public DelegateCollection.DataStorageErpUpdateEvent DataStorageErpUpdateEvent { get; set; }

        #endregion

        ~DataStorageSQL()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //someone want the deterministic release of all resources
                //Let us release all the managed resources
                if (worker != null && worker.IsAlive)
                {
                    cancelThread = true;
                }
                //while (queueworker.IsAlive)
                //{
                //    ;
                //}
                //if (command != null)
                //{
                //    command.Dispose();
                //    command = null;
                //}
                //if (connection != null)
                //{
                //    connection.Dispose();
                //    connection = null;
                //}
            }
            else
            {
                // Do nothing, no one asked a dispose, the object went out of
                // scope and finalized is called so lets next round of GC 
                // release these resources
            }

            // Release the unmanaged resource in any case as they will not be 
            // released by GC
        }
    }
}