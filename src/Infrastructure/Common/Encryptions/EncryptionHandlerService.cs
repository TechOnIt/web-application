using iot.Infrastructure.Common.Encryptions.Contracts;
using iot.Infrastructure.Persistence.Context;
using iot.iot.Infrastructure.Common.Encryptions.SecurityTypes;
using Microsoft.EntityFrameworkCore;

namespace iot.Infrastructure.Common.Encryptions;

public class EncryptionHandlerService : IEncryptionHandlerService
{
    #region constructor
    private readonly IdentityContext _context;
    public EncryptionHandlerService(IdentityContext context)
    {
        _context = context;
    }
    #endregion

    public async Task<string> GetEncryptAsync(SensitiveEntities sensitiveDataType, string plainText, CancellationToken cancellationToken = default)
    {
        if (sensitiveDataType == SensitiveEntities.Sensor)
            return await EncryptWithSensorKeyAsync(plainText, cancellationToken);
        else if (sensitiveDataType == SensitiveEntities.Users)
            return await EncryptWithUserKeyAsync(plainText, cancellationToken);
        else if (sensitiveDataType == SensitiveEntities.Device)
            return await EncryptWithDeviceKeyAsync(plainText, cancellationToken);
        else if (sensitiveDataType == SensitiveEntities.Reports)
            return await EncryptWithReportKeyAsync(plainText, cancellationToken);
        else
            throw new NotImplementedException($"type : {sensitiveDataType.DisplayName} Not Implemented !");
    }

    public async Task<string> GetDecryptAsync(SensitiveEntities sensitiveDataType, string encryptedString, CancellationToken cancellationToken = default)
    {
        if (sensitiveDataType == SensitiveEntities.Sensor)
            return await DecryptWithSensorKeyAsync(encryptedString, cancellationToken);
        else if (sensitiveDataType == SensitiveEntities.Users)
            return await DecryptWithUserKeyAsync(encryptedString, cancellationToken);
        else if (sensitiveDataType == SensitiveEntities.Device)
            return await DecryptWithDeviceKeyAsync(encryptedString, cancellationToken);
        else if (sensitiveDataType == SensitiveEntities.Reports)
            return await DecryptWithReportKeyAsync(encryptedString, cancellationToken);
        else
            throw new NotImplementedException($"type : {sensitiveDataType.DisplayName} Not Implemented !");
    }

    #region privates
    private async Task<string> EncryptWithDeviceKeyAsync(string plainText, CancellationToken cancellationToken = default)
    {
        var key = await _context.AesKeys.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Title == "DeviceKey", cancellationToken);

        if (key is not null)
        {
            var encryptor = new Encryptor(Convert.FromBase64String(key.Key));
            return encryptor.Encrypt(plainText);
        }
        else
            throw new NullReferenceException($"there is no key for Device in system");
    }

    private async Task<string> EncryptWithUserKeyAsync(string plainText, CancellationToken cancellationToken = default)
    {
        var key = await _context.AesKeys.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Title == "UserKey", cancellationToken);

        if (key is not null)
        {
            var encryptor = new Encryptor(Convert.FromBase64String(key.Key));
            return encryptor.Encrypt(plainText);
        }
        else
            throw new NullReferenceException($"there is no key for User in system");
    }

    private async Task<string> EncryptWithSensorKeyAsync(string plainText, CancellationToken cancellationToken = default)
    {
        var key = await _context.AesKeys.AsNoTracking().FirstOrDefaultAsync(a => a.Title == "SesnsorKey", cancellationToken);
        if (key is not null)
        {
            var encryptor = new Encryptor(Convert.FromBase64String(key.Key));
            return encryptor.Encrypt(plainText);
        }
        else
            throw new NullReferenceException($"there is no key for Sesnsor in system");
    }

    private async Task<string> EncryptWithReportKeyAsync(string plainText, CancellationToken cancellationToken = default)
    {
        var key = await _context.AesKeys.AsNoTracking().FirstOrDefaultAsync(a => a.Title == "ReportKey", cancellationToken);
        if (key is not null)
        {
            var encryptor = new Encryptor(Convert.FromBase64String(key.Key));
            return encryptor.Encrypt(plainText);
        }
        else
            throw new NullReferenceException($"there is no key for Report in system");
    }



    private async Task<string> DecryptWithDeviceKeyAsync(string encryptedString, CancellationToken cancellationToken = default)
    {
        var key = await _context.AesKeys.AsNoTracking().FirstOrDefaultAsync(a => a.Title == "DeviceKey", cancellationToken);
        if (key is not null)
        {
            var encryptor = new Encryptor(Convert.FromBase64String(key.Key));
            return encryptor.Decrypt(encryptedString);
        }
        else
            throw new NullReferenceException($"there is no key for Device in system");
    }

    private async Task<string> DecryptWithUserKeyAsync(string encryptedString, CancellationToken cancellationToken = default)
    {
        var key = await _context.AesKeys.AsNoTracking().FirstOrDefaultAsync(a => a.Title == "UserKey", cancellationToken);
        if (key is not null)
        {
            var encryptor = new Encryptor(Convert.FromBase64String(key.Key));
            return encryptor.Decrypt(encryptedString);
        }
        else
            throw new NullReferenceException($"there is no key for User in system");
    }

    private async Task<string> DecryptWithSensorKeyAsync(string encryptedString, CancellationToken cancellationToken = default)
    {
        var key = await _context.AesKeys.AsNoTracking().FirstOrDefaultAsync(a => a.Title == "SesnsorKey", cancellationToken);
        if (key is not null)
        {
            var encryptor = new Encryptor(Convert.FromBase64String(key.Key));
            return encryptor.Decrypt(encryptedString);
        }
        else
            throw new NullReferenceException($"there is no key for Sesnsor in system");
    }

    private async Task<string> DecryptWithReportKeyAsync(string encryptedString, CancellationToken cancellationToken = default)
    {
        var key = await _context.AesKeys.AsNoTracking().FirstOrDefaultAsync(a => a.Title == "ReportKey", cancellationToken);
        if (key is not null)
        {
            var encryptor = new Encryptor(Convert.FromBase64String(key.Key));
            return encryptor.Decrypt(encryptedString);
        }
        else
            throw new NullReferenceException($"there is no key for Report in system");
    }

    #endregion
}
