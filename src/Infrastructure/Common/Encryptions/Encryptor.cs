using System.Text;
using TechOnIt.Infrastructure.Common.Encryptions.Contracts;
using TechOnIt.Infrastructure.Common.Extentions;

namespace TechOnIt.Infrastructure.Common.Encryptions;

public class Encryptor : IEncryptor
{
    public Encryptor(byte[] Key)
    {
        this.Key = Key;
    }

    public Encryptor(string keyString)
    {
        this.keyString = keyString;
    }

    private string keyString { get; set; }
    private byte[]? Key { get; set; }

    public string Decrypt(string encryptedString)
    {
        var split = encryptedString.Split(' ');
        var iv = Convert.FromBase64String(split[0]);
        var cipher = Convert.FromBase64String(split[1]);

        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            var decryptor = aes.CreateDecryptor(GetKey(), iv);
            var plainText = decryptor.Decrypt(cipher);

            return plainText;
        }
    }

    public string Encrypt(string plainText)
    {
        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            var encryptor = aes.CreateEncryptor(GetKey(), aes.IV);
            var encryptedBytes = encryptor.Encrypt(plainText);

            return string.Concat(Convert.ToBase64String(aes.IV), " ", Convert.ToBase64String(encryptedBytes));
        }
    }

    #region privates
    private byte[] GetKey()
    {
        if (string.IsNullOrWhiteSpace(keyString) && Key is not null)
            return Key;
        else if (!string.IsNullOrWhiteSpace(keyString) && Key is null)
            return Encoding.UTF8.GetBytes(keyString);
        else
            throw new ArgumentNullException("Key can not be null");
    }
    #endregion
}
