using TechOnIt.Application.Common.Models.ViewModels.Structures.Authentication;

namespace TechOnIt.Application.Services.Authenticateion.StructuresService;

public interface IStructureService
{
    Task<(StructureAccessToken? Token, string Message)?> SignInAsync(string apiKey, string password,
    CancellationToken cancellationToken = default);
}