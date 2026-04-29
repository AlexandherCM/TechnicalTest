using App.Interfaces;
using Domain;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TechnicalTest.InterfaceAdapter.Adapters
{   
    public class SerializeAdapter: ISerialize
    {
        //CLASE PROTEGIDA QUE CONFIGURA LA CODIFICACIÓN DEL XML
        private class StringWriterWithEncoding : StringWriter
        {
            private readonly Encoding m_Encoding;
            public StringWriterWithEncoding(Encoding encoding) : base()
            {
                m_Encoding = encoding;
            }

            public override Encoding Encoding
            {
                get { return m_Encoding; }
            }
        }

        //CONVERSIÓN DE TODO EL XML A OBJETO DE C#
        public T DeserializeFullXML<T>(string xmlContent)  
        {
            // Crear el serializador para el objeto Comprobante
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            // Deserializar el XML contenido en la cadena
            T xml;
            using (StringReader stringReader = new StringReader(xmlContent))
            {
                xml = (T)serializer.Deserialize(stringReader);
            }   

            return xml;
        }

        public string ConvertirXML<T>(T obj)
        {   
            // Serialización del Xml - - - - - - - - - - - - - - - - - - - - - - - - - 
            XmlSerializer OXmlSerializer = new XmlSerializer(typeof(T));    
            // Serialización del Xml - - - - - - - - - - - - - - - - - - - - - - - - - 

            string sXml = string.Empty;

            using (var sww = new StringWriterWithEncoding(Encoding.UTF8))
            {
                using (XmlWriter writter = new XmlTextWriter(sww))
                {
                    OXmlSerializer.Serialize(writter, obj);
                    sXml = sww.ToString();
                }
            }

            return sXml;
        }
    }
}