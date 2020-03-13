using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace LogStashExemple.API.Infra.Swagger.Filters
{
    public class CustomSchemaExcludeFilter : ISchemaFilter
    {
        private static readonly List<string> ExcludeProperties = new List<string> { "layer", "applicationName" };

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null) { return; }

            ExcludeProperties.ForEach(prop =>
            {
                if (schema.Properties.ContainsKey(prop))
                {
                    schema.Properties.Remove(prop);
                }
            });
        }
    }
}