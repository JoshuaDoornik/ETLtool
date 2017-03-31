using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface IDBController
    {
        void Write(TupleWrapper tuple, string tablename);
        DataSet Read(string query);
        void ExecuteNonQuery(string query);
    }
}
