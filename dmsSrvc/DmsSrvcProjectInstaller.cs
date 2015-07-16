using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace dmsSrvc
{
    [RunInstaller(true)]
    public partial class DmsSrvcProjectInstaller : System.Configuration.Install.Installer
    {
        public DmsSrvcProjectInstaller()
        {
            InitializeComponent();
        }

        //protected override void OnBeforeInstall(IDictionary savedState)
        //{ 
        //    string parameter = "MySource1\" \"MyLogFile1"; 
        //    Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" \"" + parameter + "\""; 
        //    base.OnBeforeInstall(savedState); 
        //}
    }
}
