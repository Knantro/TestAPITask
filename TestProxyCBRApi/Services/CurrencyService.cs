using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.WebUtilities;
using TestProxyCBRApi.Common;
using TestProxyCBRApi.Models;
using TestProxyCBRApi.Models.Remote;
using TestProxyCBRApi.Models.Response;

namespace TestProxyCBRApi.Services {
    public class CurrencyService {
        private readonly HttpClient httpClient;
        private readonly ILogger<CurrencyService> logger;

        public CurrencyService(ILogger<CurrencyService> logger, HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
        }
        
        /// <summary>
        /// Возвращает с сервиса банка России (CBR) информацию о курсе валют по дате и коду валюты
        /// </summary>
        /// <param name="date">Дата курса</param>
        /// <param name="currency">Код валюты (буквенный или числовой)</param>
        /// <returns>Курс валют</returns>
        public async Task<Result<ExchangeRateResponse>> GetRatesAsync(DateTime date, string currency) {
            try {
                var url = QueryHelpers.AddQueryString(CBRURL.EXCHANGE_RATE, new Dictionary<string, string> {
                    { CBRURL.EXCHANGE_RATE_DATE_REQ, date.ToString("dd'/'MM'/'yyyy") }
                });
                
                var req = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(req);
                response.EnsureSuccessStatusCode();

                var xmlSerializer = new XmlSerializer(typeof(ValCurs));
                var valCurs = (ValCurs)xmlSerializer.Deserialize(await response.Content.ReadAsStreamAsync());

                if (valCurs?.Valutes == null || valCurs.Valutes.Count == 0)
                    return Result<ExchangeRateResponse>.Success(null, HttpStatusCode.NoContent);
                
                var rates = new List<CurrencyRateResponse>();
                if (!string.IsNullOrEmpty(currency)) {
                    var valute = ushort.TryParse(currency, out var currencyCode)
                        ? valCurs.Valutes.FirstOrDefault(x => x.NumCode == currencyCode)
                        : valCurs.Valutes.FirstOrDefault(x => x.CharCode == currency.ToUpper());

                    if (valute == null) return Result<ExchangeRateResponse>.Success(null, HttpStatusCode.NoContent);
                    rates.Add(new CurrencyRateResponse(valute.CharCode, valute.Name, valute.VunitRate));
                }
                else rates.AddRange(valCurs.Valutes.Select(x => new CurrencyRateResponse(x.CharCode, x.Name, x.VunitRate)));

                return Result<ExchangeRateResponse>.Success(new ExchangeRateResponse(valCurs.Date, rates));
            }
            catch (HttpRequestException e) {
                logger.LogWarning(e, e.Message);
                return Result<ExchangeRateResponse>.Fail("Server is unavailable", HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception e) {
                logger.LogError(e, e.Message);
                return Result<ExchangeRateResponse>.Fail("Request is unprocessable due to unexpected error. Contact the administrator for more information", HttpStatusCode.InternalServerError);
            }
        }
    }
}