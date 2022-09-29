using iot.Application.Common.Interfaces;
using iot.Domain.Entities.Product.StructureAggregate;
using iot.Domain.Enums;
using Mapster;

namespace iot.Application.Commands.Structures.Management.UpdateStructure;

public class UpdateStructureCommand : IRequest<Result> , ICommittableRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public StuctureType Type { get; set; }
}

public class UpdatetructureCommandHandler : IRequestHandler<UpdateStructureCommand, Result>
{
    #region Constructure
    private readonly IUnitOfWorks _unitOfWorks;
    public UpdatetructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result> Handle(UpdateStructureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var findStructure = await _unitOfWorks.StructureRepository.GetStructureByIdAsyncAsNoTracking(request.Id,cancellationToken);
            if (findStructure == null)
                return Result.Fail("Structure not found !");
            else
            {
                var model = request.Adapt<Structure>();
                await _unitOfWorks.StructureRepository.UpdateStructureAsync(model,cancellationToken);
                return Result.Ok();
            }
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}
