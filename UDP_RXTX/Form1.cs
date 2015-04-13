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
        UDPComm ms;
        public Form1(int port)
        {
            InitializeComponent();
            ms = new UDPComm(port);
            ms.ReferencjaDoFunkcjiWyswietlajacejText = SetText;

        }
        void SetText(string dane)
        {
            if (InvokeRequired)
                BeginInvoke(new DelegateCollection.SetTextDel(SetText), dane);
        }
        private void Tbtext_receive_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
