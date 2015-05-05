using System;
using System.Collections.Generic;
using System.Text;

namespace TagReader.Log
{  
    public abstract class FileLog : IDataLog
    {
        protected DatasetDecl Decl;
        protected System.IO.StreamWriter DataStream;
        Encoding Encod;
        public bool IsStarted
        {
            get
            {
                return (DataStream != null);
            }
        }
        string fileName;
        public FileLog(string filename, Encoding encoding)
        {
            fileName = filename;
            Encod = encoding;
        }
        virtual public void Start(DatasetDecl decl)
        {
            Decl = decl;
            if (IsStarted)
                throw new InvalidOperationException("Log is already started");
            DataStream = new System.IO.StreamWriter(fileName, false, Encod);
        }
        virtual public void Close()
        {
            if (IsStarted)
            {
                DataStream.Close();
            }
        }
        public abstract void AddData(DateTime dt, ulong tagid, AbxProtocol.AbxData resp);
        public abstract void AddEvent(string eventId, string message);
        protected void Write(string data)
        {
            DataStream.Write(data);
        }
    }
}
