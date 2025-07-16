using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using TestProxyCBRApi.Common;
using TestProxyCBRApi.Models.Response;
using TestProxyCBRApi.Services;
using TestProxyCBRApi.SwaggerExampleModels;

namespace TestProxyCBRApi.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase {
        private readonly ILogger<CurrencyController> logger;
        private readonly CurrencyService currencyService;

        public CurrencyController(ILogger<CurrencyController> logger, CurrencyService currencyService) {
            this.logger = logger;
            this.currencyService = currencyService;
        }

        /// [GET] /api/currency/rate
        /// <summary>
        /// Получение информации о курсе валют (или валюты) за определённую дату
        /// </summary>
        /// <remarks>
        /// Если не указана дата, то берётся сегодняшняя. Если не указан код валюты, то возвращается курс всех валют
        /// </remarks>
        /// <param name="exchangeDate" example="2022-01-03">Дата формирования курса валют</param>
        /// <param name="currencyCode" example="978">Код валюты (можно использовать как буквенное, так и числовое значение)</param>
        /// <response code="200">Ошибок нет</response>
        /// <response code="204">Курс валют не найден</response>
        /// <response code="400">Модель не передана</response>
        /// <response code="500">Непредвиденная серверная ошибка</response>
        [HttpGet]
        [Route("rate")]
        [Consumes(System.Net.Mime.MediaTypeNames.Application.Json), Produces(System.Net.Mime.MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ExchangeRateResponse), StatusCodes.Status200OK), SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetExchangeRateResponse200Example))]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError), SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(DefaultResponse500Example))]
        public async Task<IActionResult> GetExchangeRate([FromQuery] DateTime? exchangeDate, [FromQuery] string currencyCode) {
            var result = await currencyService.GetRatesAsync(exchangeDate ?? DateTime.Now, currencyCode);
            return result.IsSuccess ? StatusCode((int)result.StatusCode, result.Data) : StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }
}