using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common
{
    public interface IDataImporter
    {
        List<string> ImportedData { get; set; }
        Task ImportDataFromFileAsync(string filepath);
    }
}
