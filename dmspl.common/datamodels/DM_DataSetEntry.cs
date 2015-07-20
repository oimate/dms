using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmspl.common.BigEndianExtension;

namespace dmspl.common.datamodels
{
    public class DM_DataSetEntry : DataModel
    {
        public ErpDataset Erpdataset { get; set; }

        public DM_DataSetEntry(short size, byte type, System.IO.BinaryReader br)
            : base(size, type)
        {
            Erpdataset = new ErpDataset();
            //
            //order as in plc:
            //

            //BSN - 4 Byte
            Erpdataset.BSN = br.ReadInt32().FromBigEndian();

            //Derivative Code - 2 Byte
            Erpdataset.DerivativeCode = br.ReadInt16().FromBigEndian();

            //Color - 2 Byte
            Erpdataset.Colour = br.ReadInt16().FromBigEndian();

            //Track - 1 Byte
            Erpdataset.Track = br.ReadByte();

            //Roof - 1 Byte
            Erpdataset.Roof = br.ReadByte();

            //Hood - 1 Byte
            Erpdataset.Hood = br.ReadByte();

            //siemens gap or fill between byte and following int
            br.ReadByte();

            //SkidNr - 2 Byte
            Erpdataset.SkidID = br.ReadInt16().FromBigEndian();

            //Spare - 2 Byte
            Erpdataset.Spare = br.ReadInt16().FromBigEndian();

            //Datetime from server
            Erpdataset.Timestamp = DateTime.Now;
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
            bw.Write(((short)Erpdataset.SkidID).ToBigEndian());
            bw.Write(((short)Erpdataset.Spare).ToBigEndian());
        }
    }
}
