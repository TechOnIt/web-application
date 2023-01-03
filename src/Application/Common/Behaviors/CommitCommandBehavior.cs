using MediatR.Pipeline;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Common.Behaviors;

public class CommitCommandBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    #region Constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public CommitCommandBehavior(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken = default)
    {
        if (request is ICommittableRequest)
        {
            await _unitOfWorks.SaveAsync();
        }
    }
}