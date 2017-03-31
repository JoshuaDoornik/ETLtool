using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class MssqlController : IDBController
    {
        private string ConnectionString;
        private string batch;
        private int counter = 0;
        public MssqlController(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void ExecuteNonQuery(string query)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                using (SqlCommand myCommand = new SqlCommand(query, Connection))
                {

                    int affectedrows = 0;

                    try
                    {
                        affectedrows = myCommand.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.Message);
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Joshua\Desktop\errorlog\LocatieErrorLog.txt", true))
                        {
                            file.WriteLine(e.Message + "\n");
                            file.WriteLine(query + "\n");
                        }

                        
                    }
                }


            }

        }
        public void Write(TupleWrapper tuple, string tablename) {

            object[] rawdata = tuple.getData();
            object[] rawcolumns = tuple.getColumns();
            string columns = rawcolumns[0].ToString();
            string data = "'" + rawdata[0].ToString() + "'";


            for (int i = 1; i < rawcolumns.Length; i++)
            {
                columns += " , " + rawcolumns[i].ToString();
            }

            for (int i = 1; i < rawdata.Length; i++)
            {
                if (rawdata[i].GetType() == typeof(DBNull))
                {
                    data += " , " + "" + "null" + "";
                }
                else if (rawdata[i].GetType() == typeof(DateTime))
                {
                    data += " , " + "'" + convertDate(rawdata[i].ToString()) + "'";
                }
                else {
                    data += " , " + "'" + rawdata[i].ToString().Replace("\'", "") + "'";
                }
    

            }

            string query = "INSERT INTO " + tablename +" (" + columns + ")" + "  " + " VALUES(" + data + ")";


            ExecuteNonQuery(query);


            }

        public void batchInsert(TupleWrapper tuple, string tablename, int batchSize = 10000) {

            object[] rawdata = tuple.getData();
            object[] rawcolumns = tuple.getColumns();
            string columns = rawcolumns[0].ToString();
            string data = "'" + rawdata[0].ToString() + "'";


            for (int i = 1; i < rawcolumns.Length; i++)
            {
                columns += " , " + rawcolumns[i].ToString();
            }

            for (int i = 1; i < rawdata.Length; i++)
            {
                if (rawdata[i] == null)
                {
                    data += " , " + "'" + "null" + "'";
                }
                else if (rawdata[i].GetType() == typeof(DateTime))
                {
                    data += " , " + "'" + convertDate(rawdata[i].ToString()) + "'";
                }
                else
                {
                    data += " , " + "'" + rawdata[i].ToString().Replace("\'", "") + "'";
                }
            }

            batch += "INSERT INTO " + tablename + " (" + columns + ")" + "  " + " VALUES(" + data + ") \n";
            counter++;
            if (counter == batchSize) {
                counter = 0;
                ExecuteNonQuery(batch);
            }

        }

        public DataSet Read(string query)
        {
        
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {

                Connection.Open();

                SqlCommand cmd = new SqlCommand(query, Connection);
                DataSet dataset = new DataSet();
                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                dataset.Tables.Add(table);
                return dataset;

            }
        }

        public string readSingleResult(string sqlquery, string columname) {

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(sqlquery, Connection);
                DataSet dataset = new DataSet();
                string result = null;
                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                dataset.Tables.Add(table);

                foreach (DataTable item in dataset.Tables)
                {

                    Console.WriteLine(item.ToString());

                }

                foreach (DataRow item in dataset.Tables[dataset.Tables[0].ToString()].Rows)
                {
                    Console.WriteLine(item.ToString());
                    result = item[columname].ToString();
                }

                return result;
            }
        }
        private string convertDate(string date) {
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
            else {

                return @"1111/11/11";
            }
                
        }
        public int Count(string tablename) {
            string query = "select count(*) as count from " +tablename;
            int count = 0;

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(query, Connection);
 
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        count = (int) oReader["count"];
                      
                    }

                }
                return count;

            }
        }


    }
}


        
    

