using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common
{
    public class ErpDataset
    {
        public int SkidID { get; set; }
        public int DerivativeCode { get; set; }
        public int Colour { get; set; }
        public int BSN { get; set; }
        public int Track { get; set; }
        public int Roof { get; set; }
        public int HoD { get; set; }
        public int Spare { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public int CreateUser { get; set; }
        public DateTime UpdateTimestamp { get; set; }
        public int UpdateUser { get; set; }
    }
}
