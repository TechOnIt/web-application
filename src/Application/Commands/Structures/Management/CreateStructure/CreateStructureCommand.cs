using iot.Application.Common.Interfaces;
using iot.Domain.Entities.Product;
using iot.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public CreateStructureCommandHandler(IUnitOfWorks unitOfWorks, ILogger<CreateStructureCommandHandler> logger)
    {
        _unitOfWorks = unitOfWorks;
        _logger = logger;
    }

    #endregion

    public async Task<Result<Concurrency>> Handle(CreateStructureCommand request, CancellationToken cancellationToken)
    {

        try
        {
            var newId = Guid.NewGuid();
            var structure = new Structure(newId, request.Name, request.Description, DateTime.Now, null, request.Type);

            await _unitOfWorks.SqlRepository<Structure>().AddAsync(structure);
            return Result.Ok(structure.ApiKey);
        }
        catch (Exception exp)
        {
            _logger.LogError(exp.Message);
            return Result.Fail("error during insert structure...");
        }
    }
}
