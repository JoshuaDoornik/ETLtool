using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class TupleWrapper
    {
       public Dictionary<string, object>tuple = new Dictionary<string, object>();
       



        public TupleWrapper(string key, object data)
        {
            tuple.Add(key, data);
        }
        public TupleWrapper() {

        }

        public void addTuple(string key, object data) {

            tuple.Add(key, data);
        }

        public Dictionary<string, object> getRow() {

            return this.tuple;

        }
        public string[] getColumns()
        {
            return tuple.Keys.ToArray();
        }

        public object[] getData()
        {
            return tuple.Values.ToArray();
        }

        public void deletThis() {

            tuple.Clear();
        }
    }

}
