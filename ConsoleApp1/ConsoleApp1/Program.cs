using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {

        
        static void Main(string[] args)
        {

            mssqlController msController = new mssqlController("user id=DESKTOP-7LQ5LCT\\Joshua;" +
                                 "password=;server=(localdb)\\MSSQLLocalDB;" +
                                 "Trusted_Connection=yes;" +
                                 "database=NTU;" +
                                 "connection timeout=30");

            Console.WriteLine("succes connecting to mssql!");


            AcessController AcController = new AcessController("Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data source=C:\Users\Joshua\Desktop\Database Project\NTU.mdb");

            string query = "SELECT * FROM klant";


            Connector connector = new Connector(AcController, msController);
            // connector.writeFromACtoMssql(query, "klant",  "klant_id , voorletter");

            connector.writeFromACtoMssqldiffcolumns(query, "klant", "asdasd");

            AcController.conn.Close();
          
        }

        }
    }

