using Model;

namespace Core
{
    public interface IVpnConnectionProvider
    {
        void Connect(VpnConnection selectedConnection);
        void Disconnect();
    }
}