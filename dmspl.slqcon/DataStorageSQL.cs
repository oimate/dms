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
using dmspl.common.log;

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
                //dbg:dsstat
                DataLog.Log(DateTime.Now, Module.DataBase, EvType.Info, Level.Main, "dbg:dsstat|" + state);
                OnDataStorageModeReport(state);
            }
        }

        DmsDataset dmsDataset;

        //DmsDataset.DMS_ERPDataTable DMS_ERP;
        DmsDatasetTableAdapters.DMS_ERPTableAdapter erp_DataAdapter;

        DmsDatasetTableAdapters.DMS_MarriageTableAdapter marriage_DataAdapter;

        DmsDataset.DMS_MFPDataTable DMS_MFP;
        DmsDatasetTableAdapters.DMS_MFPTableAdapter mfp_DataAdapter;

        bool cancelThread, pauseThread;
        Thread queueworker;

        AuthData ad;

        #region Constructor
        public DataStorageSQL()
        {
            ad = RegAuth.GetRegData();

            dmsDataset = new DmsDataset();

            mfp_DataAdapter = new DmsDatasetTableAdapters.DMS_MFPTableAdapter();
            erp_DataAdapter = new DmsDatasetTableAdapters.DMS_ERPTableAdapter();
            marriage_DataAdapter = new DmsDatasetTableAdapters.DMS_MarriageTableAdapter();

            //InitAdapters();

            //userManager = new usermanager.UserManager(dmsDataset);
            //if (!userManager.Login("sli", "sli"))
            //    throw new InvalidOperationException("login failed");

            State = DataStorageState.Initializing;

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
                DataLog.Log(Module.DataBase, EvType.Error, Level.Details, string.Format("SqlException, Class:{0:D2}, Msg:\r\n{1}", sqlex.Class, sqlex.Message));
                if (sqlex.Class >= 20)
                    State = DataStorageState.Offline;
            }
            catch (Exception ex)
            {
                DataLog.Log(Module.DataBase, EvType.Error, Level.Debug, string.Format("Exception, Msg:\r\n{1}", ex.Message, null));
            }
        }
        #endregion

        //public void StoreProductionData(List<string> collection)
        //{
        //    int all, ok, nok;
        //    all = collection.Count;
        //    ok = nok = 0;

        //    if (collection == null || collection.Count == 0)
        //    {
        //        OnDataStorageImportResult(string.Format("No Data To Import"));
        //        DataLog.Log(Module.DataBase, EvType.Info, Level.Main, "No Data To Import");
        //        return;
        //    }

        //    DelegateCollection.Classes.ImportUpdateData iud = new DelegateCollection.Classes.ImportUpdateData("", 0, 0, 0, collection.Count);
        //    string sql = string.Empty;
        //    try
        //    {
        //        foreach (string datatoimport in collection)
        //        {
        //            bool fail = false;
        //            if (datatoimport.Length != 21) fail = true;

        //            int ForeignSkid;
        //            if (!int.TryParse(datatoimport.Substring(0, 4), out ForeignSkid)) fail = true;

        //            int DerivativeCode;
        //            if (!int.TryParse(datatoimport.Substring(4, 3), out DerivativeCode)) fail = true;

        //            int Colour;
        //            if (!int.TryParse(datatoimport.Substring(7, 3), out Colour)) fail = true;

        //            int BSN;
        //            if (!int.TryParse(datatoimport.Substring(10, 6), out BSN)) fail = true;

        //            int Track;
        //            if (!int.TryParse(datatoimport.Substring(16, 1), out Track)) fail = true;

        //            int Roof;
        //            if (!int.TryParse(datatoimport.Substring(17, 1), out Roof)) fail = true;

        //            int HoD;
        //            if (!int.TryParse(datatoimport.Substring(18, 1), out HoD)) fail = true;

        //            int Spare;
        //            if (!int.TryParse(datatoimport.Substring(19, 2), out Spare)) fail = true;

        //            DateTime Timestamp = DateTime.Now;

        //            long User = 0;

        //            if (fail)
        //            {
        //                nok++;
        //                continue;
        //            }

        //            int? checkedSkid = erp_DataAdapter.CheckExistsSkid(ForeignSkid);

        //            if (!checkedSkid.HasValue)
        //            {
        //                erp_DataAdapter.Insert(
        //                    ForeignSkid,
        //                    DerivativeCode,
        //                    Colour,
        //                    BSN,
        //                    Track,
        //                    Roof,
        //                    HoD,
        //                    Spare,
        //                    Timestamp,
        //                    User,
        //                    null);
        //                ok++;
        //                DataLog.Log(Module.DataBase, EvType.Info, Level.Main, string.Format("dataset imported: {0}", datatoimport));
        //            }
        //            else
        //            {
        //                nok++;
        //                DataLog.Log(Module.DataBase, EvType.Warning, Level.Main, string.Format("dataset exists: {0}", datatoimport));
        //                //string msg = string.Format("{0}, {1}, {2}, {3}", ok, nok, ok + nok, all);
        //                //OnDataStorageImportUpdate(new DelegateCollection.Classes.ImportUpdateData(msg, ok, nok, ok + nok, all));
        //                //Thread.Sleep(1);
        //            }
        //            iud.Msg = datatoimport;
        //            iud.OK = ok;
        //            iud.NOK = nok;
        //            iud.IST = ok + nok;
        //            OnDataStorageImportUpdate(iud);
        //        }
        //        //erp_DataAdapter.Update(dmsDataset.DMS_ERP);
        //        //OnDataStorageErpUpdateEvent();
        //        //OnDataStorageImportResult("datastorageimportresult");
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        DataLog.Log(Module.DataBase, EvType.Error, Level.Details, string.Format("SqlException, Class:{0:D2}, Msg:\r\n{1}", sqlex.Class, sqlex.Message));
        //        if (sqlex.Class >= 20)
        //            State = DataStorageState.Offline;
        //    }
        //    catch (Exception ex)
        //    {
        //        DataLog.Log(Module.DataBase, EvType.Error, Level.Debug, string.Format("Exception, Msg:\r\n{1}", ex.Message, null));
        //    }
        //    finally
        //    {
        //        OnDataStorageImportResult("finally");
        //    }
        //}

        public void UpdateMFP(List<int> data)
        {
            if (DMS_MFP == null)
            {
                DataLog.Log(Module.DataBase, EvType.Error, Level.Debug, "DMS_MFP == null during UpdateMFP()");
                return;
            }
            lock (DMS_MFP)
            {
                try
                {
                    int mfpindex = 0;
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

                    mfp_DataAdapter.Update(DMS_MFP);
                }
                catch (SqlException sqlex)
                {
                    DataLog.Log(Module.DataBase, EvType.Error, Level.Details, string.Format("SqlException, Class:{0:D2}, Msg:\r\n{1}", sqlex.Class, sqlex.Message));
                    if (sqlex.Class >= 20)
                        State = DataStorageState.Offline;
                }
                catch (Exception ex)
                {
                    DataLog.Log(Module.DataBase, EvType.Error, Level.Debug, string.Format("Exception, Msg:\r\n{1}", ex.Message, null));
                }
            }
            OnDataStorageMfpUpdateEvent();
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
                            Thread.Sleep(1000);
                            State = DataStorageState.Connecting;
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

        public void ProcessModel(DataModel ReceivedDataModel)
        {
            if (ReceivedDataModel is MFPDataModel)
                UpdateMFP(((MFPDataModel)ReceivedDataModel).Mfps);
            //if (ReceivedDataModel is DataSetReqDataModelByBSN)
            //    GetDataSetByBSN(ReceivedDataModel as DataSetReqDataModelByBSN);
            if (ReceivedDataModel is DataSetMarriageFromPlc)
                MarriageFromPlc(ReceivedDataModel as DataSetMarriageFromPlc);
            if (ReceivedDataModel is SkidExitDataModel)
                SkidExit(ReceivedDataModel as SkidExitDataModel);
        }

        private void SkidExit(SkidExitDataModel skidExitDataModel)
        {
            var id = erp_DataAdapter.CheckExistsSkid(skidExitDataModel.Skid);
            if (id != null)
            {
                //dbg:exitok
                DataLog.Log(Module.DataBase, EvType.Info, Level.Debug, "dbg:exitok|removed " + skidExitDataModel.Skid.ToString());
                erp_DataAdapter.UpdateLeftPlantById(true, id.Value);
            }
            else
            {
                //dbg:exitnok
                DataLog.Log(Module.DataBase, EvType.Info, Level.Debug, "dbg:exitnok|not removed " + skidExitDataModel.Skid.ToString());
            }
        }

        private void MarriageFromPlc(DataSetMarriageFromPlc dataSetMarriageFromPlc)
        {
            try
            {
                ErpDataset eds = dataSetMarriageFromPlc.Erpdataset;

                var Id = erp_DataAdapter.FindSkidByDataset(eds.SkidID, eds.DerivativeCode, eds.Colour, eds.BSN, eds.Track, eds.Roof, eds.Hood, eds.Spare, false);

                if (Id == null)
                {
                    var y = erp_DataAdapter.Insert(eds.SkidID, eds.DerivativeCode, eds.Colour, eds.BSN, eds.Track, eds.Roof, eds.Hood, eds.Spare, false, DateTime.Now, 0);
                    //dbg:marrok
                    DataLog.Log(Module.DataBase, EvType.Info, Level.Debug, "dbg:marrok|added " + dataSetMarriageFromPlc.Erpdataset.ToString());
                }
                else
                {
                    //dbg:marrnok
                    DataLog.Log(Module.DataBase, EvType.Info, Level.Debug, "dbg:marrnok|already exists (skid " + Id.Value + ") " + dataSetMarriageFromPlc.Erpdataset.ToString());
                }
            }
            catch (Exception e)
            {
                //dbg:marrexc
                DataLog.Log(Module.DataBase, EvType.Info, Level.Debug, "dbg:marrexc|problem with " + dataSetMarriageFromPlc.Erpdataset.ToString());
                //dbg:mafrpl
                DataLog.Log(Module.DataBase, EvType.Error, Level.Main, "dbg:mafrpl|" + e.Message);
            }
        }

        //private void GetDataSetByBSN(DataSetReqDataModelByBSN Request)
        //{
        //    try
        //    {
        //        var collection = erp_DataAdapter.GetErpDataTableByBSN(Request.RequestBSN);
        //        if (collection.Count == 1)
        //        {
        //            var row = collection[0];
        //            ErpDataset eds = new ErpDataset()
        //            {
        //                BSN = row.BSN,
        //                Colour = row.Colour,
        //                DerivativeCode = row.DerivativeCode,
        //                Hood = row.Door,
        //                Roof = row.Roof,
        //                SkidID = row.ForeignSkid,
        //                Track = row.Track
        //            };
        //            marriage_DataAdapter.Insert(Request.RequestLocalnID, row.fk_ErpHistId);
        //            Request.ResponseReady(eds);
        //        }
        //        else
        //        {
        //            Request.ResponseReady(new ErpDataset() { BSN = Request.RequestBSN, SkidID = int.MaxValue, Colour = int.MaxValue, DerivativeCode = int.MaxValue, Hood = int.MaxValue, Roof = int.MaxValue, Track = int.MaxValue });
        //        }
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        DataLog.Log(Module.DataBase, EvType.Error, Level.Details, string.Format("SqlException, Class:{0:D2}, Msg:\r\n{1}", sqlex.Class, sqlex.Message));
        //        if (sqlex.Class >= 20)
        //            State = DataStorageState.Offline;
        //    }
        //    catch (Exception ex)
        //    {
        //        DataLog.Log(Module.DataBase, EvType.Error, Level.Debug, string.Format("Exception, Msg:\r\n{1}", ex.Message, null));
        //    }
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