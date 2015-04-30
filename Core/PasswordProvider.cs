using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace TedTechVpn.Core
{
    public class PasswordProvider : IPasswordProvider
    {
        public byte[] Hash(SecureString password, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA512Cng();
            byte[] hash = new byte[password.Length + salt.Length];
            byte[] passwordBytes = SecureStringToByteArray(password);

            for (int i = 0; i < passwordBytes.Length; ++i)
            {
                hash[i] = passwordBytes[i];
            }

            for (int i = 0; i < salt.Length; ++i)
            {
                hash[passwordBytes.Length + 1] = salt[i];
            }

            return algorithm.ComputeHash(hash);
        }

        public byte[] SecureStringToByteArray(SecureString password)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                byte[] result = new byte[password.Length];
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(password);
                for (int i = 0; i < password.Length; ++i)
                {
                    result[i] = Marshal.ReadByte(unmanagedString, i * 2);
                    //result[i + 1] = Marshal.ReadByte(unmanagedString, i * 2 + 1);
                }

                return result;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[32];
            rng.GetNonZeroBytes(bytes);
            return bytes;
        }

    }
}