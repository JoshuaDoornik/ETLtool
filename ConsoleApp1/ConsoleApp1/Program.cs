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

            string selectfromAccess = "SELECT distinct(functie) as functienaam FROM Medewerkers";
            string selectfromMSql = "select * from functie";

            Connector connector = new Connector(AcController, msController);

            //met het format geven we aan waar welke tabel thuis hoort. dus klant_id in de ene DB hoort in klantId in de ander
            Dictionary<string, string> format = new Dictionary<string, string>();

          

             
               format.Add("naam", "functienaam");
               format.Add("omschrijving", "functienaam");
            connector.generateKeyRing(1000, 20000);


            connector.writeFromACtoMssqldiffcolumns(selectfromAccess, selectfromMSql, "functie", format);
            

       
            AcController.conn.Close();
            Console.ReadLine();

        }
      
        }
    }

