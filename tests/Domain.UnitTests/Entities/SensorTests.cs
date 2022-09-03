using iot.Domain.Entities.Product;
using Shouldly;

namespace iot.Domain.UnitTests.Entities;

public class SensorTests
{
    [Fact]
    public void Should_Set_Sensorype_By_SensorType_Name()
    {
        // arrange
        var sensor = new Sensor();

        // act
        sensor.SetSensorType("Light");

        // assert
        sensor.SensorType.ShouldNotBeNull();
        sensor.SensorType.DisplayName.ShouldBe("Light");
    }

    [Fact]
    public void Should_Set_SensorType_By_SensorType_Value()
    {
        // arrange
        var sensor = new Sensor();

        // act
        sensor.SetSensorType(3);

        // assert
        sensor.SensorType.ShouldNotBeNull();
        sensor.SensorType.DisplayName.ShouldBe("Light");
        sensor.SensorType.Value.ShouldBe(3);
    }

    [Fact]
    public void Should_Returns_SensorType_When_SensorType_Not_Null()
    {
        // arrange
        var sensor = new Sensor();

        // act
        sensor.SetSensorType("Light");
        var result = sensor.GetSensorType();

        // assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(3);
        result.DisplayName.ShouldBe("Light");
    }

    [Fact]
    public void Should_Returns_SensorType_Equal_ExistingType_SensorType()
    {
        // arrange
        var sensor = new Sensor();
        var sensor2 = new Sensor();

        // act
        sensor.SetSensorType("Light");
        var result = sensor.GetSensorType();

        sensor2.SetSensorType(4); //EnergyConsumption
        var result2 = sensor2.GetSensorType();

        // assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(3);
        result.DisplayName.ShouldBe("Light");

        result2.ShouldNotBeNull();
        result2.Value.ShouldBe(4);
        result2.DisplayName.ShouldBe("EnergyConsumption");

        result.Value.ShouldNotBe(result2.Value);
    }
}
