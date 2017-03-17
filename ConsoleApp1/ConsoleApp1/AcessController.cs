using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.OleDb;
using System.Data;

namespace ConsoleApp1
{
    class AcessController
    {

        public OleDbConnection conn;
        public AcessController(string connectionString)
        {
            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {

                Console.WriteLine(e); ;
            }



        }

        public DataSet readFromAc(string query)
        {

            string tablename = query.Split("FROM".ToCharArray())[4];
            DataSet myDataSet = new DataSet();

            OleDbCommand selectFromAccess = new OleDbCommand(query, conn);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(selectFromAccess);



            myDataAdapter.Fill(myDataSet, tablename);
            DataTableCollection dta = myDataSet.Tables;

            foreach (DataTable dt in dta)
            {
                Console.WriteLine("Found data table {0}", dt.TableName);
                Console.WriteLine("{0} tables in data set", dta.Count);
            }
            DataRowCollection dra = myDataSet.Tables[tablename].Rows;
            DataColumnCollection drc = myDataSet.Tables[tablename].Columns;
            string[] columns = new string[drc.Count];

            int i = 0;
            foreach (var item in drc)
            {
                columns[i] = item.ToString();
                i++;
            }



            return myDataSet;
        }
    }
}
