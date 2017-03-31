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

            MssqlController msController = new MssqlController("user id=DAVID-LAPTOP\\David;" +
                                 "password=;server=(localdb)\\MSSQLLocalDB;" +
                                 "Trusted_Connection=yes;" +
                                 "database=NTU2;" +
                                 "connection timeout=30");


            AcessController AcController = new AcessController("Provider = Microsoft.Jet.OLEDB.4.0; " +
                @"Data source=C:\Users\David\Desktop\Database Project\NTU.mdb");

            //Voor het inserten van klant naar dimensie_Klant
            //"SELECT klant_id, postcode, LEFT(postcode, 4) as pc2, geboortedatum FROM Klant";
            //Voor het inserten van klant naar dimensie_Plaats
            //"SELECT DISTINCT postcode, LEFT(postcode, 4) as pc2 FROM Klant";
            string selectfromAccess = "SELECT DISTINCT postcode, LEFT(postcode, 4) as pc2 FROM Klant";
            string selectfromMSql = "SELECT * FROM dimensie_Plaats";

            Connector connector = new Connector(msController, msController);

            //met het format geven we aan waar welke tabel thuis hoort. dus klant_id in de ene DB hoort in klantId in de ander
            Dictionary<string, string> format = new Dictionary<string, string>();
            
            format.Add("plaats_id", "postcode");
            format.Add("provincie", "pc2");
            
          // connector.updateColumn("SELECT * FROM Activiteit Inner join Event On event.activ_id = activiteit.activ_id", "activiteit","eind_dt","activiteit.activ_id");
           connector.writeFromAccessToMSSQL(selectfromAccess, selectfromMSql, "dimensie_Plaats", format);
            

       
            AcController.conn.Close();
            Console.ReadLine();

        }
      
        }
    }

