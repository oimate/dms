using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common
{
    public interface IUDPComm
    {
        //void ProvideData(DMSData data);
        DelegateCollection.SetTextDel ReferencjaDoFunkcjiWyswietlajacejText { get; set; }
    }
}
