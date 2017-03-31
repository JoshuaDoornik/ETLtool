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

            MssqlController msController = new MssqlController("user id=DESKTOP-7LQ5LCT\\Joshua;" +
                                 "password=;server=(localdb)\\MSSQLLocalDB;" +
                                 "Trusted_Connection=yes;" +
                                 "database=NTU;" +
                                 "connection timeout=30");

            AcessController AcController = new AcessController("Provider = Microsoft.Jet.OLEDB.4.0; " +
                @"Data source=C:\Users\Joshua\Desktop\Database Project\NTU.mdb");

            string selectfromAccess = "SELECT * FROM Event WHERE prijs_abo = 0.00";
            string selectfromMSql = "SELECT * FROM Event";

            Connector connector = new Connector(AcController, msController);

            //met het format geven we aan waar welke tabel thuis hoort. dus klant_id in de ene DB hoort in klantId in de ander
            Dictionary<string, string> format = new Dictionary<string, string>();




            format.Add("event_id", "prod_nr");
            format.Add("prijs_abo", "prijs_abo");
            format.Add("begin_dt", "begin_dt");
            format.Add("eind_dt", "eind_dt");
            format.Add("locatie_id", "locatie");




           connector.updateColumn("SELECT * FROM Activiteit Inner join Event On event.activ_id = activiteit.activ_id", "activiteit","eind_dt","activiteit.activ_id");
         //  connector.writeFromAccessToMSSQL(selectfromAccess, selectfromMSql, "Event", format);
            

       
            AcController.conn.Close();
            Console.ReadLine();

        }
      
        }
    }

