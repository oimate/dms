using dmspl.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.dataimporter
{
    public static class DataImporterFactory
    {
        public static IDataImporter CreateImporter(ImporterType type)
        {
            switch (type)
            {
                case ImporterType.excel:
                    return new DataImporterExcel();
                case ImporterType.csv:
                    throw new NotImplementedException();
                case ImporterType.txt:
                    return new DataImporterTxt();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
