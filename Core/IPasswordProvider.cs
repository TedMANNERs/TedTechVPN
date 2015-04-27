using System.Security;

namespace Core
{
    public interface IPasswordProvider
    {
        byte[] Hash(SecureString password, byte[] salt);
        byte[] SecureStringToByteArray(SecureString password);
        byte[] GenerateSalt();
    }
}