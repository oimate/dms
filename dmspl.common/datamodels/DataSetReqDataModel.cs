using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common.datamodels
{
    public class DataSetReqDataModel : DataModel
    {
        public int RequestForeignID { get; set; }
        public int RequestLocalnID { get; set; }
        public string ResponseDataSet { get; set; }
        public byte[] Dataset { get; set; }

        public DataSetReqDataModel(short size, byte type, System.IO.BinaryReader br, DataSetReceivedDelegate dataSetReceived )
            : base(size, type)
        {
            RequestForeignID =System.Net.IPAddress.NetworkToHostOrder( br.ReadInt32());
            RequestLocalnID = System.Net.IPAddress.NetworkToHostOrder(br.ReadInt16());

            DataSetReceived = dataSetReceived;
        }

        public void OnDataSetReceived(string dataset)
        {
            if (DataSetReceived != null)
            {
                Dataset = new byte[dataset.Length];

                for (int i = 0; i < Dataset.Length; i++)
                {
                    Dataset[i] = (byte)(Convert.ToByte(dataset[i])  - (byte)0x30 );   
                }
              

                Size = dataset.Length + 4;
                DataSetReceived(this);
            }
        }
    }
}
