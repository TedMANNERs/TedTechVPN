using Model;

namespace Core
{
    public interface IVpnConnectionProvider
    {
        bool Connect(VpnConnection selectedConnection);
        void Disconnect();
    }
}