using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Collections;

namespace ConsoleApp1
{
    class Connector
    {

        IDBController Source;
        IDBController Target;

        private HashSet<int> KeyRing= new HashSet<int>();
        private IEnumerator iterator;
        public Connector(IDBController source, IDBController target) {
            this.Target = target;
            this.Source = source;
            
        }
    

        public void writeFromAccessToMSSQL(string selectFromAcess, string SelectFromMsSql,string towriteTable, Dictionary<String, String>tableFormats) {

            string readtablename = selectFromAcess.Split("FROM".ToCharArray())[4];
            var toWriteFormat = Target.Read(SelectFromMsSql);
            var toWrite = Source.Read(selectFromAcess);
            TupleWrapper tup = new TupleWrapper();
            List<string> format = new List<string>();


            foreach (var item in toWriteFormat.Tables[toWriteFormat.Tables[0].ToString()].Columns)
            {
                
                format.Add(item.ToString());
               
            }
            var datatables = toWrite.Tables[toWrite.Tables[0].ToString()].Rows;

            foreach (DataRow item in datatables)
            {

                foreach (var col in format)
                {
                    if (col.Equals("provincie"))
                    {

                        int provinciecode = int.Parse(item[tableFormats[col]].ToString());

                        if (provinciecode >= 1000 && provinciecode <= 1299 ||
                            provinciecode >= 1380 && provinciecode <= 1383 ||
                            provinciecode >= 1398 && provinciecode <= 1425 ||
                            provinciecode >= 1430 && provinciecode <= 2158 ||
                            provinciecode == 1394 || provinciecode == 2165)
                        {
                            tup.addTuple(col, "Noord-Holland");
                        }
                        else if (provinciecode >= 1300 && provinciecode <= 1379 ||
                                provinciecode >= 3890 && provinciecode <= 3899 ||
                                provinciecode >= 8200 && provinciecode <= 8259 ||
                                provinciecode >= 8300 && provinciecode <= 8322)
                        {
                            tup.addTuple(col, "Flevoland");
                        }
                        else if (provinciecode >= 1390 && provinciecode <= 1393 ||
                                provinciecode >= 1426 && provinciecode <= 1427 ||
                                provinciecode >= 3382 && provinciecode <= 3464 ||
                                provinciecode >= 3467 && provinciecode <= 3769 ||
                                provinciecode >= 3795 && provinciecode <= 3836 ||
                                provinciecode >= 3900 && provinciecode <= 3924 ||
                                provinciecode >= 3926 && provinciecode <= 3999 ||
                                provinciecode >= 4120 && provinciecode <= 4125 ||
                                provinciecode >= 4130 && provinciecode <= 4139 ||
                                provinciecode == 1396)
                        {
                            tup.addTuple(col, "Utrecht");
                        }
                        else if (provinciecode >= 1428 && provinciecode <= 1429 ||
                                provinciecode >= 2159 && provinciecode <= 2164 ||
                                provinciecode >= 2170 && provinciecode <= 3381 ||
                                provinciecode >= 3465 && provinciecode <= 3466 ||
                                provinciecode >= 4126 && provinciecode <= 4129 ||
                                provinciecode >= 4140 && provinciecode <= 4146 ||
                                provinciecode >= 4163 && provinciecode <= 4169 ||
                                provinciecode >= 4200 && provinciecode <= 4209 ||
                                provinciecode >= 4220 && provinciecode <= 4249 ||
                                provinciecode == 4213)
                        {
                            tup.addTuple(col, "Zuid-Holland");
                        }
                        else if (provinciecode >= 3770 && provinciecode <= 3794 ||
                               provinciecode >= 3837 && provinciecode <= 3888 ||
                               provinciecode >= 4000 && provinciecode <= 4119 ||
                               provinciecode >= 4147 && provinciecode <= 4162 ||
                               provinciecode >= 4170 && provinciecode <= 4199 ||
                               provinciecode >= 4211 && provinciecode <= 4212 ||
                               provinciecode >= 4214 && provinciecode <= 4219 ||
                               provinciecode >= 5300 && provinciecode <= 5335 ||
                               provinciecode >= 6500 && provinciecode <= 6583 ||
                               provinciecode >= 6600 && provinciecode <= 7399 ||
                               provinciecode >= 8050 && provinciecode <= 8054 ||
                               provinciecode >= 8070 && provinciecode <= 8099 ||
                               provinciecode >= 8160 && provinciecode <= 8195 ||
                               provinciecode == 3925 || provinciecode == 7439)
                        {

                            tup.addTuple(col, "Gelderland");

                        }
                        else if (provinciecode >= 4250 && provinciecode <= 4299 ||
                                provinciecode >= 4600 && provinciecode <= 4671 ||
                                provinciecode >= 4680 && provinciecode <= 4681 ||
                                provinciecode >= 4700 && provinciecode <= 5299 ||
                                provinciecode >= 5340 && provinciecode <= 5765 ||
                                provinciecode >= 5820 && provinciecode <= 5846 ||
                                provinciecode >= 6020 && provinciecode <= 6029)
                       {
                        tup.addTuple(col, "Noord-Brabant");
                       }
                       else if (provinciecode >= 5766 && provinciecode <= 5817 ||
                               provinciecode >= 5850 && provinciecode <= 6019 ||
                               provinciecode >= 6030 && provinciecode <= 6499 ||
                               provinciecode >= 6584 && provinciecode <= 6599)
                       {
                            tup.addTuple(col, "Limburg");
                       }
                       else if (provinciecode >= 7400 && provinciecode <= 7438 ||
                               provinciecode >= 7440 && provinciecode <= 7739 ||
                               provinciecode >= 7767 && provinciecode <= 7799 ||
                               provinciecode >= 7950 && provinciecode <= 7955 ||
                               provinciecode >= 8000 && provinciecode <= 8049 ||
                               provinciecode >= 8055 && provinciecode <= 8069 ||
                               provinciecode >= 8100 && provinciecode <= 8159 ||
                               provinciecode >= 8196 && provinciecode <= 8199 ||
                               provinciecode >= 8260 && provinciecode <= 8299 ||
                               provinciecode >= 8323 && provinciecode <= 8349 ||
                               provinciecode >= 8355 && provinciecode <= 8379)
                        {
                            tup.addTuple(col, "Overijssel");
                        }
                        else if (provinciecode >= 7740 && provinciecode <= 7766 ||
                                provinciecode >= 7800 && provinciecode <= 7949 ||
                                provinciecode >= 7956 && provinciecode <= 7999 ||
                                provinciecode >= 8350 && provinciecode <= 8354 ||
                                provinciecode >= 8380 && provinciecode <= 8387 ||
                                provinciecode >= 9300 && provinciecode <= 9349 ||
                                provinciecode >= 9400 && provinciecode <= 9478 ||
                                provinciecode >= 9480 && provinciecode <= 9499 ||
                                provinciecode >= 9510 && provinciecode <= 9539 ||
                                provinciecode >= 9571 && provinciecode <= 9579 ||
                                provinciecode >= 9654 && provinciecode <= 9659 ||
                                provinciecode >= 9760 && provinciecode <= 9769 ||
                                provinciecode == 9564 || provinciecode == 9749)
                        {

                            tup.addTuple(col, "Drenthe");
                        }
                        else if (provinciecode >= 9350 && provinciecode <= 9399 ||
                                provinciecode >= 9500 && provinciecode <= 9509 ||
                                provinciecode >= 9540 && provinciecode <= 9563 ||
                                provinciecode >= 9565 && provinciecode <= 9569 ||
                                provinciecode >= 9580 && provinciecode <= 9653 ||
                                provinciecode >= 9660 && provinciecode <= 9748 ||
                                provinciecode >= 9750 && provinciecode <= 9759 ||
                                provinciecode >= 9770 && provinciecode <= 9849 ||
                                provinciecode >= 9860 && provinciecode <= 9869 ||
                                provinciecode >= 9880 && provinciecode <= 9999 ||
                                provinciecode == 9479)
                        {
                            tup.addTuple(col, "Groningen");
                        }
                        else if (provinciecode >= 8388 && provinciecode <= 9299 ||
                                provinciecode >= 9850 && provinciecode <= 9859 ||
                                provinciecode >= 9870 && provinciecode <= 9879)
                        {
                            tup.addTuple(col, "Friesland");
                        }
                        else if (provinciecode >= 4300 && provinciecode <= 4599 ||
                                provinciecode >= 4672 && provinciecode <= 4679 ||
                                provinciecode >= 4682 && provinciecode <= 4699)
                        {
                            tup.addTuple(col, "Zeeland");
                        }
                        else
                        {
                            tup.addTuple(col, "FOUT");
                        }
                    }else
                    {
                        tup.addTuple(col, item[tableFormats[col]]);
                    }
                }
                
                Target.Write(tup, towriteTable);
                tup.deletThis();

            }

            Console.WriteLine("Klaar!");
            Console.ReadKey();
        }

    

        public void updateColumn(string selectAC, string updateTable, string updateColumn, string KeyColumn) {
           DataSet set = Source.Read(selectAC);

            foreach (DataRow item in set.Tables[set.Tables[0].ToString()].Rows)
            {
               
                Target.ExecuteNonQuery("UPDATE "+ updateTable + " SET "+updateColumn+" = " + "'" + convertDate(item[updateColumn].ToString()) + "'" 
                + " WHERE "+ KeyColumn + " = " + item[KeyColumn].ToString());
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

            private string removeAccent(string accentStr) {
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentStr);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            
            return asciiStr;
        }
     public void generateKeyRing(int count, int up){
            Random rand = new Random();
           
            while (KeyRing.Count != count)
            {
                KeyRing.Add(rand.Next(0, up));

            }
        
            iterator = KeyRing.GetEnumerator();
        }

        public int getKey() {

            iterator.MoveNext();
            Console.WriteLine(iterator.Current);
            return (int) iterator.Current;
        }
    }

}
