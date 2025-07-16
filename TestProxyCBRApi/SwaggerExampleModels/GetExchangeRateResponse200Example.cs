using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Filters;
using TestProxyCBRApi.Models.Response;

namespace TestProxyCBRApi.SwaggerExampleModels {
    public class GetExchangeRateResponse200Example : IExamplesProvider<ExchangeRateResponse> {
        public ExchangeRateResponse GetExamples() => new ExchangeRateResponse(
            DateTime.Now,
            new List<CurrencyRateResponse> {
                new CurrencyRateResponse("EUR", "Евро", "93.9821"),
                new CurrencyRateResponse("USD", "Доллары США", "77.3912"),
            });
    }
}