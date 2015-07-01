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
        public int Hood { get; set; }
        public int Spare { get; set; }
        public int LeftPlant { get; set; }
        public DateTime Timestamp { get; set; }
        public int fk_User { get; set; }

        public override string ToString()
        {
            return string.Format("SkidID:{0}, DerivativeCode:{1}, Colour:{2}, BSN:{3}, Track:{4}, Roof:{5}, Hood:{6}, Spare:{7}, LeftPlant:{8}, Timestamp:{9}"
                ,SkidID
                ,DerivativeCode
                ,Colour
                ,BSN
                ,Track
                ,Roof
                ,Hood
                ,Spare
                ,LeftPlant
                ,Timestamp);
        }
    }
}
