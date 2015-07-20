using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common.datamodels
{
    public class DM_LifePacket : DataModel
    {
        byte[] data;
        public DM_LifePacket()
            : base(4, 254)
        {
            data = new byte[4];
            data[0] = 255;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
        }
        public DM_LifePacket(short size, byte type, System.IO.BinaryReader reader)
            : base(size, type)
        {
            data = reader.ReadBytes(4);
        }
        public override void GetRawData(System.IO.BinaryWriter br)
        {
            br.Write(data);
        }
    }
}
