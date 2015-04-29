using dmspl.common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using dmspl.common;
using dmspl.common.log;
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

        public event EventHandler StatusChanged;
        public event EventHandler<string> DataRecv;

        public Status ActState
        {
            get { return pActState; }
            private set { SetServiceStatus(value); }
        }
        IDataLog LogObj;
        public DelegateCollection.SetTextDel ReferencjaDoFunkcjiWyswietlajacejText { get; set; }
        public UDPComm(int port, IDataLog log)
        {
            LogObj = log;
            if (socket != null)
                return;
            socket = new UdpClient(port);
            RemoteIP = new IPEndPoint(IPAddress.Any, 0);
            socket.BeginReceive(ReceiveCallback, socket);
            MainLoopStatus = true;
            TimerSF = new System.Threading.Timer(TimerCallback, null, 0, Properties.Settings.Default.TimeSF);
            SetServiceStatus(Status.Enabled);
            //   Program.Log.AddEvent(DateTime.Now, Module.Appl, EvType.Info, Level.Main, "Aplication PLCSim Started");
        }
        public UDPComm(int port)
            : this(port, null)
        {
        }

        private void Log(EvType type, Level lv, object data)
        {
            if (LogObj != null)
                LogObj.AddEvent(DateTime.Now, Module.RXTXComm, type, lv, data);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient sock = (UdpClient)ar.AsyncState;
            byte[] dane = null;
            try
            {
                dane = sock.EndReceive(ar, ref RemoteIP);
                if (DataRecv != null)
                    DataRecv(this, string.Format("{0}", RemoteIP));
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            JakoBufor(dane);

            if (MainLoopStatus)
                sock.BeginReceive(ReceiveCallback, sock);
        }

        private void JakoBufor(byte[] dane)
        {
            List<byte[]> cccc = FrameSpitter.SplitRD(dane);
            intramk(cccc);
        }

        private void intramk(List<byte[]> cccc)
        {
            foreach (byte[] buffer in cccc)
            {
                DataModel RecievedDataModel = DataModel.GetModel(buffer, OnDataReceived);
                if (RecievedDataModel == null) continue;
                DataStorage.ProcessModel(RecievedDataModel);
            }
        }

        private void OnDataReceived(DataModel dm)
        {
            try
            {
                socket.Send(dm.GetRawData(), dm.Size, RemoteIP);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void onNewDataRecieved(string s)
        {
            if (ReferencjaDoFunkcjiWyswietlajacejText != null)
            {
                ReferencjaDoFunkcjiWyswietlajacejText(s);
            }
        }

        private void TimerCallback(Object state)
        {
            if (socket != null && RemoteIP.Address != IPAddress.Any)
            {
                byte[] sendbyte = new byte[] { 255, 255, 255, 255 };
                SendDataFrame(CreateHeader(sendbyte, 254));
            }
        }
        private void SendDataFrame(byte[] tablica)
        {
            if (socket != null && RemoteIP.Address != IPAddress.Any)
            {
                socket.Send(tablica, tablica.Length, RemoteIP);
            }
        }
        private class FrameSpitter
        {
            internal static List<byte[]> SplitRD(byte[] wholerecdata)
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
                    list.Add(subst(wholerecdata, firstindex, size));
                }
                return list;
            }

            private static byte[] subst(byte[] wholerecdata, int p1, int p2)
            {
                byte[] ret = new byte[p2];
                for (int i = p1; i < p2; i++)
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
                    //     Program.Log.AddEvent(DateTime.Now, Module.Appl, EvType.Info, Level.Main, "Service from PLCSim Enabled ");
                    break;
                case Status.Disabled:
                    //     Program.Log.AddEvent(DateTime.Now, Module.Appl, EvType.Info, Level.Main, "Service from PLCSim Disabled ");
                    break;
                case Status.Connected:
                    if (!connectionstate)
                    {
                        //         Program.Log.AddEvent(DateTime.Now, Module.RXTXComm, EvType.Warning, Level.Main, "Partner for PLCSim Connected ");
                    }
                    connectionstate = true;
                    break;
                case Status.Disconnected:
                    if (connectionstate)
                    {
                        //        Program.Log.AddEvent(DateTime.Now, Module.RXTXComm, EvType.Warning, Level.Main, "Partner for PLCSim Disconnected ");
                    }
                    connectionstate = false;
                    break;
                case Status.WithoutCommFrame:
                    break;


            }
            if ((newStatus != oldStatus) && (StatusChanged != null))
                StatusChanged(this, null);
        }

        public void Dispose()
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


        static public byte[] CreateHeader(byte[] tablica, int ID)
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

        static string ConverByteArrayToStrong(byte[] data)
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

}
