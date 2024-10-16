using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using TechOnIt.Application.Common.Models.DynamicAccess;

namespace TechOnIt.Application.DesignPatterns.Builder.DynamicAccessBuilder;

public class AreaService
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
    public AreaService(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    public List<ControllerInfo> GetAllControllerActionInfo()
    {
        var controllerActionList = new List<ControllerInfo>();

        var items = _actionDescriptorCollectionProvider.ActionDescriptors.Items;
        foreach (var actionDescriptor in items)
        {
            if (actionDescriptor is ControllerActionDescriptor descriptor)
            {
                string areaName = descriptor.RouteValues.ContainsKey("area") ? descriptor.RouteValues["area"] : string.Empty;
                string controllerName = descriptor.ControllerName;
                string actionName = descriptor.ActionName;

                // Considering DisplayNameAttribute for Controller and Action
                var controllerDisplayName = descriptor.ControllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
                var actionDisplayName = descriptor.MethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;

                controllerName = controllerDisplayName ?? controllerName;
                actionName = actionDisplayName ?? actionName;

                // Find existing or create new
                var existingControllerInfo = controllerActionList
                    .FirstOrDefault(c => c.Controller == controllerName && c.Area == areaName);

                if (existingControllerInfo != null)
                {
                    if (!existingControllerInfo.Actions.Contains(actionName))
                    {
                        existingControllerInfo.Actions.Add(actionName);
                    }
                }
                else
                {
                    controllerActionList.Add(new ControllerInfo
                    {
                        Area = areaName,
                        Controller = controllerName,
                        Actions = new List<string> { actionName }
                    });
                }
            }
        }

        return controllerActionList;
    }
}