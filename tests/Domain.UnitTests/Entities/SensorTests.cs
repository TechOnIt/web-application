namespace TechOnIt.Domain.UnitTests.Entities;

public class SensorTests
{
    #region Pin
    [Theory]
    [InlineData(1)]
    [InlineData(6)]
    [InlineData(12)]
    [InlineData(27)]
    public void Should_Have_Pin_when_Created(int pin)
    {
        Sensor newSensor = new(pin: pin, type: SensorType.Psychrometer,
            placeId: Guid.NewGuid());

        newSensor.Pin.ShouldBe(pin);
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(5, 13)]
    [InlineData(25, 8)]
    public void Should_Set_Pin(int pin, int newPin)
    {
        // Arrange
        Sensor newSensor = new(pin: pin, type: SensorType.Earthquake, placeId: Guid.NewGuid());
        // Act
        newSensor.SetPin(newPin);
        // Assert
        newSensor.Pin.ShouldBe(newPin);
    }
    #endregion

    #region PlaceId
    [Fact]
    public void Should_Not_Be_Empty_Place_Id()
    {
        Sensor newSensor = new(pin: 13, type: SensorType.SmellDetection, placeId: Guid.NewGuid());
        newSensor.PlaceId.ShouldNotBe(Guid.Empty);
    }
    #endregion
}