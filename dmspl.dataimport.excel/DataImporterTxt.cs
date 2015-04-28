using dmspl.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace dmspl.dataimporter
{
    class DataImporterTxt : IDataImporter
    {
        public List<string> ImportedData { get; set; }

        public async Task ImportDataFromFileAsync(string filepath)
        {
            Task t = Task.Run(() =>
                {
                    List<string> productionplandata = new List<string>();

                    if (!File.Exists(filepath)) return;

                    using (StreamReader sr = new StreamReader(filepath))
                    {
                        string line = string.Empty;
                        while (!sr.EndOfStream)
                        {
                            line = sr.ReadLine().Trim();
                            if (!string.IsNullOrWhiteSpace(line)) productionplandata.Add(line);
                        }
                    }
                    ImportedData = productionplandata;
                }
            );
            await t;
        }
    }
}