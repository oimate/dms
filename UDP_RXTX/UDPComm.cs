using dmspl.common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using dmspl.common;
using dmspl.common.log;
using dmspl.common.datamodels;
namespace UDP_RXTX
{
    public class UDPComm : IUDPComm, IDisposable
    {
        public IDataStorage DataStorage { get; set; }
        UdpClient socket;  // socfket for commmunications
        IPEndPoint RemoteIP;   // remmote ip object
        System.Threading.Timer TimerSF; // Timer for call to func for checking connections status 
        bool MainLoopStatus;
        public bool connectionstate = true;
        Status pActState;
        int ConnReqCnt = 0;
        public event EventHandler StatusChanged;
        public event EventHandler<string> DataRecv;
        object Dupa;

        public Status ActState
        {
            get { return pActState; }
            private set { SetServiceStatus(value); }
        }
        dmspl.common.log.IDataLog LogObj;
        public DelegateCollection.SetTextDel ReferencjaDoFunkcjiWyswietlajacejText { get; set; }
        public UDPComm(int port, dmspl.common.log.IDataLog log)
        {
            Dupa = new object();
            LogObj = log;
            if (socket != null)
                return;
            socket = new UdpClient(port);
            RemoteIP = new IPEndPoint(IPAddress.Any, 0);
            socket.BeginReceive(ReceiveCallback, socket);
            MainLoopStatus = true;
            TimerSF = new System.Threading.Timer(TimerCallback, null, 0, Properties.Settings.Default.TimeSF);
            SetServiceStatus(Status.Enabled);
            //           throw new UdpRxTxException("test");
            //   Program.Log.AddEvent(DateTime.Now, Module.Appl, EvType.Info, Level.Main, "Aplication PLCSim Started");
        }
        public UDPComm(int port)
            : this(port, null)
        {
        }

        private void Log(dmspl.common.log.EvType type, dmspl.common.log.Level lv, object data)
        {
            if (LogObj != null)
                LogObj.AddEvent(DateTime.Now, dmspl.common.log.Module.RXTXComm, type, lv, data);
        }

        #region Receive func
        private void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient sock = (UdpClient)ar.AsyncState;
            byte[] dane = null;
            try
            {
                dane = sock.EndReceive(ar, ref RemoteIP);  // przekazanie rem ip do innej fcji
                JakoBufor(dane);
                if (DataRecv != null)
                    DataRecv(this, string.Format("{0}", RemoteIP));
            }
            catch (SocketException)
            {
                SetServiceStatus(Status.Disconnected);
                //    System.Threading.Thread.Sleep(500);
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            if (MainLoopStatus && sock != null)
                try
                {
                    sock.BeginReceive(ReceiveCallback, sock);
                }
                catch (SocketException)
                {
                    SetServiceStatus(Status.Disconnected);
                    //    System.Threading.Thread.Sleep(500);
                }
                catch (ObjectDisposedException)
                {
                }
        }
        #endregion
        private void JakoBufor(byte[] dane)
        {
            List<byte[]> cccc = FrameSpitter.SplitRD(dane, LogObj);
            intramk(cccc);
        }

        private bool ArraysEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }

