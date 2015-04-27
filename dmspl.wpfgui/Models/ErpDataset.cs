using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.wpfgui.Models
{
    class ErpDataset
    {
        public int ID { get { return int.Parse(LocalSkidID); } }
        public string LocalSkidID { get; set; }
        public string DerivativeCode { get; set; }
        public string Colour { get; set; }
        public string BSN { get; set; }
        public string Track { get; set; }
        public string Roof { get; set; }
        public string HoD { get; set; }
        public string Spare { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public int CreateUser { get; set; }
        public DateTime UpdateTimestamp { get; set; }
        public int UpdateUser { get; set; }
    }
}
