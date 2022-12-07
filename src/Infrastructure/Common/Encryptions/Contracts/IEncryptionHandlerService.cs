using TechOnIt.Infrastructure.Common.Encryptions.SecurityTypes;

namespace TechOnIt.Infrastructure.Common.Encryptions.Contracts;

public interface IEncryptionHandlerService
{
    Task<string> GetEncryptAsync(SensitiveEntities sensitiveDataType, string plainText, CancellationToken cancellationToken = default);
    Task<string> GetDecryptAsync(SensitiveEntities sensitiveDataType, string encryptedString, CancellationToken cancellationToken = default);
}
