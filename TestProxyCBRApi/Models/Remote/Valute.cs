using System.Globalization;
using System.Xml.Serialization;

namespace TestProxyCBRApi.Models.Remote {
    /// <summary>
    /// Информация о конкретной валюте и её курсе
    /// </summary>
    public class Valute {
        /// <summary>
        /// Уникальный идентификатор валюты (атрибут ID).
        /// </summary>
        [XmlAttribute("ID")]
        public string ID { get; set; }

        /// <summary>
        /// ISO Цифровой код валюты
        /// </summary>
        [XmlElement("NumCode")]
        public ushort NumCode { get; set; }

        /// <summary>
        /// ISO Буквенный код валюты
        /// </summary>
        [XmlElement("CharCode")]
        public string CharCode { get; set; }

        /// <summary>
        /// Номинал (ед.)
        /// </summary>
        [XmlElement("Nominal")]
        public int Nominal { get; set; }

        /// <summary>
        /// Название валюты
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        [XmlElement("Value")]
        public string Value { get; set; }

        /// <summary>
        /// Курс за 1 единицу валюты
        /// </summary>
        [XmlElement("VunitRate")]
        public string VunitRate { get; set; }
    }
}