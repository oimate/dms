using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dmspl
{
    public enum DMSSessionStatus
    {
        unknown,
        connecting,
        connected,
        disconnected,
        connectionproblem,
        disconnectionproblem,
        disconnecting,
    }
}
