using System.Security.Cryptography;
using System.Text;

namespace SpeedDeal.Infrastructure;

  public static  class Hasher
    {
    public static string HashPasword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(64);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            350000,
            HashAlgorithmName.SHA512,
            64);
        return Convert.ToHexString(hash);
    }

    public static bool IsPaswordOk(string password, string userPasswordHash, byte[] salt)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            350000,
            HashAlgorithmName.SHA512,
            64);
        return userPasswordHash == Convert.ToHexString(hash);
    }
}
