using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dmspl.common.datamodels
{
    public class DM_DataSetMfp : DataModel
    {
        public List<int> Mfps { get; private set; }

        public DM_DataSetMfp(short size, byte type, System.IO.BinaryReader br)
            : base(size, type)
        {
            Mfps = new List<int>();
            int count = Size / 2;
            for (int i = 0; i < count; i++)
            {
                Mfps.Add(System.Net.IPAddress.NetworkToHostOrder(br.ReadInt16()));
            }
        }
        public override void GetRawData(System.IO.BinaryWriter bw)
        { }
    }
}
