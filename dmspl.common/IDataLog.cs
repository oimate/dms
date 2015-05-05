using System;
using System.Collections.Generic;
using System.Text;

namespace TagReader.Log
{
    public interface IDataLog
    {
        bool IsStarted { get; }
        void Start(DatasetDecl decl);
        void AddData(DateTime dt, ulong tagid, AbxProtocol.AbxData resp);
        void AddEvent(string eventId, string message);
        void Close();
    }
}
