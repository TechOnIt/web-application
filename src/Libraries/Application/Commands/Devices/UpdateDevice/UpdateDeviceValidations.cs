﻿using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Devices.UpdateDevice;

public class UpdateDeviceValidations : BaseFluentValidator<UpdateDeviceCommand>
{
    public UpdateDeviceValidations()
    {
        RuleFor(a => a.DeviceId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            ;

        RuleFor(a => a.DeviceType)
            .NotEmpty()
            .NotNull()
            ;

        RuleFor(a => a.Pin)
            .NotNull()
            .NotEqual(0)
            ;

    }
}