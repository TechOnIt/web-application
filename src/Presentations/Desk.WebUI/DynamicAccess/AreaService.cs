using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using TechOnIt.Application.Common.Models.DynamicAccess;

namespace TechOnIt.Desk.Web.DynamicAccess;

public class AreaService
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

    public AreaService(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    public List<ControllerInfo> GetControllersAndActions()
    {
        var controllersActionsList = new List<ControllerInfo>();

        var actionDescriptors = _actionDescriptorCollectionProvider.ActionDescriptors.Items;
        foreach (var action in actionDescriptors.OfType<ControllerActionDescriptor>())
        {
            var controllerName = action.ControllerName;
            var actionName = action.ActionName;
            var httpMethod = action.MethodInfo.GetCustomAttributes<HttpMethodAttribute>()
                .SelectMany(attr => attr.HttpMethods)
                .Distinct()
                .FirstOrDefault();

            var controllerInfo = controllersActionsList.FirstOrDefault(c => c.Name == controllerName);
            if (controllerInfo == null)
            {
                controllerInfo = new ControllerInfo
                {
                    Name = controllerName,
                    Actions = new List<ActionInfo>()
                };
                controllersActionsList.Add(controllerInfo);
            }

            controllerInfo.Actions.Add(new ActionInfo
            {
                Name = actionName,
                HttpMethod = httpMethod ?? "GET" // Default to GET if no attribute is found
            });
        }

        return controllersActionsList;
    }
}