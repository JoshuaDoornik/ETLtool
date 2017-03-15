using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace ConsoleApp1
{
    class Connector
    {
       
        AcessController AcController;
        mssqlController MsController;
        public Connector(AcessController AcController, mssqlController MsController) {
            this.MsController = MsController;
            this.AcController = AcController;

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

        public void writeFromACtoMssqldiffcolumns(string selectquery, string table) {

            var toWriteFormat = MsController.readfrommssql(selectquery);
            var toWrite = AcController.readFromAc(selectquery);
            List<TupleWrapper> tuples = new List<TupleWrapper>();
            TupleWrapper tup = new TupleWrapper();
            List<string> format = new List<string>();


            foreach (var item in toWriteFormat.Tables[toWriteFormat.Tables[0].ToString()].Columns)
            {
                
                format.Add(item.ToString());
                Console.WriteLine(item.ToString());
            }

            foreach (DataRow item in toWrite.Tables[toWrite.Tables[0].ToString()].Rows)
            {

                foreach (var col in format)
                {

                    if (col.Equals("klant_id"))
                    {
                        tup.addTuple(col, item["klant_id"]);
                    }
                    if (col.Equals("voorletter"))
                    {
                        tup.addTuple(col, item["voorl"]);
                    }
                    if (col.Equals("tussenvoegsel"))
                    {
                        tup.addTuple(col, item["tv"]);
                    }
                    if (col.Equals("achternaam"))
                    {
                        tup.addTuple(col, item["anaam"]);
                    }
                    if (col.Equals("b_naam"))
                    {
                        tup.addTuple(col, null);
                    }
                    if (col.Equals("straat"))
                    {
                        tup.addTuple(col, item["str"]);
                    }
                    if (col.Equals("huis_nr"))
                    {
                        tup.addTuple(col, item["hnr"]);
                    }
                    if (col.Equals("achtervoegsel"))
                    {
                        tup.addTuple(col, item["av"]);
                    }
                    if (col.Equals("postcode"))
                    {
                        tup.addTuple(col, item["pc"]);
                    }
                    if (col.Equals("geslacht"))
                    {
                        tup.addTuple(col, item["gesl"]);
                    }
                    if (col.Equals("geboortedatum"))
                    {
                        tup.addTuple(col, item["gebdt"]);
                    }
                }
                MsController.writeWithTupleWrapper(tup);
                tup.deletThis();

            }
       
        }
    }
}
