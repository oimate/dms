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
        DmsDataset dmsDataset;

        DmsDatasetTableAdapters.DMS_ERPTableAdapter erp_DataAdapter;
        DmsDatasetTableAdapters.DMS_MFPTableAdapter mfp_DataAdapter;

        volatile bool mfpupdating;

        usermanager.UserManager userManager;

        DataStorageMode mode, lastmode;

        public IUDPComm UdpComm { get; set; }

        private DataStorageMode Mode
        {
            get { return mode; }
            set
            {
                lastmode = mode;
                mode = value;
                OnDataStorageModeReport(lastmode, mode);
            }
        }

        bool cancelThread, pauseThread;
        Thread queueworker;

        AuthData ad;
        //string connectionstring;
        //SqlConnection connection;

        #region Constructor
        public DataStorageSQL()
        {
            ad = RegAuth.GetRegData();

            dmsDataset = new DmsDataset();

            mfp_DataAdapter = new DmsDatasetTableAdapters.DMS_MFPTableAdapter();
            mfp_DataAdapter.Fill(dmsDataset.DMS_MFP);

            erp_DataAdapter = new DmsDatasetTableAdapters.DMS_ERPTableAdapter();
            erp_DataAdapter.Fill(dmsDataset.DMS_ERP);

     //       userManager = new usermanager.UserManager(dmsDataset);
      //      if (!userManager.Login("sli", "sli"))
       //         throw new InvalidOperationException("login failed");

            lastmode = DataStorageMode.Ready;
            mode = DataStorageMode.Init;

            //processedqueue = new List<FileInfo>();

            //CreateDataAdapters(connection);

            mfpupdating = false;

            cancelThread = false;
            pauseThread = false; ;

            queueworker = new Thread(Cycle);
            queueworker.Name = "dmsqw";
            queueworker.IsBackground = true;
            queueworker.Priority = ThreadPriority.Lowest;
            //queueworker.Start();
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
            string sql = string.Empty;
            try
            {
                foreach (string datatoimport in collection)
                {
                    //dataset structure is fixed
                    string SkidID = datatoimport.Substring(0, 4);
                    int Id;
                    if (!int.TryParse(SkidID, out Id)) continue;
                    string DerivativeCode = datatoimport.Substring(4, 3);
                    string Colour = datatoimport.Substring(7, 3);
                    string BSN = datatoimport.Substring(10, 6);
                    string Track = datatoimport.Substring(16, 1);
                    string CSOB = datatoimport.Substring(17, 4);
                    DateTime CreateTimestamp = DateTime.Now;
                    long CreateUser = userManager.Currentuser.Id;

                    bool exists = dmsDataset.DMS_ERP.Rows.Contains(Id);

                    if (!exists)
                    {
                        dmsDataset.DMS_ERP.Rows.Add(
                            Id,
                            datatoimport,
                            SkidID,
                            DerivativeCode,
                            Colour,
                            BSN,
                            Track,
                            CSOB,
                            CreateTimestamp,
                            CreateUser,
                            null,
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
                }
                erp_DataAdapter.Update(dmsDataset.DMS_ERP);
                OnDataStorageErpUpdateEvent();
                OnDataStorageImportResult("datastorageimportresult");
            }
            catch (Exception ex)
            {
                OnDataStorageImportResult("data import unsuccessful!", collection.Count);
            }
        }

        public async Task UpdateMFPAsync(List<int> data)
        {
            //if (connection.State != ConnectionState.Open)
            //    return;
            await Task.Run(() => UpdateMFP(data));
        }
        public void UpdateMFP(List<int> data)
        {
            int counter = 0;
            while (mfpupdating)
            {
                System.Diagnostics.Debug.WriteLine("while mfpr: {0}", counter++);
                Thread.Sleep(5);
            }
            mfpupdating = true;
            int mfpindex = 1;
            foreach (int skid in data)
            {
                string newskid = string.Format("{0:D4}", skid);
                if (dmsDataset.DMS_MFP.Rows.Contains(mfpindex))
                {
                    DmsDataset.DMS_MFPRow row = dmsDataset.DMS_MFP.FindById(mfpindex);
                    if (row.localskidnr != skid)
                    {
                        row.localskidnr = skid;
                    }
                }
                else
                {
                    dmsDataset.DMS_MFP.Rows.Add(
                        mfpindex,
                        null,
                        skid,
                        null);
                }
                mfpindex++;
            }

            try
            {
                mfp_DataAdapter.Update(dmsDataset.DMS_MFP);
            }
            catch (Exception e)
            {
                dmsDataset.DMS_MFP.RejectChanges();
                dmsDataset.RejectChanges();
                System.Diagnostics.Debug.WriteLine(string.Format("catched: {0}", e.Message));
                Thread.Sleep(5000);
                dmsDataset.Reset();
                mfp_DataAdapter.Fill(dmsDataset.DMS_MFP);
                erp_DataAdapter.Fill(dmsDataset.DMS_ERP);
            }

            OnDataStorageMfpUpdateEvent();
            mfpupdating = false;
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
                    switch (Mode)
                    {
                        case DataStorageMode.Connecting:
                            //OnDataStorageStateReport(connection.State, connection.State);
                            Thread.Sleep(5000);
                            Mode = DataStorageMode.Init;
                            break;
                        case DataStorageMode.Init:
                            try
                            {
                                //if (connection.State != ConnectionState.Open) connection.Open();
                                //OnDataStorageErpUpdateEvent();
                                Mode = DataStorageMode.Ready;
                                pauseThread = true;
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine("+++> {0}{1}{2}", ex.GetType(), ex.Message, ex.Source);
                                Mode = DataStorageMode.Connecting;
                                //Thread.Sleep(5000);
                            }
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
        }

        public async Task<bool> Login(string u, string p)
        {
            bool success = await Task<bool>.Run(() => { return userManager.Login(u, p); });
            return success;
        }

        #region Delegates/Events

        private void connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            OnDataStorageStateReport(e.OriginalState, e.CurrentState);
        }

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

        private void OnDataStorageStateReport(ConnectionState oldstate, ConnectionState newstate)
        {
            if (DataStorageStateReport != null)
            {
                DataStorageStateReport(this, oldstate, newstate);
            }
        }
        public DelegateCollection.DataStorageStateReport DataStorageStateReport { get; set; }

        private void OnDataStorageModeReport(DataStorageMode oldstate, DataStorageMode newstate)
        {
            if (DataStorageModeReport != null)
            {
                DataStorageModeReport(this, oldstate, newstate);
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