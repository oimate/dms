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
        private DataStorageState State
        {
            get { return state; }
            set
            {
                state = value;
                OnDataStorageModeReport(state);
            }
        }

        DmsDataset dmsDataset;

        DmsDataset.DMS_ERPDataTable DMS_ERP;
        DmsDatasetTableAdapters.DMS_ERPTableAdapter erp_DataAdapter;

        DmsDataset.DMS_MFPDataTable DMS_MFP;
        DmsDatasetTableAdapters.DMS_MFPTableAdapter mfp_DataAdapter;

        bool cancelThread, pauseThread;
        Thread queueworker;

        AuthData ad;

        dmspl.common.log.IDataLog LogObj;

        #region Constructor
        public DataStorageSQL()
        {
            ad = RegAuth.GetRegData();

            dmsDataset = new DmsDataset();

            mfp_DataAdapter = new DmsDatasetTableAdapters.DMS_MFPTableAdapter();
            erp_DataAdapter = new DmsDatasetTableAdapters.DMS_ERPTableAdapter();
            
            //InitAdapters();

            //userManager = new usermanager.UserManager(dmsDataset);
            //if (!userManager.Login("sli", "sli"))
            //    throw new InvalidOperationException("login failed");

            state = DataStorageState.Initializing;

            //processedqueue = new List<FileInfo>();

            //CreateDataAdapters(connection);

            cancelThread = false;
            pauseThread = false;

            queueworker = new Thread(Cycle);
            queueworker.Name = "dmsqw";
            queueworker.IsBackground = true;
            queueworker.Priority = ThreadPriority.Lowest;
            queueworker.Start();
        }

        private void InitAdapters()
        {
            try
            {
                if (DMS_MFP != null)
                    DMS_MFP.Dispose();
                DMS_MFP = mfp_DataAdapter.GetData();
                State = DataStorageState.Ready;
            }
            catch (SqlException sqlex)
            {
                System.Diagnostics.Debug.WriteLine("{0}", sqlex.Message, null);
                if (sqlex.Class >= 20)
                    State = DataStorageState.Offline;
            }
        }
        #endregion

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
                        //string msg = string.Format("{0}, {1}, {2}, {3}", ok, nok, ok + nok, all);
                        //OnDataStorageImportUpdate(new DelegateCollection.Classes.ImportUpdateData(msg, ok, nok, ok + nok, all));
                        //Thread.Sleep(1);
                    }
                    iud.Msg = datatoimport;
                    iud.OK = ok;
                    iud.NOK = nok;
                    iud.IST = ok + nok;
                    OnDataStorageImportUpdate(iud);
                }
                //erp_DataAdapter.Update(dmsDataset.DMS_ERP);
                //OnDataStorageErpUpdateEvent();
                //OnDataStorageImportResult("datastorageimportresult");
            }
            catch (SqlException sqlEx)
            {
                OnDataStorageImportResult("data import unsuccessful!", collection.Count);
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
        //public async Task UpdateMFPAsync(List<int> data)
        //{
        //    //if (connection.State != ConnectionState.Open)
        //    //    return;
        //    await Task.Run(() => UpdateMFP(data));
        //}

        public void UpdateMFP(List<int> data)
        {
            lock (DMS_MFP)
            {
                int mfpindex = 1;

                DMS_MFP = mfp_DataAdapter.GetData();

                foreach (int skid in data)
                {
                    string newskid = string.Format("{0:D4}", skid);
                    if (DMS_MFP.Rows.Contains(mfpindex))
                    {
                        DmsDataset.DMS_MFPRow row = DMS_MFP.FindByMfpPos(mfpindex);
                        if (row.fk_LocalSkidNr != skid)
                        {
                            row.fk_LocalSkidNr = skid;
                        }
                    }
                    else
                    {
                        DMS_MFP.Rows.Add(
                            mfpindex,
                            null,
                            skid);
                    }
                    mfpindex++;
                }

                try
                {
                    mfp_DataAdapter.Update(DMS_MFP);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("catched: {0}", e.Message, null);
                    InitAdapters();
                }
            }
            OnDataStorageMfpUpdateEvent();
            //mfpupdating = false;
        }

        public void StartThread()
        {
            if (queueworker != null && queueworker.ThreadState.HasFlag(ThreadState.Unstarted))
                queueworker.Start();
        }
        public void PauseResumeThread()
        {
            pauseThread = !pauseThread;
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
                        case DataStorageState.Initializing:
                            InitAdapters();
                            break;
                        case DataStorageState.Connecting:
                            InitAdapters();
                            break;
                        case DataStorageState.Offline:
                            Thread.Sleep(5000);
                            break;
                        case DataStorageState.Ready:
                            Thread.CurrentThread.IsBackground = true;
                            Thread.Sleep(50);
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

        private void FinalizeCommit(SqlCommand cmd)
        {
            try
            {
                cmd.Transaction.Commit();
                OnDataStorageImportResult(string.Format("ERP Import Commit OK"));
            }
            catch (Exception ex)
            {
                OnDataStorageImportResult(string.Format("ERP Import Commit error"));
            }
        }
        private bool ExecuteQuery(SqlCommand cmd, string sql)
        {
            try
            {
                cmd.CommandText = sql;
                int i = cmd.ExecuteNonQuery();
                if (i <= 0)
                {
                    //OnDataStorageImportUpdate(string.Format("problems with execution: {0}", sql));
                    return false;
                }
                else
                {
                    //OnDataStorageImportUpdate(string.Format("executed: {0}", sql));
                    //System.Diagnostics.Debug.WriteLine(string.Format("\t{0}", sql));
                    return true;
                }

                //if (commitcounter++ == COMMITLIMIT)
                //{
                //    commitcounter = 0;
                //    cmd.Transaction.Commit();
                //    OnDataStorageImportResult(string.Format("commited"));
                //    cmd.Transaction = connection.BeginTransaction();
                //}

            }
            catch (Exception ex)
            {
                try
                {
                    OnDataStorageImportResult(string.Format("{0}", ex.GetType()));
                    cmd.Transaction.Rollback();
                    return false;
                }
                catch (Exception ex2)
                {
                    OnDataStorageImportResult(string.Format("{0}", ex2.GetType()));
                    return false;
                }
            }
        }

        public void ProcessModel(DataModel ReceivedDataModel)
        {
            if (ReceivedDataModel is MFPDataModel)
                UpdateMFP(((MFPDataModel)ReceivedDataModel).Mfps);
            //if (ReceivedDataModel is DataSetReqDataModel)
            //    GetDataSetByBSN(ReceivedDataModel as DataSetReqDataModel);
        }

        //private void GetDataSetBySkid(DataSetReqDataModel dataSetReqDataModel)
        //{
        //    try
        //    {
        //        using (var erp = erp_DataAdapter.GetData())
        //        {
        //            var row = erp.FindBySkidID(dataSetReqDataModel.RequestForeignID);
        //            if (row == null)
        //                dataSetReqDataModel.OnDataSetReceived(null);
        //            else
        //            {
        //                dataSetReqDataModel.OnDataSetReceived(new ErpDataset()
        //                    {
        //                        BSN = row.BSN,
        //                        Colour = row.Colour,
        //                        DerivativeCode = row.DerivativeCode,
        //                        HoD = row.Door,
        //                        Roof = row.Roof,
        //                        SkidID = row.SkidID,
        //                        Track = row.Track,
        //                        Spare = row.Spare
        //                    }
        //                );
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("{1}\r\n{0}", ex.Message, ex.Source);
        //    }
        //}

        //private void GetDataSetByBSN(DataSetReqDataModel dataSetReqDataModel)
        //{
        //    try
        //    {
        //        using (var erp = erp_DataAdapter.GetData())
        //        {
        //            var q = (from r in erp
        //                     where r.BSN == dataSetReqDataModel.RequestForeignID
        //                     select r);
        //            if (q.Count() != 1)
        //                dataSetReqDataModel.OnDataSetReceived(null);
        //            else
        //            {
        //                var row = q.First();
        //                dataSetReqDataModel.OnDataSetReceived(new ErpDataset()
        //                {
        //                    BSN = row.BSN,
        //                    Colour = row.Colour,
        //                    DerivativeCode = row.DerivativeCode,
        //                    HoD = row.Door,
        //                    Roof = row.Roof,
        //                    SkidID = row.SkidID,
        //                    Track = row.Track,
        //                    Spare = row.Spare
        //                }
        //                );
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("{1}\r\n{0}", ex.Message, ex.Source);
        //    }
        //}

        //public async Task<bool> Login(string u, string p)
        //{
        //    bool success = await Task<bool>.Run(() => { return userManager.Login(u, p); });
        //    return success;
        //}

        #region Delegates/Events

        private void OnDataStorageImportUpdate(DelegateCollection.Classes.ImportUpdateData updatedata)
        {
            if (DataStorageImportUpdate != null)
            {
                DataStorageImportUpdate(this, updatedata);
            }
        }
        public DelegateCollection.DataStorageImportUpdate DataStorageImportUpdate { get; set; }

        private void OnDataStorageImportResult(string msg, int items = 0, int duplicates = 0)
        {
            if (DataStorageImportResult != null)
            {
                DataStorageImportResult(this, msg, items, duplicates);
            }
        }
        public DelegateCollection.DataStorageImportResult DataStorageImportResult { get; set; }

        private void OnDataStorageModeReport(DataStorageState newstate)
        {
            if (DataStorageStateReport != null)
            {
                DataStorageStateReport(newstate);
            }
        }
        public DelegateCollection.DataStorageStateReport DataStorageStateReport { get; set; }

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
                if (queueworker != null && queueworker.IsAlive)
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