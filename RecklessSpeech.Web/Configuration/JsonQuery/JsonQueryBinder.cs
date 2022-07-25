using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;

namespace RecklessSpeech.Web.Configuration.JsonQuery;

internal class JsonQueryBinder : IModelBinder
{
    private readonly IObjectModelValidator validator;
    private readonly ILogger<JsonQueryBinder> logger;

    public JsonQueryBinder(ILogger<JsonQueryBinder> logger, IObjectModelValidator validator)
    {
        this.logger = logger;
        this.validator = validator;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.FieldName).FirstValue;
        if (value == null)
        {
            return Task.CompletedTask;
        }

        try
        {
            var parsed = JsonSerializer.Deserialize(
                value,
                bindingContext.ModelType,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            bindingContext.Result = ModelBindingResult.Success(parsed);

            if (parsed != null)
            {
                this.validator.Validate(
                    bindingContext.ActionContext,
                    validationState: bindingContext.ValidationState,
                    prefix: string.Empty,
                    model: parsed
                );
            }
        }
        catch (JsonException e)
        {
            this.logger.LogError(e, "Failed to bind parameter '{FieldName}'", bindingContext.FieldName);
            bindingContext.ActionContext.ModelState.TryAddModelError(
                key: e.Path ?? String.Empty,
                exception: e,
                bindingContext.ModelMetadata);
        }
        catch (Exception e) when (e is FormatException or OverflowException)
        {
            this.logger.LogError(e, "Failed to bind parameter '{FieldName}'", bindingContext.FieldName);
            bindingContext.ActionContext.ModelState.TryAddModelError(string.Empty, e, bindingContext.ModelMetadata);
        }

        return Task.CompletedTask;
    }
}