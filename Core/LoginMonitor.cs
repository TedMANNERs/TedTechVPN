using System.Linq;
using System.Security;
using System.Text;
using Model;

namespace Core
{
    public class LoginMonitor : ILoginMonitor
    {
        private readonly IPasswordProvider _passwordProvider;

        public LoginMonitor(IPasswordProvider passwordProvider)
        {
            _passwordProvider = passwordProvider;
            Password = new SecureString();
        }

        public string Username { get; set; }
        public SecureString Password { get; set; }

        public void Logout()
        {
            Password.Clear();
            Username = string.Empty;
        }

        public bool Login()
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            using (TedTechVPNEntities dbContext = new TedTechVPNEntities())
            {
                User user = dbContext.User.FirstOrDefault(u => u.Name == Username);

                return user != null &&
                       user.Password ==
                       encoding.GetString(_passwordProvider.Hash(Password, encoding.GetBytes(user.Salt)));
            }
        }
    }

    public interface ILoginMonitor
    {
        string Username { get; set; }
        SecureString Password { get; set; }
        void Logout();
        bool Login();
    }
}