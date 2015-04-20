using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace PLCSimUDP
{
    public partial class PLCSimUDP : Form
    {
        UdpClient socket;
        IPEndPoint RemoteIP;
        Statistics Stat = new Statistics();
        System.Threading.Timer TimerSF;
        System.Threading.Timer TimerMFP_Frame;
        object lockObj;
        bool petla;
        public int[] MFP_Skid = new int[100];


        public PLCSimUDP()
        {
            lockObj = new object();
            InitializeComponent();
            ReadConfig();

            Stat.OnChange += UpdateStats;

        }
        public void bStartService_Click(object sender, EventArgs e)
        {
            if (socket != null)
                return;
            socket = new UdpClient(Convert.ToInt32(tLocalPort.Text));
            RemoteIP = new IPEndPoint(IPAddress.Any, 0);
            socket.BeginReceive(ReceiveCallback, socket);
            petla = true;
            TimerSF = new System.Threading.Timer(TimerCallback, null, 0, 1000);
           TimerMFP_Frame = new System.Threading.Timer(TimeUpdateMFPSkid, null, 0, 2000);
            ServiceStatus(Status.Enabled);
            string HostName = Dns.GetHostName();
            var dnsAdresses = Dns.GetHostEntry(HostName).AddressList;
            for (int i = 0; i < dnsAdresses.Length; i++)
            {
                if (Convert.ToString(tRemoteIP.Text).Substring(0, 10) == Convert.ToString(dnsAdresses[i]).Substring(0, 10))
                {
                    tLocalIP.Text = Convert.ToString(dnsAdresses[i]);
                    return;
                }
                else 
                {
                    tLocalIP.Text = "Wrong Rem IP";
                }
            }
        }

        public void TimeUpdateMFPSkid(object state)
        {
            if (socket != null)
            {
                lock (lockObj)
                {
                    byte[] sendbyte = new byte[304];
                    //     MFP_Skid.Length

                    //   socket.Send(sendbyte, sendbyte.Length, Convert.ToString(tRemoteIP.Text), Convert.ToInt32(tRemotePort.Text));


                    //   Stat.AddValues(0, 300, 0, 0);
                }
            }
        }

        private void UpdateStats(object sender, EventArgs e)
        {
            ThText = sender.ToString();
        }

        private void bStopService_Click(object sender, EventArgs e)
        {
            if (socket == null)
                return;
            TimerSF.Dispose();
            TimerSF = null;
            petla = false;
            socket.Close();
            socket = null;
            ServiceStatus(Status.Disabled);
        }


        private void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient sock = (UdpClient)ar.AsyncState;
            byte[] dane = null;
            try
            {
                dane = sock.EndReceive(ar, ref RemoteIP);
                Stat.AddValues(dane.Length, 0, 0, 0);
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            JakoBufor(dane);

            if (petla)
                sock.BeginReceive(ReceiveCallback, sock);
        }

        public string ThText
        {
            get { return Text; }
            set
            {
                SetFormText(value);
            }
        }
        delegate void UptdTxt(string str);



        void SetFormText(string text)
        {
            if (InvokeRequired)
                this.BeginInvoke(new UptdTxt(SetFormText), text);
            else
                Text = text;

        }

        private void JakoBufor(byte[] dane)
        {
            List<byte[]> cccc = FrameSpitter.SplitRD(dane);
            intramk(cccc);
        }

        bool ArraysEqual(byte[] a, byte[] b)
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
                //        string  ramka = Convert.ToString( buffer);
                switch (buffer[2])
                {
                    case 254:
                        byte[] sendbyte = new byte[] { 0, 8, 254, 255, 255, 255, 255, 1 };
                        if (ArraysEqual(buffer, sendbyte))
                            Stat.ConnReqCnt = 0;
                        ServiceStatus(Status.Connected);
                        break;
                    case 8:

                        break;
                    case 9:

                        break;

                    default:
                        break;
                }

            }
        }

        private void onNewDataRecieved(string s)
        {

        }

        public void TimerCallback(Object state)
        {
            if (socket != null)
            {
                lock (lockObj)
                {
                    byte[] sendbyte = new byte[] { 0, 8, 254, 255, 255, 255, 255, 1 };
                    string bb = tRemoteIP.Text;
                    System.Diagnostics.Debug.WriteLine(bb);
                    socket.Send(sendbyte, sendbyte.Length, Convert.ToString(tRemoteIP.Text), Convert.ToInt32(tRemotePort.Text));

                    if (Stat.ConnReqCnt > 1)
                    {
                        ServiceStatus(Status.Disconnected);
                    }
                    Stat.AddValues(0, 0, 1, 1);
                }
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
        public void ServiceStatus(Status activestatus)
        {
            switch (activestatus)
            {
                case Status.Enabled:
                    bSService.BackColor = System.Drawing.Color.LimeGreen;
                    bStartService.Text = "Stop";
                    bStartService.Click -= bStartService_Click;
                    bStartService.Click += bStopService_Click;
                    tLocalPort.Enabled = false;
                    tRemoteIP.Enabled = false;
                    tRemotePort.Enabled = false;
                    Stat.Clear();
                    break;
                case Status.Disabled:
                    bSService.BackColor = System.Drawing.Color.Gray;
                    bStartService.Text = "Start";
                    bStartService.Click -= bStopService_Click;
                    bStartService.Click += bStartService_Click;
                    tLocalPort.Enabled = true;
                    tRemoteIP.Enabled = true;
                    tRemotePort.Enabled = true;
                    BSConn.BackColor = System.Drawing.Color.Gray;
                    tLocalIP.Text = "0.0.0.0";
                    break;
                case Status.Connected:
                    BSConn.BackColor = System.Drawing.Color.LimeGreen;
                    break;
                case Status.Disconnected:
                    BSConn.BackColor = System.Drawing.Color.Red;
                    break;
                case Status.WithoutCommFrame:
                    BSConn.BackColor = System.Drawing.Color.Gray;
                    break;


            }

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

        struct Statistics
        {
            public EventHandler OnChange;
            public int RxBytes;
            public int TxBytes;
            public int ConnFrame;
            public int ConnReqCnt;

            public void AddValues(int rx, int tx = 0, int conF = 0, int conRq = 0)
            {
                RxBytes += rx;
                TxBytes += tx;
                ConnFrame += conF;
                ConnReqCnt += conRq;
                if (OnChange != null)
                    OnChange(this, null);
            }

            public void Clear()
            {
                RxBytes = 0;
                TxBytes = 0;
                ConnFrame = 0;
                ConnReqCnt = 0;
                if (OnChange != null)
                    OnChange(this, null);
            }

            public override string ToString()
            {
                return string.Format("Odebrano: {0}[B] Wysłano: {1}[B] Connection Out Frame: {2}", RxBytes, TxBytes, ConnFrame);
            }
        }

        private void tRemoteIP_Enter(object sender, EventArgs e)
        {
            Properties.Settings.Default.LocalPort = Convert.ToInt32(tLocalPort.Text);
            Properties.Settings.Default.LocalIP = tLocalIP.Text;
            Properties.Settings.Default.RemPort = Convert.ToInt32(tRemotePort.Text);
            Properties.Settings.Default.RemIP = tRemoteIP.Text;
            Properties.Settings.Default.Save();
        }

        private void ReadConfig()
        {
            tLocalPort.Text = Properties.Settings.Default.LocalPort.ToString();
            tLocalIP.Text = Properties.Settings.Default.LocalIP;
            tRemotePort.Text = Properties.Settings.Default.RemPort.ToString();
            tRemoteIP.Text = Properties.Settings.Default.RemIP;
        }

        private void tSkidIndex_Leave(object sender, EventArgs e)
        {
            tSkidValue.Text = Convert.ToString(MFP_Skid[Convert.ToInt32(tSkidIndex.Text)]);
        }

        private void bMFPUpdate_Click(object sender, EventArgs e)
        {
            MFP_Skid[Convert.ToInt32(tSkidIndex.Text)] = Convert.ToInt32(tSkidValue.Text);
        }
    }

}
