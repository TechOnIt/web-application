using iot.Application.Common.Interfaces;
using iot.Domain.Enums;

namespace iot.Application.Commands.Device.CreateDevice;

public class CreateDeviceCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public int Pin { get; set; }
    public DeviceType DeviceType { get; private set; }
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }
}

public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, Result<Guid>>
{
    #region Constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreateDeviceCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }


    #endregion

    public async Task<Result<Guid>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWorks.SqlRepository<Domain.Entities.Product.Device>();

        try
        {
            var model = new Domain.Entities.Product.Device(request.Pin, request.DeviceType, request.IsHigh, request.PlaceId);
            await repo.AddAsync(model);


            return Result.Ok(model.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}