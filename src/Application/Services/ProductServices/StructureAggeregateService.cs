using TechOnIt.Application.Common.Models.ViewModels.Places;
using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Domain.Entities.Product.StructureAggregate;

namespace TechOnIt.Application.Services.ProductServices;

public class StructureAggeregateService : IStructureAggeregateService
{
	#region constructor
	private readonly IUnitOfWorks _unitOfWorks;
	public StructureAggeregateService(IUnitOfWorks unitOfWorks)
	{
		_unitOfWorks = unitOfWorks;
	}

    #endregion

    #region constructor
    public async Task<Concurrency?> CreateStructureAsync(StructureViewModel viewModel,CancellationToken cancellationToken)
    {
        var model = new Structure(viewModel.Id,viewModel.Name,viewModel.Description,viewModel.CreateDate,viewModel.ModifyDate,viewModel.Type);
        await _unitOfWorks.StructureRepository.CreateAsync(model,cancellationToken);
        return await Task.FromResult(model.ApiKey);
    }
    #endregion

    #region place
    public async Task<Guid?> UpdatePlaceAsync(Guid structorId, PlaceUpdateViewModel place, CancellationToken cancellationToken)
	{
		var getPlace=await _unitOfWorks.StructureRepository.GetPlaceByStructureIdAsync(structorId,place.Id,cancellationToken);
		if (getPlace is null) return await Task.FromResult<Guid?>(null);

        getPlace.StuctureId = place.StructureId;
        getPlace.Name = place.Name;
        getPlace.Description = place.Description;
        getPlace.SetModifyDate();

        await _unitOfWorks.StructureRepository.UpdatePlaceAsync(structorId, getPlace, cancellationToken);
        return await Task.FromResult(getPlace.Id);
    }
    public async Task<Guid?> CreatePlaceAsync(Guid structureId, PlaceCreateViewModel place, CancellationToken cancellationToken)
    {
        if (!await _unitOfWorks.StructureRepository.IsExistsStructoreByIdAsync(structureId, cancellationToken))
            return await Task.FromResult<Guid?>(null);

        var model = place.Adapt<Place>();
        model.Id = model.Id == null || model.Id == Guid.Empty ? Guid.NewGuid() : model.Id;
        model.SetCreateDate();
        model.SetModifyDate();

        await _unitOfWorks.StructureRepository.CreatePlaceAsync(model,structureId,cancellationToken);
        return await Task.FromResult(model.Id);
    }
    public async Task<bool?> DeletePlaceByStructureIdAsync(Guid structureId, Guid placeId, CancellationToken cancellationToken)
    {
        var getPlace = await _unitOfWorks.StructureRepository.GetPlaceByStructureIdAsync(structureId, placeId, cancellationToken);
        if (getPlace is null) return await Task.FromResult<bool?>(null);

        await _unitOfWorks.StructureRepository.DeletePlaceAsync(getPlace, cancellationToken);
        return await Task.FromResult(true);
    }
    #endregion
}
