using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common.log
{
   public class XMLLog : FileLog
    {
       System.Xml.Serialization.XmlSerializer doc;
        public XMLLog(string filename)    // konstruktor klasy
            : base(filename, Encoding.UTF8)
        {
        //    doc.Serialize(
        }

        protected override string GetEventText(DateTime dt, Module mod, EvType typ, Level lv, object data)
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Close()
        {
            base.Close();
        }

    }
}
