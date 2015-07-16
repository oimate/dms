using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCom
{
    public class ReciverService
    {
        public ReciverService()
        {
            var server = new NetConnection();
            server.Start(40404);
        }
    }
}
