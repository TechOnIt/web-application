using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TechOnIt.Application.DesignPatterns.Builder.DynamicAccessBuilder;

public class AreaControllerActionBuilder : IAreaControllerActionBuilder
{
    private IActionDescriptorCollectionProvider _provider;

    public IAreaControllerActionBuilder WithProvider(IActionDescriptorCollectionProvider provider)
    {
        _provider = provider;
        return this;
    }

    public List<string> Build()
    {
        if (_provider == null)
            throw new InvalidOperationException("Action Descriptor Collection Provider is not set.");

        var cks = new CancellationTokenSource();
        AreaService service = new AreaService(_provider);
        return service.GetAreaWithControllersWithActionsAsync(cks.Token);
    }


}
