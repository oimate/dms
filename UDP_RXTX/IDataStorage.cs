using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDP_RXTX
{
   public interface IDataStorage
    {
       void Invoke(int i, List<int> l);
    }
}
