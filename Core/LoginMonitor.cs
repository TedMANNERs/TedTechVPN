using System.Linq;
using System.Security;
using System.Text;
using TedTechVpn.Model;

namespace TedTechVpn.Core
{
    public class LoginMonitor : ILoginMonitor
    {
        private readonly IPasswordProvider _passwordProvider;

        public LoginMonitor(IPasswordProvider passwordProvider)
        {
            _passwordProvider = passwordProvider;
            SecurePassword = new SecureString();
            User = new User();
        }

        public User User { get; set; }
        public SecureString SecurePassword { get; set; }

        public void Logout()
        {
            SecurePassword.Clear();
            User.Name = string.Empty;
        }

        public bool Login()
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            using (TedTechVPNEntities dbContext = new TedTechVPNEntities())
            {
                User user = dbContext.User.FirstOrDefault(u => u.Name == User.Name);
                if (user == null || user.Password !=
                    encoding.GetString(_passwordProvider.Hash(SecurePassword, encoding.GetBytes(user.Salt))))
                {
                    return false;
                }

                User.IsPrivileged = user.IsPrivileged;
                return true;
            }
        }
    }
}