using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.erpimport
{
    public interface IPlantDataLoader
    {
        string FilePath { get; set; }
        List<string> ImportDataFromFile();
        List<string> ImportDataFromFile(string filepath);
    }
}
