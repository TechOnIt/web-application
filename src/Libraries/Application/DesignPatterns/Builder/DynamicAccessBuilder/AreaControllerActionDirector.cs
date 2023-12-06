using Microsoft.AspNetCore.Mvc.Infrastructure;
using TechOnIt.Application.Common.Models.DynamicAccess;

namespace TechOnIt.Application.DesignPatterns.Builder.DynamicAccessBuilder;

public class AreaControllerActionDirector
{
    private readonly IAreaControllerActionBuilder _builder;

    public AreaControllerActionDirector(IAreaControllerActionBuilder builder)
    {
        _builder = builder;
    }

    public List<string> Construct(IActionDescriptorCollectionProvider provider)
    {
        return _builder.WithProvider(provider).Build();
    }
}
