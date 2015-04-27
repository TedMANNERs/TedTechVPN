using System.Security;

namespace Core
{
    public interface ILoginMonitor
    {
        string Username { get; set; }
        SecureString Password { get; set; }
        void Logout();
        bool Login();
    }
}