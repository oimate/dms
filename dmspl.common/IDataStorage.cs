using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common
{
    public interface IDataStorage
    {
        //responsible for inserting provided data to database------------
        void StoreProductionData(List<string> collection);
        void StartThread();
        //---------------------------------------------------------------

        //
        IUDPComm UdpComm { get; set; }
        //

        //
        void UpdateMFP(List<int> data);
        Task UpdateMFPAsync(List<int> data);
        //

        //
        void PauseResumeThread();
        //

        //
        DelegateCollection.DataStorageImportUpdate DataStorageImportUpdate { get; set; }
        DelegateCollection.DataStorageImportResult DataStorageImportResult { get; set; }
        DelegateCollection.DataStorageMfpUpdateEvent DataStorageMfpUpdateEvent { get; set; }
        DelegateCollection.DataStorageErpUpdateEvent DataStorageErpUpdateEvent { get; set; }
        DelegateCollection.DataStorageStateReport DataStorageStateReport { get; set; }
        DelegateCollection.DataStorageModeReport DataStorageModeReport { get; set; }
        //

        void ProcessModel(DataModel ReceivedDataModel);

        //Task<bool> Login(string u, string p);
    }
}
