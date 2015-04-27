using dmspl.common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_RXTX
{
    public class UDPComm : IUDPComm
    {
        public IDataStorage DataStorage { get; set; }

        UdpClient socket;
        IPEndPoint RemoteIP;
        System.Threading.Timer d;

        bool petla;

        public DelegateCollection.SetTextDel ReferencjaDoFunkcjiWyswietlajacejText { get; set; }

        public UDPComm(int port)
        {
            if (socket != null)
                return;
            socket = new UdpClient(port);
            RemoteIP = new IPEndPoint(IPAddress.Any, 0);

            socket.BeginReceive(ReceiveCallback, socket);
            petla = true;
            d = new System.Threading.Timer(TimerCallback, null, 0, 1000);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient sock = (UdpClient)ar.AsyncState;
            byte[] dane = null;
            try
            {
                dane = sock.EndReceive(ar, ref RemoteIP);
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            JakoBufor(dane);

            if (petla)
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
                DataModel RecievedDataModel = DataModel.GetModel(buffer , OnDataReceived);
                if (RecievedDataModel == null) continue;
                DataStorage.ProcessModel(RecievedDataModel);
            }
        }

        private void OnDataReceived(DataModel dm)
        {
            throw new NotImplementedException();
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
                byte[] sendbyte = new byte[] { 0, 8, 254, 255, 255, 255, 255, 1 };
                socket.Send(sendbyte, sendbyte.Length, RemoteIP);
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
    }

}
