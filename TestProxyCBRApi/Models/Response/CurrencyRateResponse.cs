namespace TestProxyCBRApi.Models.Response {
    
    /// <summary>
    /// Модель ответа курса валюты
    /// </summary>
    public class CurrencyRateResponse {
        
        /// <summary>
        /// Код валюты
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Название валюты
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Курс за единицу валюты
        /// </summary>
        public string Rate { get; set; }

        public CurrencyRateResponse(string currencyCode, string name, string rate) {
            CurrencyCode = currencyCode;
            Name = name;
            Rate = rate;
        }
    }
}