namespace TechOnIt.Domain.UnitTests.Entities;

public class DeviceTests
{
    [Fact]
    /// <summary>
    /// When device object created, it should have id.
    /// </summary>
    public void Should_Have_Id()
    {
        // arrange
        var device = new Device(63, DeviceType.Heater, Guid.NewGuid());
        // assert
        device.Id.ToString().ShouldNotBeNull();
    }

    #region Pin
    [Theory]
    [InlineData(1)]
    [InlineData(9)]
    [InlineData(31)]
    [InlineData(18)]
    [InlineData(255)]
    /// <summary>
    /// Device should initialize by pin.
    /// </summary>
    /// <param name="pin">Device pin number.</param>
    public void Should_Have_Pin(int pin)
    {
        // arrange
        var device = new Device(pin, DeviceType.Fan, Guid.NewGuid());
        // assert
        device.Pin.ShouldBe(pin);
    }
    [Theory]
    [InlineData(256)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-9)]
    [InlineData(-18)]
    /// <summary>
    /// Range of pin should between 1-255.
    /// </summary>
    /// <param name="pin">Device pin number.</param>
    public void Should_Out_Of_Range_Pin_Exception(int pin)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => new Device(pin, DeviceType.Fan, Guid.NewGuid()));
    }
    [Theory]
    [InlineData(3, 14)]
    [InlineData(18, 1)]
    [InlineData(9, 25)]
    /// <summary>
    /// Device pin should change by SetPin() method.
    /// </summary>
    /// <param name="pin">Initialize device pin number.</param>
    /// <param name="newPin">New pin number you want to change.</param>
    public void Should_Set_New_Pin(int pin, int newPin)
    {
        // arrange
        var device = new Device(pin, DeviceType.Fan, Guid.NewGuid());
        // act
        device.SetPin(newPin);
        // assert
        device.Pin.ShouldBe(newPin);
    }
    #endregion

    #region IsHigh
    [Fact]
    /// <summary>
    /// Device should high by High() method. (IsHigh = true)
    /// </summary>
    public void Should_Be_High()
    {
        // arrange
        var device = new Device(63, DeviceType.Heater, Guid.NewGuid());
        // act
        device.High();
        // assert
        device.IsHigh.ShouldBeTrue();
    }
    [Fact]
    /// <summary>
    /// Device should low by Low() method. (IsHigh = false)
    /// </summary>
    public void Should_Be_Low()
    {
        // arrange
        var device = new Device(63, DeviceType.Heater, Guid.NewGuid());
        // act
        device.High();
        device.Low();
        // assert
        device.IsHigh.ShouldBeFalse();
    }
    #endregion

    #region Place
    [Fact]
    /// <summary>
    /// Device should have device id on initialize.
    /// </summary>
    public void Should_Have_Place_Id_On_Initialize()
    {
        var device = new Device(12, DeviceType.Fan, Guid.NewGuid());
        device.Id.ToString().ShouldNotBeNullOrEmpty();
    }
    #endregion
}