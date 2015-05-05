using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common
{
    public enum DataStorageState : int
    {
        Connecting,
        Initializing,
        Ready,
        Offline,
    }
}
