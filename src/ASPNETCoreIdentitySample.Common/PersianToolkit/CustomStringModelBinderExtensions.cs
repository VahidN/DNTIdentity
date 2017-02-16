using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ASPNETCoreIdentitySample.Common.PersianToolkit
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2583
    /// </summary>
    public class CustomStringModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.IsComplexType)
            {
                return null;
            }

            var fallbackBinder = new SimpleTypeModelBinder(context.Metadata.ModelType);
            if (context.Metadata.ModelType == typeof(string))
            {
                return new CustomStringModelBinder(fallbackBinder);
            }
            return fallbackBinder;
        }
    }

    public class CustomStringModelBinder : IModelBinder
    {
        private readonly IModelBinder _fallbackBinder;
        public CustomStringModelBinder(IModelBinder fallbackBinder)
        {
            if (fallbackBinder == null)
            {
                throw new ArgumentNullException(nameof(fallbackBinder));
            }
            _fallbackBinder = fallbackBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                var valueAsString = valueProviderResult.FirstValue;
                if (string.IsNullOrWhiteSpace(valueAsString))
                {
                    return _fallbackBinder.BindModelAsync(bindingContext);
                }

                var model = valueAsString.ApplyCorrectYeKe();
                bindingContext.Result = ModelBindingResult.Success(model);
                return Task.CompletedTask;
            }

            return _fallbackBinder.BindModelAsync(bindingContext);
        }
    }

    public static class CustomStringModelBinderExtensions
    {
        /// <summary>
        /// یکدست کردن «ی» و «ک» در برنامه
        /// </summary>
        public static MvcOptions UseCustomStringModelBinder(this MvcOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var simpleTypeModelBinder = options.ModelBinderProviders.FirstOrDefault(x => x.GetType() == typeof(SimpleTypeModelBinderProvider));
            if (simpleTypeModelBinder == null)
            {
                return options;
            }

            var simpleTypeModelBinderIndex = options.ModelBinderProviders.IndexOf(simpleTypeModelBinder);
            options.ModelBinderProviders.Insert(simpleTypeModelBinderIndex, new CustomStringModelBinderProvider());
            return options;
        }
    }
}