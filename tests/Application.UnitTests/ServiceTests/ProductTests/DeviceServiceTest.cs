using iot.Application.Common.ViewModels.Devices;

namespace iot.Application.UnitTests.ServiceTests.ProductTests;

public class DeviceCreateNewDeviceServiceTest
{
    #region constructor
    private Mock<IUnitOfWorks> _unitOfWork;

    public DeviceCreateNewDeviceServiceTest()
    {
        _unitOfWork = new Mock<IUnitOfWorks>();
    }

    private DeviceService Subject() => new DeviceService(_unitOfWork.Object);
    #endregion

    #region properties
    public DeviceViewModel viewModel { get; set; }
    public Device model { get; set; }
    public CancellationToken cancellationToken { get; set; }
    #endregion

    #region create new device test
    public void Given_Request_To_Create_New_Device()
    {

    }

    public void When_Data_IsValid_And_Device_NotExists_In_System()
    {
        this.model = new Device(2, DeviceType.Heater, true, Guid.NewGuid());
        this.viewModel = new DeviceViewModel(Guid.NewGuid(), Guid.NewGuid(), 2, DeviceType.Heater, true);
        this.cancellationToken = new CancellationTokenSource().Token;
    }

    public async Task Then_Device_Will_Create_Successfully()
    {
        // arrange
        var service = Subject();

        _unitOfWork.Setup(repo => repo.DeviceRepositry.CreateAsync(this.model, this.cancellationToken))
            .Returns(Task.CompletedTask);

        _unitOfWork.Setup(repo => repo.SaveAsync(this.cancellationToken, false)).Returns(Task.CompletedTask);

        // act
        var result = await service.CreateAsync(this.viewModel, this.cancellationToken);
        var exceptionResult = Record.ExceptionAsync(() => service.CreateAsync(this.viewModel, this.cancellationToken)).Result;

        // assert
        exceptionResult.ShouldBe(null);
        result.ShouldNotBe(null);
        result.ShouldBeOfType(typeof(DeviceViewModel));
    }

    [Fact]
    public void Create_new_device_returns_200Ok()
    {
        this.BDDfy();
    }

    #endregion

}

public class DeviceDeleteExistingDeviceByIDTest
{
    #region constructor
    private Mock<IUnitOfWorks> _unitOfWork;

    public DeviceDeleteExistingDeviceByIDTest()
    {
        _unitOfWork = new Mock<IUnitOfWorks>();
    }

    private DeviceService Subject() => new DeviceService(_unitOfWork.Object);
    #endregion

    #region properties
    public Guid DeviceId { get; set; }
    public CancellationToken cancellationToken { get; set; }
    #endregion

    public void Given_Request_To_Delete_Existing_Device()
    {

    }

    public void When_DeviceId_Is_Valid_Id()
    {
        this.DeviceId = Guid.NewGuid();
        this.cancellationToken = new CancellationTokenSource().Token;
    }

    public async Task Then_Device_Will_Remove_Compeletlly_With_Status_200Ok()
    {
        // act
        var service = Subject();

        _unitOfWork.Setup(repo => repo.DeviceRepositry
        .DeleteByIdAsync(this.DeviceId, this.cancellationToken))
            .Returns(Task.CompletedTask);

        _unitOfWork.Setup(repo => repo
        .SaveAsync(this.cancellationToken, false))
            .Returns(Task.CompletedTask);

        // arrange
        var result = await service.DeleteByIdAsync(this.DeviceId, this.cancellationToken);
        var exceptionResult = Record.ExceptionAsync(() => service.DeleteByIdAsync(this.DeviceId, this.cancellationToken)).Result;

        // assert
        result.ShouldBe(true);
        exceptionResult.ShouldBeNull();
    }

    [Fact]
    public void Delete_ExistingDeviceFromDataBase()
    {
        this.BDDfy();
    }
}

public class DeviceUpdateExistingDeviceTest
{
    #region constructor
    private Mock<IUnitOfWorks> _unitOfWork;
    public DeviceUpdateExistingDeviceTest()
    {
        //_unitOfWork = new Mock<IUnitOfWorks>(MockBehavior.Strict);
        _unitOfWork = new Mock<IUnitOfWorks>();
    }

    private DeviceService Subject() => new DeviceService(_unitOfWork.Object);

    #endregion

    #region properties
    public DeviceViewModel viewModel { get; set; }
    public Guid deviceId { get; set; }
    public Guid placeId { get; set; }
    public Device model { get; set; }
    public CancellationToken cancellationToken { get; set; }
    #endregion

    public void Given_Request_ToUpdate_Existing_Device_With_Valid_Data()
    {

    }

