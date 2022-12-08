using TechOnIt.Application.Common.Models.ViewModels.Places;
using TechOnIt.Application.Common.Models.ViewModels.Structures;

namespace TechOnIt.Application.Services.ProductServices.ProductContracts;

public interface IStructureAggeregateService
{
    #region structor
    Task<Concurrency?> CreateStructureAsync(StructureViewModel viewModel, CancellationToken cancellationToken);
    #endregion

    #region place
    Task<Guid?> UpdatePlaceAsync(Guid structorId, PlaceUpdateViewModel place, CancellationToken cancellationToken);
    Task<Guid?> CreatePlaceAsync(Guid structureId, PlaceCreateViewModel place, CancellationToken cancellationToken);
    Task<bool?> DeletePlaceByStructureIdAsync(Guid structureId, Guid placeId, CancellationToken cancellationToken);
    #endregion
}