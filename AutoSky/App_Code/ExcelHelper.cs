using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoSky.App_Code
{
    class ExcelHelper
    {
        private const string excelFilePath = "..\\..\\planets.xls";

        public static Placemarks readPlanets()
        {
            Placemarks placemarks = new Placemarks();
            placemarks.Items = new List<Placemark>();
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            string query = "SELECT * FROM [planets$]";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Placemark tmp = new Placemark();
                        tmp.Name = reader[1].ToString();
                        double latitude = Double.Parse(reader[5].ToString());
                        double longitude = Double.Parse(reader[3].ToString());
                        if (longitude > 180)
                            longitude = -180 + longitude;
                        tmp.Latitude = latitude;
                        tmp.Longitude = longitude;
                        tmp.Range = 12000000;
                        placemarks.Items.Add(tmp);
                    }
                    reader.Close();
                }
                catch
                {
                }
            }
            return placemarks;

        }
    }
}
