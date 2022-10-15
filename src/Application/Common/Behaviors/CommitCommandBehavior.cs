using iot.Application.Common.Interfaces;
using iot.Infrastructure.Repositories.UnitOfWorks;
using MediatR;
using MediatR.Pipeline;

namespace iot.Application.Common.Behaviors;

public class CommitCommandBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
{
    #region Constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public CommitCommandBehavior(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        if (request is ICommittableRequest)
        {
            await _unitOfWorks.SaveAsync();
        }
    }
}