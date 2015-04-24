using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Model;

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
            if (Password == null || Password.Length <= 0) return;

            IPasswordProvider provider = new PasswordProvider();
            UnicodeEncoding encoding = new UnicodeEncoding();

            using (TedTechVPNEntities dbContext = new TedTechVPNEntities())
            {
                User user = dbContext.User.FirstOrDefault(u => u.Name == Username);
                
                if (user != null && user.Password == encoding.GetString(provider.Hash(Password, encoding.GetBytes(user.Salt))))
                {
                    Console.WriteLine("Success");
                    RequestSwitch(new SwitchViewEventArgs("App"));
                }
            }
        }
    }
}