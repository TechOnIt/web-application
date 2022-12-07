namespace TechOnIt.Infrastructure.Common.Encryptions.Contracts;

public interface IEncryptor
{
    string Encrypt(string plainText);
    string Decrypt(string encryptedString);
}
