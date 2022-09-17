using iot.Application.Common.Interfaces;
using iot.Domain.Entities.Product.StructureAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Commands.Structures.Management.DeleteStructure;

public class DeleteStructureCommand : IRequest<Result> , ICommittableRequest
{
    public Guid StructureId { get; set; }
}

public class DeleteStructureCommandHandler : IRequestHandler<DeleteStructureCommand, Result>
{
    #region Structure
    private readonly IUnitOfWorks _unitOfWorks;
    public DeleteStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<Result> Handle(DeleteStructureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var findStructure = await _unitOfWorks.SqlRepository<Structure>().Table.FirstOrDefaultAsync(a => a.Id == request.StructureId);
            if (findStructure != null)
            {
                await _unitOfWorks.SqlRepository<Structure>().DeleteAsync(findStructure,cancellationToken);
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Structure not found !");
            }
        }
        catch (Exception exp)
        {
            return Result.Fail($"error ! : {exp.Message}");
        }
    }
}