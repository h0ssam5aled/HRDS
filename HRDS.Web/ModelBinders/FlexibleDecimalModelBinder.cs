using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace HRDS.Web.ModelBinders;

public class FlexibleDecimalModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult =
            bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrWhiteSpace(value))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        // أولاً نحاول InvariantCulture
        if (decimal.TryParse(
            value,
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out var invariantValue))
        {
            bindingContext.Result =
                ModelBindingResult.Success(invariantValue);

            return Task.CompletedTask;
        }

        // ثم نحاول Culture الحالية
        if (decimal.TryParse(
            value,
            NumberStyles.Number,
            CultureInfo.CurrentCulture,
            out var currentCultureValue))
        {
            bindingContext.Result =
                ModelBindingResult.Success(currentCultureValue);

            return Task.CompletedTask;
        }

        bindingContext.ModelState.AddModelError(
            bindingContext.ModelName,
            $"القيمة '{value}' غير صحيحة."
        );

        return Task.CompletedTask;
    }
}