using System.Security.Cryptography;

namespace iot.Infrastructure.Common.Extentions;

public static class CryptoTransformExtention
{
    public static byte[] Encrypt(this ICryptoTransform cryptoTransform, string plainText)
    {
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, cryptoTransform, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                return msEncrypt.ToArray();
            }
        }
    }

    public static string Decrypt(this ICryptoTransform cryptoTransform, byte[] cipher)
    {
        using (MemoryStream msDecrypt = new MemoryStream())
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, cryptoTransform, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}