using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Common.WebToolkit
{
    public static class ControllerExtensions
    {
        public static string ShortControllerName<T>() where T : Controller
        {
            return typeof(T).Name.Replace("Controller", "");
        }

        public static string ShortControllerName<T>(this T controller) where T : Controller
        {
            return nameof(controller).Replace("Controller", "");
        }
    }
}