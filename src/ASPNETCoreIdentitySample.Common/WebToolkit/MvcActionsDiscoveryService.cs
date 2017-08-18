using ASPNETCoreIdentitySample.Common.GuardToolkit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System;

namespace ASPNETCoreIdentitySample.Common.WebToolkit
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2573
    /// </summary>
    public interface IMvcActionsDiscoveryService
    {
        ICollection<MvcControllerViewModel> MvcControllers { get; }
        ICollection<MvcControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName);
    }

    public static class MvcActionsDiscoveryServiceExtensions
    {
        public static IServiceCollection AddMvcActionsDiscoveryService(this IServiceCollection services)
        {
            services.AddSingleton<IMvcActionsDiscoveryService, MvcActionsDiscoveryService>();
            return services;
        }
    }

    public class MvcActionsDiscoveryService : IMvcActionsDiscoveryService
    {
        // 'GetOrAdd' call on the dictionary is not thread safe and we might end up creating the GetterInfo more than
        // once. To prevent this Lazy<> is used. In the worst case multiple Lazy<> objects are created for multiple
        // threads but only one of the objects succeeds in creating a GetterInfo.
        private readonly ConcurrentDictionary<string, Lazy<ICollection<MvcControllerViewModel>>> _allSecuredActionsWithPloicy =
            new ConcurrentDictionary<string, Lazy<ICollection<MvcControllerViewModel>>>();

        public MvcActionsDiscoveryService(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            actionDescriptorCollectionProvider.CheckArgumentIsNull(nameof(actionDescriptorCollectionProvider));

            MvcControllers = new List<MvcControllerViewModel>();

            var lastControllerName = string.Empty;
            MvcControllerViewModel currentController = null;

            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach (var actionDescriptor in actionDescriptors)
            {
                var descriptor = actionDescriptor as ControllerActionDescriptor;
                if (descriptor == null)
                {
                    continue;
                }

                var controllerTypeInfo = descriptor.ControllerTypeInfo;
                var actionMethodInfo = descriptor.MethodInfo;

                if (lastControllerName != descriptor.ControllerName)
                {
                    currentController = new MvcControllerViewModel
                    {
                        AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                        ControllerAttributes = getAttributes(controllerTypeInfo),
                        ControllerDisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                        ControllerName = descriptor.ControllerName,
                    };
                    MvcControllers.Add(currentController);

                    lastControllerName = descriptor.ControllerName;
                }

                currentController?.MvcActions.Add(new MvcActionViewModel
                {
                    ControllerId = currentController.ControllerId,
                    ActionName = descriptor.ActionName,
                    ActionDisplayName = actionMethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    ActionAttributes = getAttributes(actionMethodInfo),
                    IsSecuredAction = isSecuredAction(controllerTypeInfo, actionMethodInfo)
                });
            }
        }

        public ICollection<MvcControllerViewModel> MvcControllers { get; }

        public ICollection<MvcControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName)
        {
            var getter = _allSecuredActionsWithPloicy.GetOrAdd(policyName, y => new Lazy<ICollection<MvcControllerViewModel>>(
                () =>
                {
                    var controllers = new List<MvcControllerViewModel>(MvcControllers);
                    foreach (var controller in controllers)
                    {
                        controller.MvcActions = controller.MvcActions.Where(
                            model => model.IsSecuredAction &&
                            (
                            model.ActionAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName ||
                            controller.ControllerAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName
                            )).ToList();
                    }
                    return controllers.Where(model => model.MvcActions.Any()).ToList();
                }));
            return getter.Value;
        }

        private static List<Attribute> getAttributes(MemberInfo actionMethodInfo)
        {
            return actionMethodInfo.GetCustomAttributes(inherit: true)
                                   .Where(attribute =>
                                    {
                                        var attributeNamespace = attribute.GetType().Namespace;
                                        return attributeNamespace != typeof(CompilerGeneratedAttribute).Namespace &&
                                               attributeNamespace != typeof(DebuggerStepThroughAttribute).Namespace;
                                    })
                                    .Cast<Attribute>()
                                   .ToList();
        }

        private static bool isSecuredAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
        {
            var actionHasAllowAnonymousAttribute = actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(inherit: true) != null;
            if (actionHasAllowAnonymousAttribute)
            {
                return false;
            }

            var controllerHasAuthorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;
            if (controllerHasAuthorizeAttribute)
            {
                return true;
            }

            var actionMethodHasAuthorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;
            if (actionMethodHasAuthorizeAttribute)
            {
                return true;
            }

            return false;
        }
    }

    public class MvcActionViewModel
    {
        public IList<Attribute> ActionAttributes { get; set; }
        public string ActionDisplayName { get; set; }
        public string ActionId => $"{ControllerId}:{ActionName}";
        public string ActionName { get; set; }
        public string ControllerId { get; set; }
        public bool IsSecuredAction { get; set; }

        public override string ToString()
        {
            const string attribute = "Attribute";
            var actionAttributes = string.Join(",", ActionAttributes.Select(a => a.GetType().Name.Replace(attribute, "")));
            return $"[{actionAttributes}]{ActionName}";
        }
    }

    public class MvcControllerViewModel
    {
        public string AreaName { get; set; }
        public IList<Attribute> ControllerAttributes { get; set; }
        public string ControllerDisplayName { get; set; }
        public string ControllerId => $"{AreaName}:{ControllerName}";
        public string ControllerName { get; set; }
        public IList<MvcActionViewModel> MvcActions { get; set; } = new List<MvcActionViewModel>();

        public override string ToString()
        {
            const string attribute = "Attribute";
            var controllerAttributes = string.Join(",", ControllerAttributes.Select(a => a.GetType().Name.Replace(attribute, "")));
            return $"[{controllerAttributes}]{AreaName}.{ControllerName}";
        }
    }
}