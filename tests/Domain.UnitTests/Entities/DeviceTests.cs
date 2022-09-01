using iot.Domain.Entities.Product;
using Shouldly;

namespace iot.Domain.UnitTests.Entities;

public class DeviceTests
{
    [Fact]
    public void Should_Set_DeviceType_By_DeviceType_Name()
    {
        // arrange
        var device = new Device();

        // act
        device.SetDeviceType("Thermometer");

        // assert
        device.DeviceType.ShouldNotBeNull();
        device.DeviceType.DisplayName.ShouldBe("Thermometer");
    }

    [Fact]
    public void Should_Set_DeviceType_By_DeviceType_Value()
    {
        // arrange
        var device = new Device();

        // act
        device.SetDeviceType(1);

        // assert
        device.DeviceType.ShouldNotBeNull();
        device.DeviceType.DisplayName.ShouldBe("Thermometer");
        device.DeviceType.Value.ShouldBe(1);
    }

    [Fact]
    public void Should_Returns_DeviceType_When_DeviceType_Not_Null()
    {
        // arrange
        var device = new Device();

        // act
        device.SetDeviceType("Thermometer");
        var result = device.GetDeviceType();

        // assert
        result.ShouldNotBeNull();
    }
}
