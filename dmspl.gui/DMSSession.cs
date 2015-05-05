using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Security;
using System.Threading;

namespace dmspl
{
    class DMSSession
    {
        string usr;
        string pwd;
        string srv;
        string dbn;
        SqlConnection connection;
        public DMSSessionStatus Status { get { return sscea.Status; } set { sscea.Status = value; OnSessionStatusChanged(sscea); } }
        SessionStatusChangedEventArgs sscea;

        public DMSSession()
        {
            usr = "root";
            pwd = "some";
            srv = @"192.168.232.128\durr_systems";
            dbn = "EMOS_WEB";
            string cs = "data source=" + srv + ";Persist Security Info=false;database=" + dbn + ";user id=" + usr + ";password=" + pwd + ";Connection Timeout = 15";
            connection = new SqlConnection(cs);
            sscea = new SessionStatusChangedEventArgs();
        }

        public void Connect()
        {
            Status = DMSSessionStatus.connecting;
            try
            {
                connection.Open();
                Status = DMSSessionStatus.connected;
            }
            catch (Exception)
            {
                Status = DMSSessionStatus.connectionproblem;
                return;
            }
        }

        internal void Disconnect()
        {
            Status = DMSSessionStatus.disconnecting;
            try
            {
                connection.Close();
                Status = DMSSessionStatus.disconnected;
            }
            catch (Exception)
            {
                Status = DMSSessionStatus.disconnectionproblem;
                return;
            }
        }

        public virtual void OnSessionStatusChanged(SessionStatusChangedEventArgs e)
        {
            EventHandler<SessionStatusChangedEventArgs> handler = SessionStatusChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<SessionStatusChangedEventArgs> SessionStatusChanged;
    }

    public class SessionStatusChangedEventArgs : EventArgs
    {
        public DMSSessionStatus Status { get; set; }
    }
}
