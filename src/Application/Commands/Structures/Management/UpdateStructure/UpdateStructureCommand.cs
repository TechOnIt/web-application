using iot.Application.Common.Interfaces;
using iot.Domain.Entities.Product;
using iot.Domain.Enums;

namespace iot.Application.Commands.Structures.Management.UpdateStructure;

public class UpdateStructureCommand : IRequest<Result> , ICommittableRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
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
        var repo = _unitOfWorks.SqlRepository<Structure>();

        try
        {
            var findStructure = await repo.Table.FirstOrDefaultAsync(a=>a.Id==request.Id);
            if (findStructure == null)
                return Result.Fail("Structure not found !");
            else
            {
                findStructure.Name = request.Name;
                findStructure.SetStructureType(request.Type);
                findStructure.Description = request.Description;
                findStructure.IsActive = request.IsActive;
                findStructure.ModifyDate = DateTime.Now;

                await repo.UpdateAsync(findStructure);
                return Result.Ok();
            }
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}
