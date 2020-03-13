using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace LogStashExemple.API.Infra.Swagger.Filters
{
    public class ApplyIgnoreRelationshipsInNamespace<T> : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null || !context.Type.Namespace.Equals(typeof(T).Namespace))
                return;

            context.Type.GetProperties()
                .Where(prop => prop.PropertyType.Namespace == typeof(T).Namespace)
                .Select(prop => ToCamelCase(prop.Name))
                .ToList().ForEach(prop =>
                {
                    if (schema.Properties.ContainsKey(prop))
                        schema.Properties.Remove(prop);
                });
        }

        public static string ToCamelCase(string str) => string.IsNullOrEmpty(str) || str.Length < 2 ? str : char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}