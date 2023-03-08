using System;
using System.IO;
using System.Xml.Serialization;


namespace XMLFileSettings
{
    public class PropsFields
    {
        public String XMLFileName = AppDomain.CurrentDomain.BaseDirectory + "settings.xml";

        public int selectModeTheme = 0;

    }


    class Props {

        public PropsFields Fields;

        public Props() {

            Fields = new PropsFields();
        }

        //Запись настроек в файл
        public void WriteXml() {

            XmlSerializer ser = new XmlSerializer(typeof(PropsFields));

            TextWriter writer = new StreamWriter(Fields.XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
        }

        //Чтение насроек из файла
        public void ReadXml() {

            if (File.Exists(Fields.XMLFileName)) {

                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextReader reader = new StreamReader(Fields.XMLFileName);
                Fields = ser.Deserialize(reader) as PropsFields;
                reader.Close();
            }
        }
    }
}