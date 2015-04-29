using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace dmspl.common
{
    class Logger
    {

 
       static void writeLogfile(string text, string info)
        {
            if (!(System.IO.Directory.Exists(@"Debug")))
            {
                Directory.CreateDirectory(@"Debug");
            }

            int i = 1;
            if (File.Exists(@"Debug\FrameLog.txt"))
            {
                i = 0;
                var lines = File.ReadAllLines(@"Debug\FrameLog.txt");
                var q = from s in lines
                        where !string.IsNullOrEmpty(s)
                        select s;
                i = q.Count() + 1;
            }
            text = i + ":" + "\t" + DateTime.Now.ToString() + i + ":" + "\t" + text + "\t" + info + Environment.NewLine;
            File.AppendAllText(@"Debug\FrameLog.txt", text);
        }

        static string ConverByteArrayToStrong(byte[] data)
        {
            string s = string.Empty;

            foreach (var item in data)
            {
                s += item.ToString();

            }
            return s;
        }
    }

    [Flags]
    public enum LModule : int
    {
        None = 0,
        Appl = 1,
        RXTXComm = 1 << 1,
        DataBase = 1 << 2,
        All = 0x7FFFFFFF,
    }

    [Flags]
    public enum LEvType : int
    {
        None = 0,
        Error = 1,
        Warning = 1 << 1,
        Info = 1 << 2,
        All = 0x7FFFFFFF,
    }

    public enum LLevel : int
    {
        None = 0,
        Main = 1,
        Primary = 2,
        Detailes = 3,
        Debug = 4,
    }
}
