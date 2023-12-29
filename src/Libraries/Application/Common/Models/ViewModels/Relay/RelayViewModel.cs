using TechOnIt.Domain.Entities;
using TechOnIt.Domain.Enums;

namespace TechOnIt.Application.Common.Models.ViewModels.Relay;

public class RelayViewModel
{
    #region Ctors
    public RelayViewModel()
    {
    }

    public RelayViewModel(Guid id, Guid placeId, int pin, RelayType relayType, bool isHight)
    {
        Id = id;
        PlaceId = placeId;
        Pin = pin;
        RelayType = relayType;
        IsHigh = isHight;
    }
    #endregion

    public Guid Id { get; set; }
    public int Pin { get; set; }
    public RelayType RelayType { get; set; } = RelayType.Light;
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }

    #region explicit casting
    public static explicit operator RelayEntity(RelayViewModel viewModel)
    {
        return new RelayEntity(viewModel.Pin, viewModel.RelayType, viewModel.PlaceId);
    }
    #endregion
}
