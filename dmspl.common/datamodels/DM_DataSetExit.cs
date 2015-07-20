using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmspl.common.BigEndianExtension;

namespace dmspl.common.datamodels
{
    public class DM_DataSetExit : DataModel
    {
        public int Skid { get; set; }

        public DM_DataSetExit(short size, byte type, System.IO.BinaryReader br)
            : base(size, type)
        {
            Skid = br.ReadInt16().FromBigEndian();
        }

        public override void GetRawData(System.IO.BinaryWriter bw)
        {
            bw.Write(Skid.ToBigEndian());
        }
    }
}
