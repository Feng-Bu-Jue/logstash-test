using LogStashExemple.API.Infra.Swagger.Filters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace LogStashExemple.API.Infra.Swagger.Extensions
{
    public static class SwaggerServicesExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                options.SchemaFilter<ApplyIgnoreRelationshipsInNamespace<JObject>>();
                options.SchemaFilter<ApplyIgnoreRelationshipsInNamespace<JToken>>();

                options.MapType<JObject>(() => new OpenApiSchema { Type = "object" });

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = GetApplicationName(),
                        Version = description.ApiVersion.ToString()
                    });
                }

                options.SchemaFilter<CustomSchemaExcludeFilter>();
                options.DocumentFilter<LowerCaseDocumentFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        private static string GetApplicationName()
            => Assembly.GetExecutingAssembly().GetName().Name.Replace(".", " ");
    }
}