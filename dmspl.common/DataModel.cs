using dmspl.common.datamodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmspl.common.log;

namespace dmspl.common
{
    public class DataLifePacket : DataModel
    {
        byte[] data;
        public DataLifePacket() : base(8,254)
        {
            data = new byte[4];
            data[0] = 255;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
        }
        public DataLifePacket(short size, byte type,System.IO.BinaryReader reader) : base(size,type)
        {
            data = new byte[4];
        }
        public override void GetRawData(System.IO.BinaryWriter br)
        {
            br.Write(data);
        }
    }

    public class DataHeader
    {
        public short datasize;
        public byte datatype;
        public byte Crc;
        public DataModel Data;
        public DataHeader(System.IO.BinaryReader br)
        {
            datasize = System.Net.IPAddress.NetworkToHostOrder(br.ReadInt16());
            datatype = br.ReadByte();
        }

        public DataHeader(DataModel dm)
        {
            Data = dm;
            if (dm is MFPDataModel)
                datatype = 9;
            else if (dm is DataLifePacket)
                datatype = 254;
            else if (dm is DataSetReqDataModel)
                datatype = 4;
            datasize = (short)dm.Size;
        }

        public void WriteHeader(System.IO.BinaryWriter br)
        {
            br.Write((byte)(datasize>>8));
            br.Write((byte)(datasize ));
            br.Write((byte)(datatype));
            Data.GetRawData(br);
            Crc = ((byte)~(datatype));
            br.Write(Crc);
        }

        private void ModelFactory(System.IO.BinaryReader br)
        {
            switch (datatype)
            {
                case 9: //mfp type
                    Data = new MFPDataModel(datasize, datatype, br);
                    break;
                case 4: //request type
                    Data = new DataSetReqDataModel(datasize, datatype, br);
                    break;
                case 254:
                    Data = new DataLifePacket(datasize, datatype, br);
                    break;
                default:
                    {
                        byte[] data = br.ReadBytes(datasize - 4);
                        DataLog.Log(Module.Appl, EvType.Error, Level.Debug, "Unsupported Frame ID: " + datatype + "  Data:" + ConverByteArrayToStrong(data));
                        //                    System.Diagnostics.Debug.WriteLine(string.Format("Unsupported type: {0}", type));
                        Data = null;
                    }
                    break;
            }
        }

        private static object ConverByteArrayToStrong(byte[] data)
        {
            string s = string.Empty;

            foreach (var item in data)
            {
                s += item.ToString();
            }
            return s;
        }

        public void ReadCrc(System.IO.BinaryReader br)
        {
        }

        public static DataModel GetModel(System.IO.BinaryReader br)
        {            
            DataHeader header = new DataHeader(br);
            DataModel retModel = header.Data;
            header.ReadCrc(br);
            return retModel;
        }

    }

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

        public abstract void GetRawData(System.IO.BinaryWriter br);
        public byte[] GetRawData()
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(new byte[Size]))
            {
                using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms))
                {
                    bw.Write((byte)(Size >> 8));
                    bw.Write((byte)Size);
                    bw.Write(Type);
                    GetRawData(bw);
                    bw.Write(CRC);
                    return ms.ToArray();
                }
            }
            
        }
        /*
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
                        case 254:
                            retModel = new DataLifePacket(size, type, reader);
                            break;
                        default:
                            DataLog.Log(Module.Appl, EvType.Error, Level.Debug, "Unsupported Frame ID: " + type + "  Data:" + ConverByteArrayToStrong(buffer));
                            //                    System.Diagnostics.Debug.WriteLine(string.Format("Unsupported type: {0}", type));
                            return null;
                    }
                    retModel.CRC = reader.ReadByte();
                }
            }
            return retModel;
        }
        */
        
    }
}
