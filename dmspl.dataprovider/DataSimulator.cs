using dmspl.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dmspl.dataprovider
{
    public class DataSimulator : IDisposable
    {
        Thread continousThread;
        bool cancelThread, pauseThread;

        IDataStorage datastorage;
        List<int> datatosend;
        private int number;
        private int currentdata;

        public DataSimulator(IDataStorage ds)
        {
            datastorage = ds;

            cancelThread = false;
            pauseThread = false;

            datatosend = new List<int>(new int[400]);

            continousThread = new Thread(ContinousJob);
            continousThread.Name = "dms_datasimulator_thread";
            continousThread.IsBackground = true;
            continousThread.Priority = ThreadPriority.Lowest;
            continousThread.Start();
        }

        void ContinousJob()
        {
            Thread.Sleep(2000); //delay start
            while (!cancelThread)
            {
                if (!pauseThread)
                {
                    continousThread.IsBackground = false;
                    if (datatosend == null || datastorage == null) continue;
                    int r = new Random().Next(0, 9999);
                    datatosend[currentdata] = r;
                    datastorage.UpdateMFP(datatosend);
                    System.Diagnostics.Debug.WriteLine("datatosend[{0:D3}] = {1:D4}", currentdata + 1, r);
                    currentdata = (++currentdata) % 400;
                    continousThread.IsBackground = true;
                    Thread.Sleep(500);
                }
                else
                {
                    continousThread.IsBackground = true;
                    Thread.Sleep(10);
                }
            }
        }

        public void StopThread()
        {
            cancelThread = true;
        }

        public void PauseResumeThread()
        {
            pauseThread = !pauseThread;
        }

        ~DataSimulator()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //someone want the deterministic release of all resources
                //Let us release all the managed resources
                if (continousThread != null && continousThread.IsAlive)
                {
                    cancelThread = true;
                }
            }
            else
            {
                // Do nothing, no one asked a dispose, the object went out of
                // scope and finalized is called so lets next round of GC 
                // release these resources
            }

            // Release the unmanaged resource in any case as they will not be 
            // released by GC
        }
    }
}
