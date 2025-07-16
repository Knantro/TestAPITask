using System.Net;

namespace TestProxyCBRApi.Common {
    
    /// <summary>
    /// Общий тип возврата для API-запросов
    /// </summary>
    /// <typeparam name="T">Тип данных ответа</typeparam>
    public class Result<T> {
        private Result() { }
        
        /// <summary>
        /// Код ответа
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        
        /// <summary>
        /// Данные ответа, если такие есть (отсутствует, если <see cref="IsFail"/> равен True)
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// Признак того, что ответ успешный
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// Признак того, что ответ провальный
        /// </summary>
        public bool IsFail => !IsSuccess;
        
        /// <summary>
        /// Сообщение об ошибке (отсутствует, если <see cref="IsSuccess"/> равен True)
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Фабричный метод, возвращающий успешное выполнение запроса
        /// </summary>
        /// <param name="data">Данные ответа</param>
        /// <param name="statusCode">Код ответа (по умолчанию 200)</param>
        /// <returns>Успешный результат выполнения запроса</returns>
        public static Result<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK) => new Result<T> {
            IsSuccess = true,
            Data = data,
            StatusCode = statusCode,
        };
            
        /// <summary>
        /// Фабричный метод, возвращающий провальное выполнение запроса
        /// </summary>
        /// <param name="errorMessage">Текст ошибки</param>
        /// <param name="statusCode">Код ответа (по умолчанию 200)</param>
        /// <returns>Провальный результат выполнения запроса</returns>
        public static Result<T> Fail(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new Result<T> {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            StatusCode = statusCode,
        };
    }
}