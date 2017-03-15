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

            string query = "SELECT * FROM Aankoop";


            Connector connector = new Connector(AcController, msController);

            //met het format geven we aan waar welke tabel thuis hoort. dus klant_id in de ene DB hoort in klantId in de ander
            Dictionary<String, string> format = new Dictionary<string, string>();

            format.Add("aankoop_id", "aankoop_nr");
            format.Add("prod_id", "prod_nr");
            format.Add("order_id", "order_nr");
            format.Add("aantal", "aantal");
            format.Add("bedrag","bedrag");

            connector.writeFromACtoMssqldiffcolumns(query, "premielevering", format);

            AcController.conn.Close();
          
        }

        }
    }

