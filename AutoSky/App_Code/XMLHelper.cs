using System;
using System.IO;
using System.Xml.Serialization;

namespace AutoSky.App_Code
{
    class XmlHelper
    {
        private const string PlacemarkXmlFile = "..\\..\\Xml\\Placemarks.xml";

        public static Placemarks ReadPlacemarkXml()
        {
            var serReader = new XmlSerializer(typeof(Placemarks));
            const string fileName = PlacemarkXmlFile;
            var fs = new FileStream(fileName, FileMode.Open);
            var placemarks = serReader.Deserialize(fs) as Placemarks;

            fs.Flush();
            fs.Close();
            return placemarks;
        }

        public static int WritePlacemarkXml(Placemarks placemarks)
        {
            StreamWriter file = null;
            try
            {
                var serWriter = new XmlSerializer(typeof (Placemarks));
                file = new StreamWriter(PlacemarkXmlFile);
                serWriter.Serialize(file, placemarks);
                file.Close();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {
                if (file != null )
                    file.Close();
            }
        }
    }
}
