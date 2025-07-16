using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TestProxyCBRApi.Services;

namespace TestProxyCBRApi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddHttpClient<CurrencyService>();
            
            var assembly = Assembly.GetEntryAssembly();
            AssemblyName info = assembly.GetName();
            
            services.AddSwaggerExamples();
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly(), assembly);
            // var contractsAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name.Equals(assembly.GetName().Name + ".Contracts"));
            // if (contractsAssembly != null) services.AddSwaggerExamplesFromAssemblies(contractsAssembly);
            
            services.AddSwaggerGen(options =>
            {
                // Добавить документацию основной библиотеки
                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // Добавить документацию других библиотек
                var baseDirectory = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory));
                //var xmlsEnumerable = baseDirectory.EnumerateFiles("*.xml").Where(fi => !Path.GetFileName(fi.FullName).StartsWith("System."));
                var xmlsEnumerable = baseDirectory.EnumerateFiles("*.xml");
                foreach (var fi in xmlsEnumerable)
                    options.IncludeXmlComments(fi.FullName);
                
                options.ExampleFilters();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "An ASP.NET Core Web API for managing ToDo items"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = "swagger";
                });
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}