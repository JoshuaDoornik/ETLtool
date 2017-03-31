using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class AcessController : IDBController
    {

        public OleDbConnection conn;
        private string ConnectionString;
        public AcessController(string connectionString)
        {
            this.ConnectionString = connectionString;
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

        public DataSet Read(string query)
        {

            string tablename = query.Split("FROM".ToCharArray())[4];
            //  string tablename = "training";
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

        public void Write(TupleWrapper tuple, string tablename) {

        
}

        /*     public int Count(string tablename)
             {
                 string query = "select count(*) as count from " + tablename;
                 int count = 0;

                 using (SqlConnection Connection = new SqlConnection(ConnectionString))
                 {
                     Connection.Open();
                     SqlCommand cmd = new SqlCommand(query, Connection);

                     using (SqlDataReader oReader = cmd.ExecuteReader())
                     {
                         while (oReader.Read())
                         {
                             count = (int)oReader["count"];
                         }

                     }
                     return count;
                    
    }  */


    

    public void ExecuteNonQuery(string query)
        {
            using (OleDbConnection Connection = new OleDbConnection(ConnectionString))
            {
                Connection.Open();

                using (OleDbCommand myCommand = new OleDbCommand(query, Connection))
                {

                    int affectedrows = 0;

                    try
                    {
                        affectedrows = myCommand.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.Message);
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Joshua\Desktop\errorlog\ErrorLog.txt", true))
                        {
                            file.WriteLine(e.Message + "\n");
                            file.WriteLine(query + "\n");
                        }


                    }
                }
            }
        }
    }
}
