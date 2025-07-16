using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TestProxyCBRApi.Models.Remote {
    /// <summary>
    /// Официальные курсы валют на заданную дату устанавливаемые ежедневно
    /// </summary>
    public class ValCurs {
        
        /// <summary>
        /// Дата установления курса в строковом формате
        /// </summary>
        [XmlAttribute("Date")]
        public string DateString {
            get => Date.ToString("dd.MM.yyyy");
            set => Date = DateTime.ParseExact(value, "dd.MM.yyyy", null);
        }

        /// <summary>
        /// Дата установления курса (может отличаться от запрашиваемой если на запрашиваемою дату курс не устанавливался)
        /// </summary>
        [XmlIgnore]
        public DateTime Date { get; set; }

        /// <summary>
        /// Имя документа
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Список валют и их курсов
        /// </summary>
        [XmlElement("Valute")]
        public List<Valute> Valutes { get; set; }
    }
}