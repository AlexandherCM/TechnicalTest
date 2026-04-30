using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Domain
{
    [XmlRoot("DATA")]
    public class PostalCodeXmlResponseEntity
    {
        public PostalCodeXmlResponseEntity()
        {
            Colonias = new List<ColoniaXmlEntity>();
        }

        [XmlElement("DESCESTADO")]
        public string DescripcionEstado { get; set; }

        [XmlElement("CODESTADO")]
        public string CodigoEstado { get; set; }

        [XmlElement("CODMUNICIPIO")]
        public string CodigoMunicipio { get; set; }

        [XmlElement("DESCMUNICIPIO")]
        public string DescripcionMunicipio { get; set; }

        [XmlArray("COLONIAS")]
        [XmlArrayItem("COLONIA")]
        public List<ColoniaXmlEntity> Colonias { get; set; }
    }
}
