using dmspl.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace dmspl.dataimporter
{
    class DataImporterExcel : IDataImporter
    {
        public List<string> ImportedData { get; set; }

        public async Task ImportDataFromFileAsync(string filepath)
        {
            await Task.Run(() =>
                {
                    List<string> productionplandata = new List<string>();
                    DataSet excel;

                    using (FileStream fs = File.Open(filepath, FileMode.Open))
                    {
                        Excel.IExcelDataReader edr = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                        excel = edr.AsDataSet();
                    }

                    if (excel == null || excel.Tables.Count == 0 || excel.Tables[0].Rows.Count == 0) return;

                    foreach (DataRow item in excel.Tables[0].Rows)
                    {
                        productionplandata.Add(item[0].ToString());
                    }
                    ImportedData = productionplandata;
                }
            );
        }
    }
}