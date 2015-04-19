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
        System.Threading.Timer d;
        bool petla;

        public PLCSimUDP()
        {
            InitializeComponent();

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
        }

  
    }
}
