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
        public ErpDataset Erpdataset { get; set; }

        public DataSetReqDataModel(short size, byte type, System.IO.BinaryReader br, DataSetReceivedDelegate dataSetReceived)
            : base(size, type)
        {
            RequestForeignID = System.Net.IPAddress.NetworkToHostOrder(br.ReadInt32());
            RequestLocalnID = System.Net.IPAddress.NetworkToHostOrder(br.ReadInt16());

            DataSetReceived = dataSetReceived;
        }

        public void OnDataSetReceived(ErpDataset dataset)
        {
            if (DataSetReceived != null)
            {
                if (dataset == null)
                {
                    DataSetReceived(null);
                    return;
                }
                Size = 6 + 4;
                this.Erpdataset = dataset;
                DataSetReceived(this);
            }
        }

        public override byte[] GetRawData()
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(new byte[Size]))
            {
                using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms))
                {
                    bw.Write(Type);
                    bw.Write((Int16)Size);
                    bw.Write((byte)(Erpdataset.SkidID >> 8));
                    bw.Write((byte)Erpdataset.SkidID);
                    bw.Write((byte)(Erpdataset.BSN >> 24));
                    bw.Write((byte)(Erpdataset.BSN >> 16));
                    bw.Write((byte)(Erpdataset.BSN >> 8));
                    bw.Write((byte)Erpdataset.BSN);
                    bw.Write(CRC);
                    return ms.ToArray();
                }
            }
        }
    }
}
