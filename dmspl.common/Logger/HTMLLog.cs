using System;
using System.Collections.Generic;
using System.Text;

namespace dmspl.common.log
{
    public class HTMLLog : FileLog
    {
        public HTMLLog(string filename)
            : base(filename, Encoding.UTF8)
        {
        }

        public override void Start()
        {
            base.Start();
            if (!IsStarted)
                return;
 //           InitModStream(Module.Appl);
 //           InitModStream(Module.DataBase);
 //           InitModStream(Module.RXTXComm);
            InitModStream(Module.All);
        }
        private void InitModStream(Module mod)
        {
            Write("<!doctype html>\r\n", mod);
            Write("<html>\r\n", mod);
            Write("<head>\r\n", mod);
            Write(string.Format("<title>RFID LOG: {0:yyyy-MM-dd hh:mm:ss}</title>\r\n", DateTime.Now), mod);
            Write("<meta charset=\"UTF-8\"/>\r\n", mod);
            Write(Properties.Resource.HTMLStyle, mod);
            Write("\r\n", mod);
            Write(Properties.Resource.HTMLScript, mod);
            Write("\r\n", mod);
            Write("</head>\r\n", mod);
            Write("<body>\r\n", mod);
            Write("<div id=\"dmenu\" onmouseover=\"Mnu(true);\" onmouseout=\"Mnu(false);\">\r\n", mod);
            Write("<ul class=\"visul\">\r\n", mod);
            Write("<li class=\"viscpt\">Type</li>\r\n", mod);
            Write("<li onclick=\"SHH('Appl', this);\" class=\"vissel\">APPL</li>\r\n", mod);
            Write("<li onclick=\"SHH('RXTXComm', this);\" class=\"vissel\">RXTXCOM</li>\r\n", mod);
            Write("<li onclick=\"SHH('DataBase', this);\" class=\"vissel\">DATABASE</li>\r\n", mod);
            Write("</ul>\r\n", mod);

            //switch (mod)
            //{
            //    case Module.All:
            //        Write("<ul class=\"visul\">\r\n", mod);
            //        Write("<li class=\"viscpt\">Module</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('Appl', this);\" class=\"vissel\">APPL</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('RXTXComm', this);\" class=\"vissel\">RXTXCOM</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('DataBase', this);\" class=\"vissel\">DATABASE</li>\r\n", mod);
            //        Write("</ul>\r\n", mod);
            //        break;
            //    case Module.Appl:
            //        Write("<ul class=\"visul\">\r\n", mod);
            //        Write("<li class=\"viscpt\">Module</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('Appl', this);\" class=\"vissel\">ALL</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('RXTXComm', this);\" class=\"vissel\">RXTXCOM</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('DataBase', this);\" class=\"vissel\">DATABASE</li>\r\n", mod);
            //        Write("</ul>\r\n", mod);
            //        break;
            //    case Module.DataBase:
            //        Write("<ul class=\"visul\">\r\n", mod);
            //        Write("<li class=\"viscpt\">Module</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('Appl', this);\" class=\"vissel\">APPL</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('RXTXComm', this);\" class=\"vissel\">RXTXCOM</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('DataBase', this);\" class=\"vissel\">ALL</li>\r\n", mod);
            //        Write("</ul>\r\n", mod);
            //        break;
            //    case Module.RXTXComm:
            //        Write("<ul class=\"visul\">\r\n", mod);
            //        Write("<li class=\"viscpt\">Module</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('Appl', this);\" class=\"vissel\">APPL</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('RXTXComm', this);\" class=\"vissel\">ALL</li>\r\n", mod);
            //        Write("<li onclick=\"SHH('DataBase', this);\" class=\"vissel\">DATABASE</li>\r\n", mod);
            //        Write("</ul>\r\n", mod);
            //        break;

            //    default:
            //        break;
            //}
            Write("<ul class=\"visul\">\r\n", mod);
            Write("<li class=\"viscpt\">Columns</li>\r\n", mod);
            int indx = 0;
            Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">Time</li>\r\n", indx++), mod);
            Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">Module</li>\r\n", indx++), mod);
            Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">Type<li>\r\n", indx++), mod);
            Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">Level<li>\r\n", indx++), mod);
            Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">Description<li>\r\n", indx++), mod);
            //          foreach (DataField f in Decl.Fields)
            //            Write(string.Format("<li onclick=\"SHH('c{0}', this);\" class=\"vissel\">{1}</li>\r\n", indx++, f.Name));
            Write("</ul>\r\n", mod);
            Write("</div>\r\n", mod);
            Write("<div id=\"wraper\">\r\n", mod);
            Write("<table>\r\n", mod);
            Write("<tr>", mod);
            indx = 0;
            Write(string.Format("<th class=\"c{0}\">TIME</th>", indx++), mod);
            Write(string.Format("<th class=\"c{0}\">Module</th>", indx++), mod);
            Write(string.Format("<th class=\"c{0}\">Type</th>", indx++), mod);
            Write(string.Format("<th class=\"c{0}\">Level</th>", indx++), mod);
            Write(string.Format("<th class=\"c{0}\">Description</th>", indx++), mod);
            //foreach (DataField f in Decl.Fields)
            //    Write(string.Format("<th class=\"c{0}\">{1}</th>", indx++, f.Name));
            Write(string.Format("</tr>\r\n"), mod);
        }

        void CloseModSteram(Module mod)
        {
            Write("</table>\r\n", mod);
            Write("</div>\r\n", mod);
            Write("</body>\r\n", mod);
            Write("</html>\r\n", mod);
        }

        public override void Close()
        {
            if (!IsStarted)
                return;
            CloseModSteram(Module.Appl);
            CloseModSteram(Module.DataBase);
            CloseModSteram(Module.RXTXComm);
            CloseModSteram(Module.All);
            base.Close();
        }

        protected override string GetEventText(DateTime dt, Module mod, EvType typ, Level lv, object data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<tr class=\"{0}\">", mod));
            sb.Append(string.Format("<td class=\"c{0}\">{1:yy-MM-dd hh:mm:ss}</td>", 0, dt));
            sb.Append(string.Format("<td class=\"c{0}\">{1}</td>", 1, mod));
            sb.Append(string.Format("<td class=\"c{0}\">{1}</td>", 2, typ));
            sb.Append(string.Format("<td class=\"c{0}\">{1}</td>", 3, lv));
            sb.Append(string.Format("<td class=\"c{0}\">{1}</td>", 4, data));
            sb.Append(string.Format("</tr>\r\n"));
            return sb.ToString();
        }

    }
}
