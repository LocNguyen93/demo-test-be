namespace Ext.Shared.Web.ModelBinders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Threading.Tasks;

    public class JObjectBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult =
                bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName,
                valueProviderResult);

            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            try {
                var obj = JObject.Parse(value);
                bindingContext.Result = ModelBindingResult.Success(obj);
            }
            catch (Exception)
            {
                bindingContext.ModelState.TryAddModelError(
                                    modelName,
                                    "Invalid json value.");
            }
            return Task.CompletedTask;
        }
    }
}
