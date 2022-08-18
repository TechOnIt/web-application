using iot.Application.Common.Security;
using System.Security.Claims;

namespace Infrastructure.UnitTests.Common.JwtBearerService;

public class JwtServiceTests
{
    [Theory]
    [MemberData(nameof(TokenTest))]
    public void Should_Return_New_Token_Accept_Claim(List<Claim> claims, int expireTime)
    {
        // arrange
        var service = new JwtService();

        // act
        var result = service.GenerateTokenWithClaims(claims, expireTime);
        var anyException = Record.Exception(() => service.GenerateTokenWithClaims(claims, expireTime));

        // assert
        Assert.NotNull(result);
        Assert.Null(anyException);
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