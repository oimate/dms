using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace dmspl.common
{
    static public class RegAuth
    {
        const string key = @"SOFTWARE\EMOS.WEB\VisuStudio";

        public static AuthData GetRegData()
        {
            AuthData ad = null;
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(key))
            {
                if (rk == null) return DefaultAuthData();
                ad = new AuthData();
                int activesql = int.Parse((string)rk.GetValue("SQL"));

                string host = (string)rk.GetValue(string.Format("SQL{0:D2}_Hostname", activesql));
                if (string.IsNullOrWhiteSpace(host)) return DefaultAuthData();

                string instance = (string)rk.GetValue(string.Format("SQL{0:D2}_Instance", activesql));
                ad.Server = (string.IsNullOrWhiteSpace(instance)) ? host : string.Format("{0}\\{1}", host, instance);

                string user = (string)rk.GetValue(string.Format("SQL{0:D2}_Username", activesql));
                if (string.IsNullOrWhiteSpace(user)) return DefaultAuthData();
                ad.User = user;

                string pwd = (string)rk.GetValue(string.Format("SQL{0:D2}_Password", activesql));
                pwd = Obfuscation.Decode(user, pwd);
                if (string.IsNullOrWhiteSpace(pwd)) return DefaultAuthData();
                ad.Password = pwd;

                string database = (string)rk.GetValue(string.Format("SQL{0:D2}_Database", activesql));
                if (string.IsNullOrWhiteSpace(database)) return DefaultAuthData();
                ad.Database = database;
            }
            return ad;
        }

        private static AuthData DefaultAuthData()
        {
            AuthData ad = new AuthData();
            ad.Server = @"dbmachine\durr_systems";
            ad.User = "user";
            ad.Password = "user";
            ad.Database = "emos_web";
            return ad;
        }
    }

    public class AuthData
    {
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public string ConnectionString
        {
            get
            {
                return string.Format("data source={0};Persist Security Info=false;database={1};user id={2};password={3};Connection Timeout = 5"
                    , Server
                    , Database
                    , User
                    , Password);
            }
        }

        public AuthData()
        { }

        public AuthData(string s, string u, string p, string d)
        {
            Server = s;
            User = u;
            Password = p;
            Database = d;
        }

        public override string ToString()
        {
            return string.Format("Server:{0}, User:{1}, Pwd:{2}, Database:{3}"
                , Server
                , User
                , Password
                , Database);
        }
    }
}
