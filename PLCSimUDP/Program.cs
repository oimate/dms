using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using dmspl.common.log;

namespace PLCSimUDP
{
    static class Program
    {
         public static IDataLog Log;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            startlog();
            Application.Run(new PLCSimUDP());
        }

     static private void startlog()
        {
            Log = new HTMLLog("dupa.html");
            Log.EnabledModules = Module.All;
            Log.EnabledEventType = EvType.All;
            Log.SetLevel(Module.Appl, Level.Main);
            Log.SetLevel(Module.DataBase, Level.Primary);
            Log.SetLevel(Module.RXTXComm, Level.Debug);
          //  Log.SetLevel(Module.All, Level.Main);
            Log.Start();
        }
    }
}
