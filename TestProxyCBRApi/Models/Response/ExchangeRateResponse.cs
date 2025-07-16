using System;
using System.Collections.Generic;

namespace TestProxyCBRApi.Models.Response {
    
    /// <summary>
    /// Модель ответа на запрос курса валют
    /// </summary>
    public class ExchangeRateResponse {
        
        /// <summary>
        /// Дата формирования курса валют
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Курсы валют
        /// </summary>
        public List<CurrencyRateResponse> Rates { get; set; } 
        
        public ExchangeRateResponse(DateTime date, List<CurrencyRateResponse> rates) {
            Date = date;
            Rates = rates;
        }
    }
}