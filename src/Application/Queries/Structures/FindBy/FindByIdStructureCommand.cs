﻿using iot.Domain.Entities.Product;
using Mapster;

namespace iot.Application.Queries.Structures.FindBy;

public class FindByIdStructureCommand : IRequest<Result<StructureViewModel>>
{
    public Guid Id { get; set; }
}

public class FindByIdStructureCommandHandler : IRequestHandler<FindByIdStructureCommand, Result<StructureViewModel>>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;

    public FindByIdStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result<StructureViewModel>> Handle(FindByIdStructureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var findStruct = await _unitOfWorks.SqlRepository<Structure>().TableNoTracking.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (findStruct != null)
                return Result.Ok(findStruct.Adapt<StructureViewModel>());
            else
                return Result.Ok(new StructureViewModel());
        }
        catch (Exception exp)
        {
            return Result.Fail(exp.Message);
        }
    }
}