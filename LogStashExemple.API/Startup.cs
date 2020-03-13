using LogStashExemple.API.Infra.Configurations.Extensions.Applications;
using LogStashExemple.API.Infra.Configurations.Extensions.Services;
using LogStashExemple.API.Infra.Swagger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogStashExemple.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<ILogger>(provider => provider.GetService<ILogger<Startup>>())
                .AddApiConfigurations()
                .AddVersioning()
                .AddSwaggerDocumentation()
                .HealthChecksConfiguration(_configuration)
                .GlobalizationConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionProvider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app
                .UseRouting()
                //.UseSimpleLogRequest()
                .UseResponseCompression()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .HealthCheckConfiguration()
                .UseVersionedSwagger(versionProvider);
        }
    }
}