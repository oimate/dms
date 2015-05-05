using System;
using System.Collections.Generic;
using System.Text;

namespace dmspl.common.log
{
    public abstract class FileLog : IDataLog
    {
        IDictionary<Module, Level> LogLevels;
        IDictionary<Module, System.IO.StreamWriter> LogSterams;
        protected System.IO.StreamWriter DataStream;
        Encoding Encod;
        public bool IsStarted
        {
            get
            {
                return (LogSterams != null);
            }
        }

        public Module EnabledModules { get; set; }
        public EvType EnabledEventType { get; set; }


        public void SetLevel(Module mod, Level lv)
        {
            LogLevels[mod] = lv;
        }

        string fileName;
        public FileLog(string filename, Encoding encoding)
        {
            LogLevels = new Dictionary<Module, Level>();
            LogLevels[Module.Appl] = Level.None;
            LogLevels[Module.DataBase] = Level.None;
            LogLevels[Module.RXTXComm] = Level.None;
            EnabledModules = Module.None;
            EnabledEventType = EvType.None;
            fileName = filename;
            Encod = encoding;
        }
        virtual public void Start()
        {
            if (IsStarted)
                throw new InvalidOperationException("Log is already started");
          //  DataStream = new System.IO.StreamWriter(fileName, false, Encod);
          DataStream =    new System.IO.StreamWriter(string.Format("{0}{1}.html", System.IO.Path.GetDirectoryName(fileName), "logger"));
            LogSterams = new Dictionary<Module, System.IO.StreamWriter>();
           // LogSterams.Add(Module.Appl, new System.IO.StreamWriter(string.Format("{0}{1}_{2:yymmddhhnnss}.html", System.IO.Path.GetDirectoryName(fileName), Module.Appl, DateTime.Now)));
        //    LogSterams.Add(Module.Appl, new System.IO.StreamWriter(string.Format("{0}{1}.html", System.IO.Path.GetDirectoryName(fileName), Module.Appl)));
        //    LogSterams.Add(Module.DataBase, new System.IO.StreamWriter(string.Format("{0}{1}.html", System.IO.Path.GetDirectoryName(fileName), Module.DataBase)));
        //    LogSterams.Add(Module.RXTXComm, new System.IO.StreamWriter(string.Format("{0}{1}.html", System.IO.Path.GetDirectoryName(fileName), Module.RXTXComm)));
        }
        virtual public void Close()
        {
            if (IsStarted)
            {
                DataStream.Close();
         //       LogSterams[Module.Appl].Close();
         //       LogSterams[Module.DataBase].Close();
         //       LogSterams[Module.RXTXComm].Close();
                LogSterams = null;
            }
        }
        public void AddEvent(DateTime dt, Module mod, EvType typ, Level lv, object data)
        {
            if (!IsEnablesEvent(mod, typ, lv))
                return;
            string str = GetEventText(dt, mod, typ, lv, data);
      //      Write(str,mod);
            WriteMain(str);
        }

        protected abstract string GetEventText(DateTime dt, Module mod, EvType typ, Level lv, object data);


        protected void Write(string data, Module mod)
        {
            DataStream.Write(data);
            DataStream.Flush();
            //if (mod == Module.All)
            //    WriteMain(data);
            //else
            //{
            //    LogSterams[mod].Write(data);
            //    LogSterams[mod].Flush();
            //}
        }
        protected void WriteMain(string data)
        {
            DataStream.Write(data);
            DataStream.Flush();
        }


        public bool IsEnablesEvent(Module mod, EvType typ, Level lv)
        {
            if (!LogLevels.ContainsKey(mod))
                return false;
            return (((EnabledModules & mod) != Module.None) && ((EnabledEventType & typ) != EvType.None) && (LogLevels[mod] >= lv));
        }
    }
}
