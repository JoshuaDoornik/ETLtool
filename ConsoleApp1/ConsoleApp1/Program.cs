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

            string selectfromAccess = "SELECT medewerker_code, voornaam, achternaam, functie,email, datum_in_dienst FROM Medewerkers";
            string selectfromMSql = "select medewerker_code, voornaam, achternaam, functie,email, datum_in_dienst from Medewerker";

            Connector connector = new Connector(AcController, msController);

            //met het format geven we aan waar welke tabel thuis hoort. dus klant_id in de ene DB hoort in klantId in de ander
            Dictionary<string, string> format = new Dictionary<string, string>();

          

             
               format.Add("medewerker_code", "medewerker_code");
               format.Add("voornaam", "voornaam");
               format.Add("achternaam","achternaam");
            format.Add("functie", "functie");
            format.Add("email", "email");
            format.Add("datum_in_dienst", "datum_in_dienst");
          
           // connector.generateKeyRing(1000, 20000);


            connector.writeFromACtoMssqldiffcolumns(selectfromAccess, selectfromMSql, "medewerker", format);
            

       
            AcController.conn.Close();
            Console.ReadLine();

        }
      
        }
    }

