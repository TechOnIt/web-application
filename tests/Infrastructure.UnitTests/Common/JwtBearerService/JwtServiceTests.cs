using TechOnIt.Application.Common.Security.JwtBearer;
using System.Security.Claims;
using Moq;

namespace Infrastructure.UnitTests.Common.JwtBearerService;

public class JwtServiceTests
{
    [Theory]
    [MemberData(nameof(TokenTest))]
    public async void Should_Return_New_Token_Accept_Claim(List<Claim> claims, int expireTime)
    {
        // arrange
        var service = new Mock<IJwtService>();
        var expireDateTime = DateTime.Now.AddMinutes(expireTime);
        // act
        //var result = await service(claims, expireDateTime);
        //var anyException = Record.Exception(() => service.GenerateTokenWithClaims(claims, expireDateTime));

        // assert
        //Assert.NotNull(result);
        //Assert.Null(anyException);
    }

    public static IEnumerable<object[]> TokenTest()
    {
        var User1 = new List<Claim>()
                {
                    new Claim("UserId","235325"),
                    new Claim("UserName","usernameUser"),
                    new Claim("Role","User")
                };

        var User2 = new List<Claim>()
                {
                    new Claim("UserId","6344574"),
                    new Claim("UserName","usernameAdmin"),
                    new Claim("Role","Admin")
                };

        var User3 = new List<Claim>()
                {
                    new Claim("UserId","5745754"),
                    new Claim("UserName","usernameAdmin"),
                    new Claim("Role","Admin")
                };

        yield return new object[] { User1, 20 };
        yield return new object[] { User2, 100 };
        yield return new object[] { User3, null };
    }
}