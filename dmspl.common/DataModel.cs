using dmspl.common.datamodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common
{
    public abstract class DataModel
    {
        public int Size { get; set; }
        public byte Type { get; private set; }
        public byte CRC { get; private set; }
        public delegate void DataSetReceivedDelegate(DataModel dm);
        public DataSetReceivedDelegate DataSetReceived { get; set; }

        public DataModel(short size, byte type)
        {
            Size = size;
            Type = type;
        }

        public virtual byte[] GetRawData()
        {
            return null;
        }

        public static DataModel GetModel(byte[] buffer, DataSetReceivedDelegate dataSetReceived)
        {
            DataModel retModel;

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer))
            {
                using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
                {
                    short size = reader.ReadInt16();
                    size = System.Net.IPAddress.NetworkToHostOrder(size);
                    byte type = reader.ReadByte();
                    switch (type)
                    {
                        case 9: //mfp type
                            retModel = new MFPDataModel(size, type, reader);
                            break;
                        case 4: //request type
                            retModel = new DataSetReqDataModel(size, type, reader, dataSetReceived);
                            break;
                        default:
                            Log(EvType.Warning, Level.Debug, ConverByteArrayToStrong(buffer));  
                            System.Diagnostics.Debug.WriteLine(string.Format("Unsupported type: {0}", type));
                            return null;
                    }
                    retModel.CRC = reader.ReadByte();
                }
            }
            return retModel;
        }
        static string ConverByteArrayToString(byte[] data)    // convert array in byte to string array
        {
            string s = string.Empty;

            foreach (var item in data)
            {
                s += item.ToString();
            }
            return s;
        }
    }
}
