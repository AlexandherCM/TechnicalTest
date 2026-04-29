using System.Xml.Serialization;

namespace Domain
{
    public class ColoniaXml
    {
        [XmlAttribute("ID")]
        public string Id { get; set; }

        [XmlElement("DESCRIPCION_COLONIA")]
        public string DescripcionColonia { get; set; }

        [XmlElement("CODIGO_COLONIA")]
        public string CodigoColonia { get; set; }
    }
}
