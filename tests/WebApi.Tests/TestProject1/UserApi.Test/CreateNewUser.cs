using FluentAssertions;
using FluentResults;
using TechOnIt.Application.Commands.Users.Management.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Tests.UserApi.Test;

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


    //public void GivenRequestToCreateNewUser()
    //{

    //}

    //public void WhenNewUserAddedToSystemWithDetails()
    //{
    //    this._command = new CreateUserCommand
    //    {
    //        Name = "testName",
    //        Surname = "testsurname",
    //        Email = "testEmail",
    //        Password = "Aa123456@",
    //        PhoneNumber = "09124133486"
    //    };
    //}

    public async Task ThenResponseShouldBe200Ok()
    {
        var mockData = new Mock<IMediator>();

        mockData.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(FluentResults.Result.Ok() as FluentResults.Result);

        var result = (OkObjectResult)await _userController.Create(this._command);
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void User_Should_Insert_Successfully_With_Valid_Details()
    {
        this.BDDfy();
    }
}
