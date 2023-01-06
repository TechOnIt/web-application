using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Application.Common.Models.ViewModels.Users.Authentication;
using TechOnIt.Application.Services.AssemblyServices;

namespace TechOnIt.Application.Common.Security.JwtBearer;

public class JwtService : IJwtService
{
    #region constructure
    private readonly IAppSettingsService<AppSettingDto> _appSetting;
    public JwtService(IAppSettingsService<AppSettingDto> appSetting)
    {
        _appSetting = appSetting;
    }
    #endregion

    #region new one
    public async Task<AccessToken> GenerateAccessToken(IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await Task.FromResult(new AccessToken
        {
            Token = Generate(claims),
            TokenExpireAt = DateTime.Now.AddMinutes(_appSetting.Value.JwtSettings.ExpirationMinutes).ToString()
        });
    }


    private string Generate(IEnumerable<Claim> claims)
    {
        var secretKey = Encoding.UTF8.GetBytes(_appSetting.Value.JwtSettings.SecretKey); // it must be atleast 16 characters or more
        var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _appSetting.Value.JwtSettings.Issuer,
            Audience = _appSetting.Value.JwtSettings.Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(_appSetting.Value.JwtSettings.NotBeforeMinutes),
            Expires = DateTime.Now.AddHours(_appSetting.Value.JwtSettings.ExpirationMinutes),
            SigningCredentials = signinCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(descriptor);
        string token = tokenHandler.WriteToken(securityToken);
        return token;
    }
    #endregion

    #region the bad one
    //public async Task<AccessToken> GenerateAccessToken(User user, IList<Role> userRoles, CancellationToken cancellationToken)
    //{
    //    cancellationToken.ThrowIfCancellationRequested();
    //    return await Task.FromResult(new AccessToken
    //    {
    //        Token = Generate(user, userRoles),
    //        TokenExpireAt = DateTime.Now.AddMinutes(_appSetting.Value.JwtSettings.ExpirationMinutes).ToString()
    //    });
    //}


    //private string Generate(User user, IList<Role> userRoles)
    //{
    //    var secretKey = Encoding.UTF8.GetBytes(_appSetting.Value.JwtSettings.SecretKey); // it must be atleast 16 characters or more
    //    var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),SecurityAlgorithms.HmacSha256Signature);

    //    var claims = GetClaims(user,userRoles);
    //    var descriptor = new SecurityTokenDescriptor
    //    {
    //        Issuer = _appSetting.Value.JwtSettings.Issuer,
    //        Audience = _appSetting.Value.JwtSettings.Audience,
    //        IssuedAt = DateTime.Now,
    //        NotBefore = DateTime.Now.AddMinutes(_appSetting.Value.JwtSettings.NotBeforeMinutes),
    //        Expires = DateTime.Now.AddHours(_appSetting.Value.JwtSettings.ExpirationMinutes),
    //        SigningCredentials = signinCredentials,
    //        Subject = new ClaimsIdentity(claims)
    //    };

    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var securityToken = tokenHandler.CreateToken(descriptor);
    //    string token = tokenHandler.WriteToken(securityToken);
    //    return token;
    //}

    //private IEnumerable<Claim> GetClaims(User user, IList<Role> userRoles)
    //{
    //    IList<Claim> claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Name,$"{user.FullName.Name} {user.FullName.Surname}"),
    //        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
    //        new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
    //    };

    //    if (userRoles.Count() > 0)
    //    {
    //        foreach (var role in userRoles)
    //        {
    //            claims.Add(new Claim(ClaimTypes.Role, role.Name));
    //        }
    //    }

    //    return claims;
    //}
    #endregion
}