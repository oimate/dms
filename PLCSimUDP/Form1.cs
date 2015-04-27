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
using System.Diagnostics;
using dmspl.common;
namespace PLCSimUDP
{
    public partial class PLCSimUDP : Form
    {
        UdpClient socket;
        IPEndPoint RemoteIP;
        Statistics Stat = new Statistics();
        System.Threading.Timer TimerSF;
        System.Threading.Timer TimerMFPFrame;
        object lockObj;
        bool petla;
        public int[] MFP_Skid = new int[150];
        public int[] Vin_Req = new int[2];
        public int[] MPP_Update = new int[8];

        Status ActState;

        //   MFP_Skid[2] = 22;

        public PLCSimUDP()
        {
            ActState = Status.Disconnected;
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
            TimerMFPFrame = new System.Threading.Timer(TimeUpdateMFPSkid, null, 0, Convert.ToInt16(tbTimerMFP.Text));
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
            if (ActState == Status.Connected)
            {
                SendDataFrame(CreateDataFrame(MFP_Skid, 9));
            }
        }

        static public byte[] CreateDataFrame(int[] tablica, int ID)
        {

            byte[] sendbyte = new byte[tablica.Length * 2];
            for (int i = 0; i < tablica.Length; i++)
            {
                sendbyte[i * 2] = (byte)(tablica[i] >> 8);
                sendbyte[(i * 2) + 1] = (byte)tablica[i];
            }
            byte[] sendFrame = new byte[sendbyte.Length + 4];
            for (int i = 0; i < sendbyte.Length; i++)
            {
                sendFrame[i + 3] = sendbyte[i];
            }
            sendFrame[0] = (byte)(sendFrame.Length >> 8);
            sendFrame[1] = (byte)(sendFrame.Length);
            sendFrame[2] = (byte)ID;
            sendFrame[sendFrame.Length - 1] = (byte)~sendFrame[2];

            return sendFrame;
        }


