using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common.datamodels
{
    public class DataSetReqDataModel : DataModel
    {
        public string RequestModelID { get; set; }
        public string ResponseDataSet { get; set; }

        public delegate void DataSetReceivedDelegate(byte[] dataSet);
        public DataSetReceivedDelegate DataSetReceived { get; set; }

        public DataSetReqDataModel(short size, byte type, System.IO.BinaryReader br)
            : base(size, type)
        {
            int count = (Size - 4) / 2;
            RequestModelID = string.Empty;
            RequestModelID = new string(br.ReadChars(count));
        }

        public void OnDataSetReceived(string dataset)
        {
            if (DataSetReceived != null)
            {
                DataSetReceived(Encoding.UTF8.GetBytes(dataset));
            }
        }
    }
}
