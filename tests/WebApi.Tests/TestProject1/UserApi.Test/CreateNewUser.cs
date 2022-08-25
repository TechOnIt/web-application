using iot.Identity.Api.Areas.Manage.Controllers.v1;
using iot.Identity.Api.Controllers;

namespace TestProject1.UserApi.Test;

public class CreateNewUser
{
    #region Constructor
    private BaseController _baseController;
    private UserController _userController;
    private IMediator _mediator;

    public CreateNewUser()
    {
        //_mediator = new BaseController();
    }
    #endregion


    public async void GivenRequestToCreateNewUser()
    {

    }

    public async void WhenNewUserAddedToSystemWithDetails()
    {
    }

    public async void ThenResponseShouldBe200Ok()
    {

    }

    [Fact]
    public void User_Should_Insert_Successfully_With_Valid_Details()
    {
        this.BDDfy();
    }
}
