using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common
{
    public enum DataStorageType
    {
        SQL,
    }

    public enum PLCRequestType
    {
        UpdateMFP,
        RequestDataForVIN,
        ChangeSkidNr,
    }

    delegate void InvokeUpdate(PLCRequestType reqtype, List<int> data);
}
