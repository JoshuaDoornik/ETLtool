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

        IDBController Source;
        IDBController Target;

        private HashSet<int> KeyRing= new HashSet<int>();
        private IEnumerator iterator;
        public Connector(IDBController source, IDBController target) {
            this.Target = target;
            this.Source = source;
            
        }
    

        public void writeFromAccessToMSSQL(string selectFromAcess, string SelectFromMsSql,string towriteTable, Dictionary<String, String>tableFormats) {

            string readtablename = selectFromAcess.Split("FROM".ToCharArray())[4];
            var toWriteFormat = Target.Read(SelectFromMsSql);
            var toWrite = Source.Read(selectFromAcess);
            TupleWrapper tup = new TupleWrapper();
            List<string> format = new List<string>();


            foreach (var item in toWriteFormat.Tables[toWriteFormat.Tables[0].ToString()].Columns)
            {
                
                format.Add(item.ToString());
               
            }
            var datatables = toWrite.Tables[toWrite.Tables[0].ToString()].Rows;

            foreach (DataRow item in datatables)
            {

                foreach (var col in format)
                {


 

                        tup.addTuple(col, item[tableFormats[col]]);
                    
                }
                
                Target.Write(tup, towriteTable);
                tup.deletThis();

            }

            Console.WriteLine("Klaar!");
            Console.ReadKey();
        }

    

        public void updateColumn(string selectAC, string updateTable, string updateColumn, string KeyColumn) {
           DataSet set = Source.Read(selectAC);

            foreach (DataRow item in set.Tables[set.Tables[0].ToString()].Rows)
            {
               
                Target.ExecuteNonQuery("UPDATE "+ updateTable + " SET "+updateColumn+" = " + "'" + convertDate(item[updateColumn].ToString()) + "'" 
                + " WHERE "+ KeyColumn + " = " + item[KeyColumn].ToString());
            }

            }

        private string convertDate(string date)
        {
            date = date.Remove(date.Length - 10, 9);
            var folders = date.Split(new char[] { '/' });

            string temp = folders[0];

            folders[0] = folders[2];
            folders[2] = temp;

            temp = folders[0] + '/' + folders[1] + '/' + folders[2];
            DateTime tempdate;
            if (DateTime.TryParse(temp, out tempdate))
            {
                return temp;

            }
            else
            {

                return @"1111/11/11";
            }
        }

            private string removeAccent(string accentStr) {
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentStr);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            
            return asciiStr;
        }
     public void generateKeyRing(int count, int up){
            Random rand = new Random();
           
            while (KeyRing.Count != count)
            {
                KeyRing.Add(rand.Next(0, up));

            }
        
            iterator = KeyRing.GetEnumerator();
        }

        public int getKey() {

            iterator.MoveNext();
            Console.WriteLine(iterator.Current);
            return (int) iterator.Current;
        }
    }

}