        public void SendDataFrame(byte[] tablica)
        {
            if (socket != null)
            {
                lock (lockObj)
                {
                    socket.Send(tablica, tablica.Length, Convert.ToString(tRemoteIP.Text), Convert.ToInt32(tRemotePort.Text));
                    Stat.AddValues(0, tablica.Length, 0, 0);
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
            TimerMFPFrame.Dispose();
            TimerMFPFrame = null;
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

                JakoBufor(dane);

            }
            catch (SocketException)
            {
                ServiceStatus(Status.Disconnected);
                //    System.Threading.Thread.Sleep(500);
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            if (petla && sock != null)
                try
                {
                    sock.BeginReceive(ReceiveCallback, sock);
                }
                catch (SocketException)
                {
                    ServiceStatus(Status.Disconnected);
                    //    System.Threading.Thread.Sleep(500);
                }
                catch (ObjectDisposedException)
                {
                }
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
                    try
                    {
                        //     byte[] sendbyte = new byte[] { 0, 8, 254, 255, 255, 255, 255, 1 };
                        int[] sendData = new int[] { -1, -1 };
                        SendDataFrame(CreateDataFrame(sendData, 254));
                        if (Stat.ConnReqCnt > 1)
                        {
                            ServiceStatus(Status.Disconnected);
                        }
                        Stat.AddValues(0, 0, 1, 1);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(string.Format("Nieznany blad na timerze: {0}", ex));
                    }
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
            ActState = activestatus;
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
                    tbTimerMFP.Enabled = false;
                    bMFPUpdate.Enabled = true;
                    bReqData.Enabled = true;
                    bUpdateMfp.Enabled = true;
                    bReqData1.Enabled = true;
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
                    bReqData.Enabled = false;
                    BSConn.BackColor = System.Drawing.Color.Gray;
                    tLocalIP.Text = "0.0.0.0";
                    tbTimerMFP.Enabled = true;
                    bMFPUpdate.Enabled = false;
                    bUpdateMfp.Enabled = false;
                    bReqData1.Enabled = false;
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
            Properties.Settings.Default.tbTimerMFP = Convert.ToInt32(tbTimerMFP.Text);
            Properties.Settings.Default.Save();
        }

        private void ReadConfig()
        {
            tLocalPort.Text = Properties.Settings.Default.LocalPort.ToString();
            tLocalIP.Text = Properties.Settings.Default.LocalIP;
            tRemotePort.Text = Properties.Settings.Default.RemPort.ToString();
            tRemoteIP.Text = Properties.Settings.Default.RemIP;
            tbTimerMFP.Text = Properties.Settings.Default.tbTimerMFP.ToString();
        }

        private void tSkidIndex_Leave(object sender, EventArgs e)
        {
            tSkidValue.Text = Convert.ToString(MFP_Skid[Convert.ToInt16(tSkidIndex.Text)]);
        }

        private void bMFPUpdate_Click(object sender, EventArgs e)
        {
            MFP_Skid[Convert.ToInt16(tSkidIndex.Text)] = Convert.ToInt16(tSkidValue.Text);
        }

        private void CheckInput_Dig(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int a = Convert.ToInt16(tb.Tag);
            if (e.KeyChar != '\b')
            {
                if (tb.TextLength >= a || !(Char.IsDigit(e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
        private void CheckInput_Color(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int a = Convert.ToInt16(tb.Tag);
            if (tb.TextLength >= a)
            {
                tb.BackColor = Color.LawnGreen;
            }
            else
            {
                tb.BackColor = Color.White;
            }
        }
        private void tbReqVin_TextChanged(object sender, EventArgs e)
        {

        }

        private void bReqData_Click(object sender, EventArgs e)
        {
            if ((tbReqVin.Text.Length == 6) & (tReqSkid.Text.Length == 4))
            {
                Vin_Req[0] = Convert.ToInt32(tbReqVin.Text);
                Vin_Req[1] = Convert.ToInt32(tReqSkid.Text);

                byte[] sendbyte = new byte[6];


                sendbyte[0] = (byte)(Vin_Req[0] >> 24);
                sendbyte[1] = (byte)(Vin_Req[0] >> 16);
                sendbyte[2] = (byte)(Vin_Req[0] >> 8);
                sendbyte[3] = (byte)Vin_Req[0];
                sendbyte[4] = (byte)(Vin_Req[1] >> 8);
                sendbyte[5] = (byte)Vin_Req[1];

                byte[] sendFrame = new byte[sendbyte.Length + 4];
                for (int i = 0; i < sendbyte.Length; i++)
                {
                    sendFrame[i + 3] = sendbyte[i];
                }
                sendFrame[0] = (byte)(sendFrame.Length >> 8);
                sendFrame[1] = (byte)(sendFrame.Length);
                sendFrame[2] = 4;
                sendFrame[sendFrame.Length - 1] = (byte)~sendFrame[2];


                SendDataFrame(sendFrame);
            }
        }

        private void bUpdateMfp_Click(object sender, EventArgs e)
        {
            if ((tMfp_Body.Text.Length == 6) && (tMfp_Code.Text.Length == 2) && (tMfp_Colour.Text.Length == 3) && (tMfp_Hod.Text.Length == 1) && (tMfp_Roof.Text.Length == 1) && (tMfp_skid.Text.Length == 4) && (tMfp_Spare.Text.Length == 2) && (tMfp_Track.Text.Length == 1))
            {
                MPP_Update[0] = Convert.ToInt32(tMfp_skid.Text);
                MPP_Update[1] = Convert.ToInt32(tMfp_Code.Text);
                MPP_Update[2] = Convert.ToInt32(tMfp_Code.Text);
                MPP_Update[3] = Convert.ToInt32(tMfp_Body.Text);
                MPP_Update[4] = Convert.ToInt32(tMfp_Track.Text);
                MPP_Update[5] = Convert.ToInt32(tMfp_Roof.Text);
                MPP_Update[6] = Convert.ToInt32(tMfp_Hod.Text);
                MPP_Update[7] = Convert.ToInt32(tMfp_Spare.Text);

                ErpDataset ErpDs = new ErpDataset() {

                    SkidID = Convert.ToInt32(tMfp_skid.Text),
                    DerivativeCode = Convert.ToInt32(tMfp_Code.Text),
                    Colour = Convert.ToInt32(tMfp_Code.Text),
                    BSN = Convert.ToInt32(tMfp_Body.Text),
                    Track =   Convert.ToInt32(tMfp_Track.Text),
                    Roof=  Convert.ToInt32(tMfp_Roof.Text),
                    HoD=  Convert.ToInt32(tMfp_Hod.Text),
                    Spare=  Convert.ToInt32(tMfp_Spare.Text),
                };
                
            }
            byte[] sendbyte = new byte[13];


            sendbyte[0] = (byte)(MPP_Update[0] >> 8);  // skid id
            sendbyte[1] = (byte)MPP_Update[0];
            sendbyte[2] = (byte)MPP_Update[1];  // derivate code
            sendbyte[3] = (byte)(MPP_Update[2] >> 8); //colour
            sendbyte[4] = (byte)MPP_Update[2];
            sendbyte[5] = (byte)(MPP_Update[3] >> 24); // BSN
            sendbyte[6] = (byte)(MPP_Update[3] >> 16);
            sendbyte[7] = (byte)(MPP_Update[3] >> 8);
            sendbyte[8] = (byte)MPP_Update[3];
            sendbyte[9] = (byte)MPP_Update[4];  // Tracek
            sendbyte[10] = (byte)MPP_Update[5];  //Roof
            sendbyte[11] = (byte)MPP_Update[6]; // HoD
            sendbyte[12] = (byte)MPP_Update[7];  //Spare



            byte[] sendFrame = new byte[sendbyte.Length + 4];
            for (int i = 0; i < sendbyte.Length; i++)
            {
                sendFrame[i + 3] = sendbyte[i];
            }
            sendFrame[0] = (byte)(sendFrame.Length >> 8);
            sendFrame[1] = (byte)(sendFrame.Length);
            sendFrame[2] = 6;   // update MFP
            sendFrame[sendFrame.Length - 1] = (byte)~sendFrame[2];


            SendDataFrame(sendFrame);
        }

        private void bReqData1_Click(object sender, EventArgs e)
        {
            if ((tbRegFskid.Text.Length == 4) & (tbRegLskid.Text.Length == 4))
            {
                Vin_Req[0] = Convert.ToInt32(tbRegFskid.Text);
                Vin_Req[1] = Convert.ToInt32(tbRegLskid.Text);

                byte[] sendbyte = new byte[4];


                sendbyte[0] = (byte)(Vin_Req[0] >> 8);
                sendbyte[1] = (byte)Vin_Req[0];
                sendbyte[2] = (byte)(Vin_Req[1] >> 8);
                sendbyte[3] = (byte)Vin_Req[1];

                byte[] sendFrame = new byte[sendbyte.Length + 4];
                for (int i = 0; i < sendbyte.Length; i++)
                {
                    sendFrame[i + 3] = sendbyte[i];
                }
                sendFrame[0] = (byte)(sendFrame.Length >> 8);
                sendFrame[1] = (byte)(sendFrame.Length);
                sendFrame[2] = 5;
                sendFrame[sendFrame.Length - 1] = (byte)~sendFrame[2];


                SendDataFrame(sendFrame);
            }
        }


    }

}
