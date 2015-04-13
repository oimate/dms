using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDP_RXTX
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int port=9001;
            foreach (var item in args)
            {
                if (item.Contains("-port:") && (item.Length > 6))
                {                   
                     port = int.Parse(item.Substring(6, item.Length - 6));
                }

            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(port));
        }
    }
}
