using iot.iot.Infrastructure.Common.Encryptions.SecurityTypes;

namespace iot.Infrastructure.Common.Encryptions.Contracts;

public interface IEncryptionHandlerService
{
    Task<string> GetEncryptAsync(SensitiveEntities sensitiveDataType, string plainText, CancellationToken cancellationToken);
    Task<string> GetDecryptAsync(SensitiveEntities sensitiveDataType, string encryptedString, CancellationToken cancellationToken);
}
