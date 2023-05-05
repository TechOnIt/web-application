using Shouldly;
using System;
using TechOnIt.Domain.Enums;

namespace TechOnIt.Domain.UnitTests.Entities;

public class DeviceTests
{
    [Fact]
    public void Should_Set_DeviceType_By_DeviceType()
    {
        // arrange
        var device = new Device(3, DeviceType.Fan, Guid.Empty);

        // act
        device.SetDeviceType(DeviceType.Cooler);

        // assert
        device.Type.ShouldNotBeNull();
        device.Type.DisplayName.ShouldBe("Cooler");
    }

    [Fact]
    public void Should_Returns_DeviceType_When_DeviceType_Not_Null()
    {
        // arrange
        var device = new Device(63, DeviceType.Heater, Guid.NewGuid());

        // act
        device.SetDeviceType(DeviceType.Light);

        // assert
        device.Type.ShouldNotBeNull();
    }
}
