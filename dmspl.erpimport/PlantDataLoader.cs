using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmspl.dataprovider;
using System.Data;
using System.IO;

namespace dmspl.erpimport
{
    class PlantDataLoader : IPlantDataLoader
    {
        public string FilePath { get; set; }

        private static DataSet OpenXLSX(string xlsxpath)
        {
            if (!File.Exists(xlsxpath)) return null;
            using (FileStream fs = File.Open(xlsxpath, FileMode.Open))
            {
                Excel.IExcelDataReader edr = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                return edr.AsDataSet();
            }
        }

        public List<string> ImportDataFromFile()
        {
            return ImportDataFromFile(FilePath);
        }

        public List<string> ImportDataFromFile(string filepath)
        {
            FilePath = filepath;

            DataSet excel = OpenXLSX(filepath);
            if (excel == null) throw new FileLoadException();

            List<string> plantdata = new List<string>();

            foreach (DataRow item in excel.Tables[0].Rows)
            {
                plantdata.Add(item[0].ToString());
            }

            return plantdata;
        }
    }
}
