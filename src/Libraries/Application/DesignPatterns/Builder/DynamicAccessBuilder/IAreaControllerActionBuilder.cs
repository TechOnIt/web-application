using Microsoft.AspNetCore.Mvc.Infrastructure;
using TechOnIt.Application.Common.Models.DynamicAccess;

namespace TechOnIt.Application.DesignPatterns.Builder.DynamicAccessBuilder;

public interface IAreaControllerActionBuilder
{
    IAreaControllerActionBuilder WithProvider(IActionDescriptorCollectionProvider provider);
    List<ControllerInfo> Build();
}
