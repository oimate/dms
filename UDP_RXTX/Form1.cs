using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using dmspl.common;

namespace UDP_RXTX
{
    public partial class Form1 : Form
    {
        dmspl.common.log.IDataLog LogObj;
        
        UDPComm ms;
        public Form1(int port)
        {
            InitializeComponent();
        //   LogObj
            ms = new UDPComm(port, LogObj);
            ms.ReferencjaDoFunkcjiWyswietlajacejText = SetText;
            ms.StatusChanged += StatChanged;
            ms.DataRecv += DataRecv;

        }
        void SetText(string dane)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.SetTextDel(SetText), dane);
        }
        private void Tbtext_receive_TextChanged(object sender, EventArgs e)
        {

        }

        void StatChanged(object sender, EventArgs aa)
        {
            UDPComm com = (UDPComm)sender;
            if (com.ActState == UDPComm.Status.Connected)
            {
            }
        }

        void DataRecv(object sender, string str)
        {
        }
    }

}
