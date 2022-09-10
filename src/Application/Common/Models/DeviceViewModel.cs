using iot.Domain.Entities.Product;
using iot.Domain.Enums;

namespace iot.Application.Common.Models;

public class DeviceViewModel
{
    public Guid Id { get; set; }
    public int Pin { get; set; }
    public DeviceType DeviceType { get; set; }
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }

    public static explicit operator Device(DeviceViewModel viewModel)
    {
        return new Device(viewModel.Pin,viewModel.DeviceType,viewModel.IsHigh,viewModel.PlaceId);
    }
}
