using TechOnIt.Application.Common.Security.JwtBearer;
using TechOnIt.Application.Services.Authenticateion;
using TechOnIt.Domain.Entities.Identity.UserAggregate;
using TechOnIt.Domain.ValueObjects;

namespace TechOnIt.Application.UnitTests.ServiceTests.IdentityTests;

public class AuthenticationServiceTest
{

    #region constructor

    private Mock<IUnitOfWorks> _unitOfWorks;
    private Mock<IJwtService> _jwtService;

    public AuthenticationServiceTest()
    {
        _unitOfWorks = new Mock<IUnitOfWorks>();
        _jwtService = new Mock<IJwtService>();
    }

    private User NewUserInstance()
    {
        var user = User.CreateNewInstance("ashnoori11@gmail.com", "09124133486");
        user.SetPassword(PasswordHash.Parse("Aa123456@"));
        return user;
    }

    private IdentityService Subject()
        => new IdentityService(_unitOfWorks.Object, null, null);
    #endregion

    #region model
    public string Phonenumber { get; set; }
    public string Password { get; set; }
    #endregion

    // TODO:
    // Ashkan
    // What this :/

    //public void GivenRequestToSignInExistsUserInSystem()
    //{

    //}

    //public void WhenAddedPhonenumberAndPasswordAreRight()
    //{
    //    this.Phonenumber = "09124133486";
    //    this.Password = "Aa123456@";
    //}

    //public async Task ThenReturnsTokenSuccessfully()
    //{
    //    var service = Subject();
    //    var userModel = NewUserInstance();

    //    _unitOfWorks.Setup(a => a.UserRepository.FindUserByPhoneNumberWithRolesAsyncNoTracking(this.Phonenumber, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(userModel);

    //    _jwtService.Setup(a => a.GenerateAccessToken(userModel, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(new AccessToken() { Token = "sdhksdhgkhsdkgsd" });

    //    var cancellationToken = new CancellationTokenSource();
    //    var result = await service.SignInUserAsync(this.Phonenumber, this.Password, cancellationToken.Token);

    //    Assert.NotNull(result.Token.Token);
    //    Assert.NotNull(result.Message);

    //    Assert.Equal("sdhksdhgkhsdkgsd", result.Token.Token);
    //    Assert.Equal("Welcome !", result.Message);
    //}

    [Fact]
    public void Sould_User_Succeesfully_Authenticated_With_Right_Informations()
    {
        this.BDDfy();
    }
}
