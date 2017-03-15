using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class mssqlController
    {
        private string ConnectionString;
        public mssqlController(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void WriteToMssql(DataRow row, string tableTowriteTo)
        {
            string data = row.ItemArray[0].ToString();
            string columns = "";

            foreach (var item in row.Table.Columns)
            {
                columns += " , " + item.ToString();
            }

            for (int i = 1; i < row.ItemArray.Length; i++)
            {
                if (row.ItemArray[i].GetType() == typeof(DateTime))
                {
                    data += " , " + "'" + convertDate(row.ItemArray[i].ToString()) + "'";
                }
                else
                {

                    data += " , " + "'" + row.ItemArray[i] + "'";
                }
            }

            string query = "INSERT INTO (" + columns + ")" + tableTowriteTo + " VALUES(" + data + ")"

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                using (SqlCommand myCommand = new SqlCommand(query, Connection))
                {
                    int affectedrows = myCommand.ExecuteNonQuery();

                    if (affectedrows == 0)
                    {
                        Console.WriteLine("error");
                    }
                }
            }
        }

        public void writeWithTupleWrapper(TupleWrapper tuple, string tablename)
        {
            object[] rawdata = tuple.getData();
            object[] rawcolumns = tuple.getColumns();
            string columns = rawcolumns[0].ToString();
            string data = rawdata[0].ToString();

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

            string query = "INSERT INTO " + tablename + " (" + columns + ")" + "  " + " VALUES(" + data + ")";

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();

                using (SqlCommand myCommand = new SqlCommand(query, Connection))
                {
                    int affectedrows = myCommand.ExecuteNonQuery();

                    if (affectedrows == 0)
                    {
                        Console.WriteLine("error");
                    }
                }
            }
        }

        public DataSet readfrommssql(string query)
        {
            string tablename = query.Split("FROM".ToCharArray())[4];
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
    }
}





