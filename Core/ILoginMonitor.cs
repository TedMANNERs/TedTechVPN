using System.Security;
using TedTechVpn.Model;

namespace TedTechVpn.Core
{
    public interface ILoginMonitor
    {
        User User { get; set; }
        SecureString SecurePassword { get; set; }
        void Logout();
        bool Login();
    }
}