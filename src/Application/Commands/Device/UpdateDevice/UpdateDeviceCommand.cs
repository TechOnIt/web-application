using iot.Application.Common.Interfaces;
using iot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Commands.Device.UpdateDevice;

public class UpdateDeviceCommand : IRequest<Result<Guid>>,ICommittableRequest
{
    public Guid DeviceId { get; set; }
    public int Pin { get; set; }
    public DeviceType DeviceType { get; private set; }
    public bool IsHigh { get; set; }
}

public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, Result<Guid>>
{
    #region Constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    public UpdateDeviceCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion
    public async Task<Result<Guid>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWorks.SqlRepository<iot.Domain.Entities.Product.Device>();

        try
        {
            var findDevice = await repo.Table.FirstOrDefaultAsync(a=>a.Id==request.DeviceId);
            if (findDevice == null)
                return Result.Fail($"can not find any device with id : {request.DeviceId}");
            else
            {
                findDevice.Pin = request.Pin;
                findDevice.IsHigh = request.IsHigh;
                findDevice.SetDeviceType(request.DeviceType);

                await repo.UpdateAsync(findDevice);
                return Result.Ok(findDevice.Id);
            }
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}