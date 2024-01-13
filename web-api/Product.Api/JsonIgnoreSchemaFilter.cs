using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Product.Api
{
    public class JsonIgnoreSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            var ignoredProperties = context.Type.GetProperties()
                .Where(prop => prop.IsDefined(typeof(JsonIgnoreAttribute), true))
                .Select(prop =>
                {
                    var jsonPropertyAttribute = prop.GetCustomAttribute<JsonPropertyAttribute>();
                    return jsonPropertyAttribute != null ? jsonPropertyAttribute.PropertyName : prop.Name;
                });

            foreach (var ignoredProperty in ignoredProperties)
            {
                if (model.Properties.ContainsKey(ignoredProperty))
                    model.Properties.Remove(ignoredProperty);
            }
        }
    }
}
