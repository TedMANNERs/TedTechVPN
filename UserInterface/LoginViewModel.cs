using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Windows.Input;

namespace UserInterface
{
    public class LoginViewModel : ViewModelBase, IViewModel
    {
        public LoginViewModel()
        {
            Name = "Login";
            LoginCommand = new DelegateCommand(obj => Login(), () => !string.IsNullOrEmpty(Username));
        }

        public ICommand LoginCommand { get; set; }
        public SecureString Password { get; set; }

        private void Login()
        {
            byte[] salt = GenerateSalt();
            byte[] hash = Hash(salt);
            RequestSwitch(new SwitchViewEventArgs("App"));
        }

        private byte[] Hash(byte[] salt)
        {
            HashAlgorithm algorithm = new SHA512Cng();
            byte[] hash = new byte[Password.Length + salt.Length];
            byte[] passwordBytes = SecureStringToByteArray();

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

        private byte[] SecureStringToByteArray()
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                byte[] result = new byte[Password.Length];
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(Password);
                for (int i = 0; i < Password.Length; ++i)
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

        private byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[32];
            rng.GetNonZeroBytes(bytes);
            return bytes;
        }
    }
}