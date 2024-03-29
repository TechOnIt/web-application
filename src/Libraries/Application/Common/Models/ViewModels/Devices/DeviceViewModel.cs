﻿using TechOnIt.Domain.Entities;
using TechOnIt.Domain.Enums;

namespace TechOnIt.Application.Common.Models.ViewModels.Devices;

public class DeviceViewModel
{
    #region Ctors
    public DeviceViewModel()
    {
    }

    public DeviceViewModel(Guid id, Guid placeId, int pin, DeviceType deviceType, bool isHight)
    {
        Id = id;
        PlaceId = placeId;
        Pin = pin;
        DeviceType = deviceType;
        IsHigh = isHight;
    }
    #endregion

    public Guid Id { get; set; }
    public int Pin { get; set; }
    public DeviceType DeviceType { get; set; } = DeviceType.Light;
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }

    #region explicit casting
    public static explicit operator Device(DeviceViewModel viewModel)
    {
        return new Device(viewModel.Pin, viewModel.DeviceType, viewModel.PlaceId);
    }
    #endregion
}
