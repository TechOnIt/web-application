namespace TechOnIt.Domain.UnitTests.Entities;

public class SensorReportTest
{
    #region Value
    [Theory]
    [InlineData(-5000)]
    [InlineData(-175.8524)]
    [InlineData(0)]
    [InlineData(12.6893)]
    [InlineData(9522)]
    public void Should_Initialize_Value(double value)
    {
        SensorReport newSensorReport = new(value: value, sensorId: Guid.NewGuid());
        newSensorReport.Value.ShouldBe(value);
    }
    #endregion

    #region SensorId
    [Fact]
    public void Should_Initialize_SensorId()
    {
        var sensorId = Guid.NewGuid();
        SensorReport newSensorReport = new(value: 9, sensorId: sensorId);
        newSensorReport.SensorId.ShouldBe(sensorId);
    }
    #endregion
}
