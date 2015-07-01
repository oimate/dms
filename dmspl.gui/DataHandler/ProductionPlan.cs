using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmspl.common;
using dmspl.dataimporter;
using dmspl.datastorage;

namespace dmspl.datahandler
{
    static class ProductionPlan
    {
        public static async Task UpdateProductionPlan(string filepath, ImporterType importertype, IDataStorage datastorage)
        {
            IDataImporter importer = DataImporterFactory.CreateImporter(ImporterType.txt);
            await importer.ImportDataFromFileAsync(filepath);
            List<string> costam = importer.ImportedData;
            //await Task.Run( () => datastorage.StoreProductionData(costam));
        }
    }
}