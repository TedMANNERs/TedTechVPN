using System.Security;

namespace TedTechVpn.Core
{
    public interface ILoginMonitor
    {
        string Username { get; set; }
        SecureString Password { get; set; }
        void Logout();
        bool Login();
    }
}