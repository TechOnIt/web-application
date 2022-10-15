﻿using iot.Application.Common.Interfaces;
using iot.Application.Events.ProductNotifications;
using iot.Domain.Entities.Product.StructureAggregate;
using iot.Domain.Enums;
using iot.Infrastructure.Repositories.UnitOfWorks;
using Microsoft.Extensions.Logging;

namespace iot.Application.Commands.Structures.Management.CreateStructure;

public class CreateStructureCommand : IRequest<Result<Concurrency>>, ICommittableRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public StuctureType Type { get; private set; }
}

public class CreateStructureCommandHandler : IRequestHandler<CreateStructureCommand, Result<Concurrency>>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly ILogger<CreateStructureCommandHandler> _logger;
    private readonly IMediator _mediator;

    public CreateStructureCommandHandler(IUnitOfWorks unitOfWorks, ILogger<CreateStructureCommandHandler> logger, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _logger = logger;
        _mediator = mediator;
    }

    #endregion

    public async Task<Result<Concurrency>> Handle(CreateStructureCommand request, CancellationToken cancellationToken)
    {

        try
        {
            var newId = Guid.NewGuid();
            var structure = new Structure(newId, request.Name, request.Description, DateTime.Now, null, request.Type);
            
            await _unitOfWorks.StructureRepository.CreateStructureAsync(structure,cancellationToken);
            await _mediator.Publish(new StructureNotifications(), cancellationToken);
            return Result.Ok(structure.ApiKey);
        }
        catch (Exception exp)
        {
            _logger.LogError(exp.Message);
            return Result.Fail("error during insert structure...");
        }
    }
}