        private void intramk(List<byte[]> cccc)
        {
            foreach (byte[] buffer in cccc)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer))
                {
                    using (System.IO.BinaryReader br = new System.IO.BinaryReader(ms))
                    {
                        DataModel RecievedDataModel = DataHeader.GetModel(br);
                        //RecievedDataModel.DataSetReceived = (dm) => { SendData(dm); };
                        if (RecievedDataModel == null)
                            continue;
                        if (RecievedDataModel is DataLifePacket)
                            ProcessLifePacket((DataLifePacket)RecievedDataModel, buffer);
                        else
                        {
                            if (DataStorage != null)
                                DataStorage.ProcessModel(RecievedDataModel);
                        }
                    }
                }
            }
        }

        private void ProcessLifePacket(DataLifePacket pack, byte[] buffer)
        {
            byte[] comparebyte = new byte[] { 0, 8, 254, 255, 255, 255, 255, 1 };
            if (ArraysEqual(buffer, comparebyte))
                ConnReqCnt = 0;
            SetServiceStatus(Status.Connected);
        }

        //private void OnDataReceived(DataModel dm)
        //{
        //    try
        //    {
        //        socket.Send(dm.GetRawData(), dm.Size, RemoteIP);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //}

        private void onNewDataRecieved(string s)
        {
            if (ReferencjaDoFunkcjiWyswietlajacejText != null)
            {
                ReferencjaDoFunkcjiWyswietlajacejText(s);
            }
        }

        private void TimerCallback(Object state)
        {
            lock (Dupa)
            {
                try
                {
                    SendData(new DataLifePacket());
                    if (ConnReqCnt > 1)
                    {
                        SetServiceStatus(Status.Disconnected);
                    }
                    ConnReqCnt++;
                }

                catch (SocketException)
                {
                }
                catch (Exception)
                {
                }
            }
        }

        public void SendData(DataModel data)
        {
            byte[] buffer = new byte[data.Size + 4];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer))
            {
                using (System.IO.BinaryWriter br = new System.IO.BinaryWriter(ms))
                {
                    DataHeader dh = new dmspl.common.DataHeader(data);
                    dh.WriteHeader(br);
                    SendDataFrame(buffer);
                }
            }

        }

        private void SendDataFrame(byte[] tablica)
        {
            if (socket != null && RemoteIP.Address != IPAddress.Any)
            {
                socket.Send(tablica, tablica.Length, RemoteIP);
            }
            else
            {
                DataLog.Log(Module.RXTXComm, EvType.Error, Level.Main, "Socket for sending data not ready ");// socket not ready 
            }
        }

        private class FrameSpitter
        {
            internal static List<byte[]> SplitRD(byte[] wholerecdata, dmspl.common.log.IDataLog log = null)
            {
                List<byte[]> list = new List<byte[]>();

                List<int> pocz = new List<int>();
                int buffindex = 0;

                while (buffindex < wholerecdata.Length)
                {
                    pocz.Add(buffindex);
                    buffindex += (wholerecdata[buffindex] << 8) + (wholerecdata[buffindex + 1]);
                }
                foreach (int firstindex in pocz)
                {
                    int size = (wholerecdata[firstindex] << 8) + (wholerecdata[firstindex + 1]);
                    byte Type = (wholerecdata[firstindex + 2]);
                    if (size <= wholerecdata.Length)
                    {
                        byte CRC = (wholerecdata[firstindex + (size - 1)]);
                        if (CRC == (byte)(~(Type)))
                        {
                            list.Add(subst(wholerecdata, firstindex, size));
                        }
                        else
                        {

                            DataLog.Log(Module.RXTXComm, EvType.Error, Level.Debug, "Incorrect Frame CRC" + ConverByteArrayToString(wholerecdata));// Incorrect CRC

                            return list;
                        }
                    }
                    else
                    {
                        DataLog.Log(Module.RXTXComm, EvType.Error, Level.Debug, "Incorrect frame size:" + ConverByteArrayToString(wholerecdata));//Incorrect size
                        return list;
                    }
                }
                return list;
            }

            private static byte[] subst(byte[] wholerecdata, int p1, int p2)
            {
                byte[] ret = new byte[p2];
                for (int i = p1; i < (p2 + p1); i++)
                {
                    ret[i - p1] = wholerecdata[i];
                }
                return ret;
            }
        }


        private void SetServiceStatus(Status newStatus)
        {
            Status oldStatus = pActState;
            pActState = newStatus;
            switch (newStatus)
            {
                case Status.Enabled:
                    DataLog.Log(Module.RXTXComm, EvType.Info, Level.Main, "UDP Module  Enabled ");
                    break;
                case Status.Disabled:
                    DataLog.Log(Module.RXTXComm, EvType.Info, Level.Main, "UDP Module  Disabled ");
                    break;
                case Status.Connected:
                    if (!connectionstate)
                    {
                        DataLog.Log(Module.RXTXComm, EvType.Info, Level.Main, "PLC  with IP:" + RemoteIP.ToString() + " Connected ");
                    }
                    connectionstate = true;
                    break;
                case Status.Disconnected:
                    if (connectionstate)
                    {
                        DataLog.Log(Module.RXTXComm, EvType.Warning, Level.Main, "PLC with IP:" + RemoteIP.ToString() + "   Disconnected ");
                    }
                    connectionstate = false;
                    break;
                case Status.WithoutCommFrame:
                    break;


            }
            if ((newStatus != oldStatus) && (StatusChanged != null))
                StatusChanged(this, null);
        }

        public void Dispose()   //
        {
            DataRecv = null;
            if (socket != null)
                socket.Close();
            socket = null;
            TimerSF.Dispose();
            TimerSF = null;
            MainLoopStatus = false;
            ActState = Status.Disabled;
            StatusChanged = null;
        }


        static public byte[] CreateHeader(byte[] tablica, int ID)   // create header for preparing send frame 
        {
            byte[] sendFrame = new byte[tablica.Length + 4];
            for (int i = 0; i < tablica.Length; i++)
            {
                sendFrame[i + 3] = tablica[i];
            }
            sendFrame[0] = (byte)(sendFrame.Length >> 8);
            sendFrame[1] = (byte)(sendFrame.Length);
            sendFrame[2] = (byte)ID;
            sendFrame[sendFrame.Length - 1] = (byte)~sendFrame[2];
            return sendFrame;
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

        #region Enumy
        public enum Status
        {
            Enabled,
            Disabled,
            Connected,
            Disconnected,
            WithoutCommFrame,
        }
        #endregion
    }

    public class UdpRxTxException : Exception
    {
        public UdpRxTxException(string message)
            : base(message)
        {
        }
    }

    public class UdpRxTxConnectionException : Exception
    {
        public UdpRxTxConnectionException(string message)
            : base(message)
        {
        }
    }

    public class UdpRxTxDataException : Exception
    {
        public UdpRxTxDataException(string message)
            : base(message)
        {
        }
    }
}
