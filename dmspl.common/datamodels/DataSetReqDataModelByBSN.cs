using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common.datamodels
{
    public class DataSetReqDataModelByBSN : DataModel
    {
        public int RequestBSN { get; set; }
        public int RequestLocalnID { get; set; }
        public string ResponseDataSet { get; set; }
        public byte[] Dataset { get; set; }
        public ErpDataset Erpdataset { get; set; }

        public DataSetReqDataModelByBSN(short size, byte type, System.IO.BinaryReader br, DataSetReceivedDelegate dataSetReceived = null)
            : base(size, type)
        {
            RequestBSN = System.Net.IPAddress.NetworkToHostOrder(br.ReadInt32());
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
                Size = 15;
                this.Erpdataset = dataset;
                DataSetReceived(this);
            }
        }

        public override void GetRawData(System.IO.BinaryWriter bw)
        {
            //
            //in plc:
            //BSN - 4 Byte
            //Derivative Code - 2 Byte
            //Color - 2 Byte
            //Track - 1 Byte
            //Roof - 1 Byte
            //Hood - 1 Byte
            //Spare - 1 Byte
            //Spare - 1 Byte
            //Spare - 1 Byte
            //Spare - 1 Byte
            //
            bw.Write((byte)(Erpdataset.BSN >> 24));
            bw.Write((byte)(Erpdataset.BSN >> 16));
            bw.Write((byte)(Erpdataset.BSN >> 8));
            bw.Write((byte)Erpdataset.BSN);
            
            bw.Write((byte)(Erpdataset.DerivativeCode >> 8));
            bw.Write((byte)(Erpdataset.DerivativeCode));
            
            bw.Write((byte)(Erpdataset.Colour >> 8));
            bw.Write((byte)(Erpdataset.Colour));

            bw.Write((byte)Erpdataset.Track);

            bw.Write((byte)Erpdataset.Roof);

            bw.Write((byte)Erpdataset.HoD);

            //as spare 2xBytes!
            bw.Write((byte)(RequestLocalnID >> 8));
            bw.Write((byte)(RequestLocalnID));

            //additional foreign skid as spare 2xBytes
            bw.Write((byte)(Erpdataset.SkidID >> 8));
            bw.Write((byte)Erpdataset.SkidID);
        }
    }
}
