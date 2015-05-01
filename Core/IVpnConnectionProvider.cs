using TedTechVpn.Model;

namespace TedTechVpn.Core
{
    public interface IVpnConnectionProvider
    {
        bool Connect(VpnConnection selectedConnection);
        void Disconnect();
    }
}