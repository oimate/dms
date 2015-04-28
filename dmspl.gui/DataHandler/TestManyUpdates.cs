using dmspl.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dmspl.datahandler
{
    public class TestManyUpdates
    {
        bool stop;
        private string filepath;
        private ImporterType importertype;
        private IDataStorage datastorage;

        public TestManyUpdates()
        {
            stop = false;
        }

        public void Stop()
        {
            System.Diagnostics.Debug.WriteLine("TestManyUpdates::Stop()");
            stop = true;
        }

        private void Job()
        {
            while (!stop)
            {
                System.Diagnostics.Debug.WriteLine("TestManyUpdates::Job()");
                ProductionPlan.UpdateProductionPlan(filepath, importertype, datastorage);
                Thread.Sleep(20);
            }
        }

        public void Start(string filepath, ImporterType importertype, IDataStorage datastorage)
        {
            System.Diagnostics.Debug.WriteLine("TestManyUpdates::Start()");
            this.filepath = filepath;
            this.importertype = importertype;
            this.datastorage = datastorage;
            Thread th = new Thread(Job);
            th.Start();
        }
    }
}
