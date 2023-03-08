using Ddd.Application.Dtos;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace multi_threadings.Routes;

public class OfficeSchemaFilter : ISchemaFilter {
    public void Apply(OpenApiSchema schema, SchemaFilterContext context) {
        if (context.Type == typeof(OfficeCreateDto)) {
            schema.Example = new OpenApiObject() {
                ["OfficeCode"] = new OpenApiString("8"),
                ["City"] = new OpenApiString("HCMC"),
                ["Phone"] = new OpenApiString("0000"),
                ["AddressLine1"] = new OpenApiString("asd"),
                ["AddressLine2"] = new OpenApiString("asd"),
                ["State"] = new OpenApiString("DD"),
                ["Country"] = new OpenApiString("CC"),
                ["PostalCode"] = new OpenApiString("AA"),
                ["Territory"] = new OpenApiString("WW")
            };
        }
    }
}
