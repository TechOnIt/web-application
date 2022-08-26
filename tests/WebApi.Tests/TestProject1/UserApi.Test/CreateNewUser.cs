using FluentAssertions;
using iot.Application.Commands.Users.Management.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace TestProject1.UserApi.Test;

public class CreateNewUser
{
    #region Constructor
    private Mock<IMediator> _mediator;

    private UserController _userController;
    private CreateUserCommand _command;

    public CreateNewUser()
    {
        _mediator = new Mock<IMediator>();

        _userController = new UserController(_mediator.Object);
    }
    #endregion


    public async void GivenRequestToCreateNewUser()
    {

    }

    public async Task WhenNewUserAddedToSystemWithDetails()
    {
        this._command = new CreateUserCommand
        {
            Name = "testName",
            Surname = "testsurname",
            Email = "testEmail",
            Password = "Aa123456@",
            PhoneNumber = "09124133486"
        };
    }

    public async Task ThenResponseShouldBe200Ok()
    {
        var returnResult = FluentResults.Result.Ok(new Guid());

        //_mediator.Setup(i => i.Send(new CreateUserCommand(), It.IsAny<System.Threading.CancellationToken>()))
        //    .ReturnsAsync(FluentResults.Result.Ok(new Guid()));
        //Task.FromResult(handHeldWrapperDataViewMoq)

        _mediator.Setup(i => i.Send(new CreateUserCommand(), It.IsAny<System.Threading.CancellationToken>()))
                .Returns(Task.FromResult(returnResult));


        var result = (OkObjectResult)await _userController.Create(this._command);
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void User_Should_Insert_Successfully_With_Valid_Details()
    {
        this.BDDfy();
    }
}
