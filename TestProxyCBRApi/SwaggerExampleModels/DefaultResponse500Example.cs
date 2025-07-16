using Swashbuckle.AspNetCore.Filters;

namespace TestProxyCBRApi.SwaggerExampleModels {
    public class DefaultResponse500Example : IExamplesProvider<string> {
        public string GetExamples() => "Request is unprocessable due to unexpected error. Contact the administrator for more information";
    }
}