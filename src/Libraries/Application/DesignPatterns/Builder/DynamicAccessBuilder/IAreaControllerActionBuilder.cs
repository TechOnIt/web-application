using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TechOnIt.Application.DesignPatterns.Builder.DynamicAccessBuilder;

public interface IAreaControllerActionBuilder
{
    IAreaControllerActionBuilder WithProvider(IActionDescriptorCollectionProvider provider);
    List<string> Build();
}
