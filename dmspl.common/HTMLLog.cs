using System;
using System.Collections.Generic;
using System.Text;

namespace TagReader.Log
{
    public class HTMLLog : FileLog
    {
        public HTMLLog(string filename)
            : base(filename, Encoding.UTF8)
        {
        }

        public override void Start(DatasetDecl decl)
        {
            base.Start(decl);
            if (!IsStarted)
                return;
            Write("<!doctype html>\r\n");
            Write("<html>\r\n");
            Write("<head>\r\n");
            Write(string.Format("<title>RFID LOG: {0:yyyy-MM-dd hh:mm:ss}</title>\r\n", DateTime.Now));
            Write("<meta charset=\"UTF-8\"/>\r\n");
            Write(Properties.Resources.HtmLogStyle);
            Write("\r\n");
            Write(Properties.Resources.HtmlLogScript);
            Write("\r\n");
            Write("</head>\r\n");
            Write("<body>\r\n");
            Write("<div id=\"dmenu\" onmouseover=\"Mnu(true);\" onmouseout=\"Mnu(false);\">\r\n");
            Write("<ul class=\"visul\">\r\n");
            Write("<li class=\"viscpt\">Type</li>\r\n");
            Write("<li onclick=\"SHH('err', this);\" class=\"vissel\">ERROR</li>\r\n");
            Write("<li onclick=\"SHH('data', this);\" class=\"vissel\">DATA</li>\r\n");
            Write("<li onclick=\"SHH('warn', this);\" class=\"vissel\">WARN</li>\r\n");
            Write("</ul>\r\n");
            Write("<ul class=\"visul\">\r\n");
            Write("<li class=\"viscpt\">Columns</li>\r\n");
            int indx = 0;
            Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">TIME</li>\r\n", indx++));
            Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">TYPE</li>\r\n", indx++));
            Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">ID</li>\r\n", indx++));
            foreach (DataField f in Decl.Fields)
                Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">{1}</li>\r\n", indx++, f.Name));
            Write("</ul>\r\n");
            Write("</div>\r\n");
            Write("<div id=\"wraper\">\r\n");
            Write("<table>\r\n");
            Write("<tr>");
            indx = 0;
            Write(string.Format("<th class=\"c{0}\">TIME</th>", indx++));
            Write(string.Format("<th class=\"c{0}\">TYPE</th>", indx++));
            Write(string.Format("<th class=\"c{0}\">ID</th>", indx++));
            foreach (DataField f in Decl.Fields)
                Write(string.Format("<th class=\"c{0}\">{1}</th>", indx++, f.Name));
            Write(string.Format("</tr>\r\n"));
        }

        public override void Close()
        {
            if (!IsStarted)
                return;
            Write("</table>\r\n");
            Write("</div>\r\n");
            Write("</body>\r\n");
            Write("</html>\r\n");
            base.Close();
        }

        override public void AddData(DateTime dt, ulong tagid, AbxProtocol.AbxData resp)
        {
            string data = string.Format("{0}", resp);
            int datalen = data.Length;
            string cssclass = "data";

            if (datalen < 100)
                cssclass = "err";
            int indx = 0;
            Write(string.Format("<tr class=\"{0}\">", cssclass));
            Write(string.Format("<td class=\"c{0}\">{1:yy-MM-dd hh:mm:ss}</td>", indx++, dt));
            Write(string.Format("<td class=\"c{0}\">DATA</td>", indx++));
            Write(string.Format("<td class=\"c{0}\">{1:X8}</td>", indx++, tagid));
            foreach (DataField f in Decl.Fields)
                Write(string.Format("<td class=\"c{0}\">{1}</td>", indx++, resp.GetString(f.AbsOffset, f.Length)));
            Write(string.Format("</tr>\r\n"));
        }
        override public void AddEvent(string eventId, string message)
        {
            //Write(string.Format("<tr class=\"event\"><td>{0:yy-MM-dd hh:mm:ss}</td><td>EVENT</td><td>{1}</td><td>{3}</td><td>{4}</td><td class=\"al\">{2}</td></tr>\r\n", DateTime.Now, eventId, message, "", ""));
        }
    }    
}
