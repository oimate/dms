using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmspl.common.BigEndianExtension;

namespace dmspl.common.datamodels
{
    public class DataSetReqDataModelByBSN : DataModel
    {
        public int RequestBSN { get; set; }
        public int RequestLocalnID { get; set; }
        public string ResponseDataSet { get; set; }
        public byte[] Dataset { get; set; }
        public ErpDataset Erpdataset { get; set; }

        public DataSetReqDataModelByBSN(short size, byte type, System.IO.BinaryReader br)
            : base(size, type)
        {
            RequestBSN = System.Net.IPAddress.NetworkToHostOrder(br.ReadInt32());
            RequestLocalnID = System.Net.IPAddress.NetworkToHostOrder(br.ReadInt16());
            //DataSetReceived = dataSetReceived;
        }

        public void ResponseReady(ErpDataset dataset)
        {
            //if (DataSetReceived != null)
            //{
            //    if (dataset == null)
            //    {
            //        DataSetReceived(null);
            //        return;
            //    }
            //    Size = 15;
            //    this.Erpdataset = dataset;
            //    DataSetReceived(this);
            //}
        }

        public override void GetRawData(System.IO.BinaryWriter bw)
        {
            //
            //order as in plc:
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

            bw.Write(Erpdataset.BSN.ToBigEndian());
            bw.Write(((short)Erpdataset.DerivativeCode).ToBigEndian());
            bw.Write(((short)Erpdataset.Colour).ToBigEndian());
            bw.Write((byte)Erpdataset.Track);
            bw.Write((byte)Erpdataset.Roof);
            bw.Write((byte)Erpdataset.Hood);
            bw.Write(((short)RequestLocalnID).ToBigEndian());
            bw.Write(((short)Erpdataset.SkidID).ToBigEndian());

        }
    }
}
