using Microsoft.AspNetCore.Mvc.Infrastructure;
using TechOnIt.Application.Common.Models.DynamicAccess;

namespace TechOnIt.Application.DesignPatterns.Builder.DynamicAccessBuilder;

public class AreaControllerActionBuilder : IAreaControllerActionBuilder
{
    private IActionDescriptorCollectionProvider _provider;

    public IAreaControllerActionBuilder WithProvider(IActionDescriptorCollectionProvider provider)
    {
        _provider = provider;
        return this;
    }

    public List<ControllerInfo> Build()
    {
        if (_provider == null)
            throw new InvalidOperationException("Action Descriptor Collection Provider is not set.");

        var cks = new CancellationTokenSource();
        AreaService service = new AreaService(_provider);
        return service.GetAllControllerActionInfo();
    }
}
