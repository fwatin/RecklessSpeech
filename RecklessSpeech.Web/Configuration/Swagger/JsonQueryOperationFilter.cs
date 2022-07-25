using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.OpenApi.Models;
using RecklessSpeech.Web.Configuration.JsonQuery;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RecklessSpeech.Web.Configuration.Swagger;

internal class JsonQueryOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var jsonQueryParams = context.ApiDescription.ActionDescriptor.Parameters
            .Where(ad => ad.BindingInfo?.BinderType == typeof(JsonQueryBinder))
            .Select(ad => ad.Name)
            .ToList();

        if (!jsonQueryParams.Any())
        {
            return;
        }

        foreach (var p in operation.Parameters.Where(p => jsonQueryParams.Contains(p.Name)))
        {
            p.Content = new Dictionary<string, OpenApiMediaType>
            {
                [MediaTypeNames.Application.Json] = new()
                {
                    Schema = p.Schema
                }
            };
            p.Schema = null;
        }
    }
}