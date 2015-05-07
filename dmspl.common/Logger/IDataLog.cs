using System;
using System.Collections.Generic;
using System.Text;

namespace dmspl.common.log
{
    public interface IDataLog
    {
        Module EnabledModules { get; set; }
        EvType EnabledEventType { get; set; }
        void SetLevel(Module mod, Level lv);

        bool IsStarted { get; }
        void Start();
        void AddEvent(DateTime dt, Module mod, EvType typ, Level lv, object data);
        void Close();
        bool IsEnablesEvent(Module mod, EvType typ, Level lv);
    }

    static public class DataLog
    {
        public static IDataLog Default { get; private set; }
        public static void SetDefautLog(IDataLog log)
        {
            Default = log;
        }

        static public void Log(Module mod, EvType type, Level lv, object data)
        {
            Log(DateTime.Now, mod, type, lv, data);
        }
        static public void Log(DateTime dtime, Module mod, EvType type, Level lv, object data)
        {
            if (Default != null)
                Default.AddEvent(dtime, mod, type, lv, data);
        }
    }

    [Flags]
    public enum Module : int
    {
        None = 0,
        Appl = 1,
        RXTXComm = 1 << 1,
        DataBase = 1 << 2,
        All = 0x7FFFFFFF,
    }

    [Flags]
    public enum EvType : int
    {
        None = 0,
        Error = 1,
        Warning = 1 << 1,
        Info = 1 << 2,
        All = 0x7FFFFFFF,
    }

    public enum Level : int
    {
        None = 0,
        Main = 1,
        Primary = 2,
        Details = 3,
        Debug = 4,
    }
}
