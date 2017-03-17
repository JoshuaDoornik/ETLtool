using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Collections;

namespace ConsoleApp1
{
    class Connector
    {
       
        AcessController AcController;
        mssqlController MsController;

        private HashSet<int> KeyRing= new HashSet<int>();
        private IEnumerator iterator;
        public Connector(AcessController AcController, mssqlController MsController) {
            this.MsController = MsController;
            this.AcController = AcController;
             iterator = KeyRing.GetEnumerator();
        }

        public void writeFromACtoMssql(string query, string table)
        {
            var toWrite = AcController.readFromAc(query);
            var orgtable = toWrite.Tables[0].ToString();
            foreach (DataRow item in toWrite.Tables[orgtable].Rows)
            {
                MsController.WriteToMssql(item, table);
            }
            Console.ReadLine();
        }

        public void writeFromACtoMssqldiffcolumns(string selectFromAcess, string SelectFromMsSql,string towriteTable, Dictionary<String, String>tableFormats) {

            string readtablename = selectFromAcess.Split("FROM".ToCharArray())[4];
            var toWriteFormat = MsController.readfrommssql(SelectFromMsSql);
            var toWrite = AcController.readFromAc(selectFromAcess);
            List<TupleWrapper> tuples = new List<TupleWrapper>();
            TupleWrapper tup = new TupleWrapper();
            List<string> format = new List<string>();


            foreach (var item in toWriteFormat.Tables[toWriteFormat.Tables[0].ToString()].Columns)
            {
                
                format.Add(item.ToString());
               
            }

            foreach (DataRow item in toWrite.Tables[toWrite.Tables[0].ToString()].Rows)
            {

                foreach (var col in format)
                {
  
                    tup.addTuple(col, item[tableFormats[col]]);
                    
                }

                MsController.writeWithTupleWrapper(tup, towriteTable);
                tup.deletThis();

            }

            Console.WriteLine("Klaar!");
            Console.ReadKey();
        }
     public void generateKeyRing(int count,int up, int down){
            Random rand = new Random();
            for (int i = 0; i == count; i++)
            {
                KeyRing.Add(rand.Next(down,up));
            }

        }

        public int getKey() {
            iterator.MoveNext();
            Console.WriteLine(iterator.Current);
            return (int) iterator.Current;
        }
    }

}
