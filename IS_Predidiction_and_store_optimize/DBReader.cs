using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace IS_Predidiction_and_store_optimize
{
    public class DBReader
    {
        public IDBTypeReader DBTypeReader;
        private string _path;

        public DBReader(string path, IDBTypeReader dBTypeReader)
        {
            _path = path;
            DBTypeReader = dBTypeReader;
        }

        public void ReadDB()
        {
            DBTypeReader.Read(_path);
        }
    }
}
