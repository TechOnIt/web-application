using iot.Application.Common.ViewModels.Structures.Authentication;
using iot.Infrastructure.Repositories.UnitOfWorks;

namespace iot.Application.Commands.Structures.Authentication.SignInCommands;

public class SignInStructureCommandHandler : IRequestHandler<SignInStructureCommand, Result<StructureAccessToken>>
{
    #region DI & Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public SignInStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result<StructureAccessToken>> Handle(SignInStructureCommand request, CancellationToken cancellationToken = default)
    {
        //var signinPassword = await _unitOfWorks.UserRepository.UserSignInByPasswordAsync(request.Username, request.Password, cancellationToken);

        //if (signinPassword.Token is null)
        //    return Result.Fail(signinPassword.Message);

        //return Result.Ok(signinPassword.Token);
        var result = new StructureAccessToken();
        result.Token = "aaaa";
        result.RefreshToken = "bbbb";

        return Result.Ok(result);
    }
}