    public void When_Device_Is_Exists_And_Data_Is_Valid()
    {
        this.deviceId = Guid.NewGuid();
        this.placeId = Guid.NewGuid();

        this.cancellationToken = new CancellationTokenSource().Token;
        this.viewModel = new DeviceViewModel(deviceId, placeId, 5, DeviceType.Heater, true);

        // device in system
        this.model = new Device();
        this.model.SetDeviceType(DeviceType.Cooler);
        this.model.SetDeviceId(deviceId);
        this.model.IsHigh = true;
        this.model.Pin = 2;
        this.model.PlaceId = placeId;
    }

    public async void Then_Device_Will_Update_SuccessFully()
    {
        // arrange
        var service = Subject();

        _unitOfWork.Setup(repo => repo.DeviceRepositry.FindByIdAsync(this.deviceId, this.cancellationToken))
            .ReturnsAsync(new Device(this.deviceId, 2, DeviceType.Cooler, true, this.placeId));

        _unitOfWork.Setup(repo => repo.DeviceRepositry.UpdateAsync(this.model, this.cancellationToken)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(repo => repo.SaveAsync(this.cancellationToken, false)).Returns(Task.CompletedTask);

        //act
        var result = await service.UpdateAsync(this.viewModel, this.cancellationToken);
        var exceptionResult = Record.ExceptionAsync(() => service.UpdateAsync(this.viewModel, this.cancellationToken)).Result;

        //assert
        result.ShouldNotBeNull();
        result.ShouldBeSameAs(this.viewModel);
        exceptionResult.ShouldBeNull();
    }

    [Fact]
    public void Should_Update_Device_SuccessFully()
    {
        this.BDDfy();
    }
}

public class DeviceFindByIdDeviceTest
{

    #region constructor
    private Mock<IUnitOfWorks> _unitOfWork;

    public DeviceFindByIdDeviceTest()
    {
        _unitOfWork = new Mock<IUnitOfWorks>();
    }

    private DeviceService Subject() => new DeviceService(_unitOfWork.Object);
    #endregion

    #region properties
    public Guid DeviceId { get; set; }
    public CancellationToken cancellationToken { get; set; }
    #endregion

    public void Given_Request_To_Find_Device()
    {

    }

    public void When_DeviceId_Is_Not_Null()
    {
        this.DeviceId = Guid.NewGuid();
        this.cancellationToken = new CancellationTokenSource().Token;
    }

    public async Task Then_Should_Not_Have_Any_Exceptions_Or_Errors()
    {
        // arrange
        var service = Subject();

        Device? findResult = null;

        _unitOfWork.Setup(repo => repo.DeviceRepositry.FindByIdAsync(this.DeviceId, this.cancellationToken))
            .ReturnsAsync(findResult);

        // act
        var exceptionResult = Record.ExceptionAsync(() => service.FindByIdAsync(this.DeviceId, this.cancellationToken)).Result;

        // assert
        exceptionResult.ShouldBeNull();
    }

    [Fact]
    public void Should_Find_Existing_Device_By_Id_Successfully()
    {
        this.BDDfy();
    }
}

public class DeviceFindByIdDeviceAsNoTrackingTest
{
    #region constructor
    private Mock<IUnitOfWorks> _unitOfWork;

    public DeviceFindByIdDeviceAsNoTrackingTest()
    {
        _unitOfWork = new Mock<IUnitOfWorks>();
    }

    private DeviceService Subject() => new DeviceService(_unitOfWork.Object);
    #endregion

    #region properties
    public Guid DeviceId { get; set; }
    public CancellationToken cancellationToken { get; set; }
    #endregion

    public void Given_Request_To_Find_Device_AsNoTracking()
    {

    }

    public void When_DeviceId_Is_Not_Null()
    {
        this.DeviceId = Guid.NewGuid();
        this.cancellationToken = new CancellationTokenSource().Token;
    }

    public async Task Then_Should_Not_Have_Any_Exceptions_Or_Errors()
    {
        // arrange
        var service = Subject();

        _unitOfWork.Setup(repo => repo.DeviceRepositry.FindByIdAsNoTrackingAsync(this.DeviceId, this.cancellationToken))
            .ReturnsAsync(new Device(this.DeviceId, 2, DeviceType.Cooler, true, Guid.NewGuid()));

        // act
        var result = await service.FindByIdAsNoTrackingAsync(this.DeviceId,this.cancellationToken);
        var exceptionResult = Record.ExceptionAsync(() => service.FindByIdAsNoTrackingAsync(this.DeviceId, this.cancellationToken)).Result;

        // assert
        result.ShouldNotBeNull();
        exceptionResult.ShouldBeNull();
    }

    [Fact]
    public void Should_Find_Existing_Device_By_Id_AsNoTracking_Successfully()
    {
        this.BDDfy();
    }
}