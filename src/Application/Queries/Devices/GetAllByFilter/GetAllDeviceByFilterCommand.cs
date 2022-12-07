﻿using TechOnIt.Domain.Entities.Product;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;
using Mapster;
using System.Linq.Expressions;
using TechOnIt.Application.Common.Models.ViewModels.Devices;

namespace TechOnIt.Application.Queries.Devices.GetAllByFilter;

public class GetAllDeviceByFilterCommand : IRequest<Result<IList<DeviceViewModel>>>
{
    public Expression<Func<Device, bool>>? Filter { get; set; }
    public Func<IQueryable, IOrderedQueryable<Device>>? Ordered { get; set; }
    public Expression<Func<Device, object>>[]? IncludesTo { get; set; }
}

public class GetAllDeviceByFilterCommandHandler : IRequestHandler<GetAllDeviceByFilterCommand, Result<IList<DeviceViewModel>>>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorksl;
    public GetAllDeviceByFilterCommandHandler(IUnitOfWorks unitOfWorksl)
    {
        _unitOfWorksl = unitOfWorksl;
    }
    #endregion

    public async Task<Result<IList<DeviceViewModel>>> Handle(GetAllDeviceByFilterCommand request, CancellationToken cancellationToken = default)
    {
        var allDevices = _unitOfWorksl.SqlRepository<Device>().TableNoTracking.AsQueryable();
        if (allDevices.Any())
        {
            if (request.IncludesTo != null)
            {
                foreach (var item in request.IncludesTo)
                {
                    allDevices = allDevices.Include(item);
                }
            }

            if (request.Filter != null)
            {
                allDevices = allDevices.Where(request.Filter);
            }

            if (request.Ordered != null)
            {
                allDevices = request.Ordered(allDevices);
            }
        }

        var exutedQuery = await allDevices.ToListAsync();
        return Result.Ok(exutedQuery.Adapt<IList<DeviceViewModel>>());
    }
}