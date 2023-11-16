using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NaturalPersonsDirectory.Application.Common.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NaturalPersonsDirectory.API.Filters.Swagger;

public class AcceptLanguageHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter()
        {
            Name = "Accept-Language",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema { Type = "String" },
            Example = new OpenApiString(CultureName.EnglishUnitedStates)
        });
    }
}
