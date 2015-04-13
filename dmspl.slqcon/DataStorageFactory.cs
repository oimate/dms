using dmspl.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.datastorage
{
    public static class DataStorageFactory
    {
        public static IDataStorage CreateStorage(DataStorageType type)
        {
            switch (type)
            {
                case DataStorageType.SQL:
                    return new DataStorageSQL();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
