using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dmspl.common
{
    public static class DelegateCollection
    {
        public delegate void DataStorageImportUpdate(IDataStorage whosend, Classes.ImportUpdateData updatedata);
        public delegate void DataStorageImportResult(IDataStorage whosend, string msg, int items, int duplicates);
        public delegate void DataStorageStateReport(IDataStorage whosend, ConnectionState oldstate, ConnectionState newstate);
        public delegate void DataStorageModeReport(IDataStorage whosend, DataStorageMode oldstate, DataStorageMode newstate);
        public delegate void DataStorageMfpUpdateEvent(DataTable mfpdt);
        public delegate void DataStorageErpUpdateEvent(DataTable erpdt);
        
        public delegate void SetTextDel(string d);

        public delegate void DataStorageInvokeUpdate(IDataStorage whosend, object data);

        public class Classes
        {
            public class ImportUpdateData
            {
                public string Msg { get; set; }
                public int OK { get; set; }
                public int NOK { get; set; }
                public int IST { get; set; }
                public int ALL { get; set; }

                public ImportUpdateData(string s, int ok, int nok, int ist, int all)
                {
                    Msg = s;
                    OK = ok;
                    NOK = nok;
                    IST = ist;
                    ALL = all;
                }
            }
        }
    }
}
