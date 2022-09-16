using iot.Application.Common.Interfaces;
using iot.Application.Events.ProductNotifications;

namespace iot.Application.Commands.Place.CreatePlace;

public class CreatePlaceCommand : IRequest<Result<Guid>>,ICommittableRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid StuctureId { get; set; }
}

public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, Result<Guid>>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreatePlaceCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<Result<Guid>> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == null || request.Id == Guid.Empty)
                request.Id = Guid.NewGuid();

            await _unitOfWorks.SqlRepository<Domain.Entities.Product.Place>()
                .AddAsync(new Domain.Entities.Product.Place(request.Id,request.Name,request.Description,DateTime.Now,DateTime.Now,request.StuctureId));

            await _mediator.Publish(new PlaceNotifications());

            return Result.Ok(request.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}