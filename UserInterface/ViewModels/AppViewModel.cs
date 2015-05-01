using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TedTechVpn.Core;
using TedTechVpn.Model;

namespace TedTechVpn.UserInterface.ViewModels
{
    public class AppViewModel : ViewModelBase, IViewModel
    {
        private readonly IVpnConnectionProvider _vpnProvider;
        private bool _isConnected;

        public AppViewModel(IVpnConnectionProvider vpnProvider, ILoginMonitor loginMonitor)
        {
            _vpnProvider = vpnProvider;
            LoginMonitor = loginMonitor;
            Name = "App";
            VpnConnections = new ObservableCollection<VpnConnectionInfo>();
            LogoutCommand = new DelegateCommand(obj => Logout(), () => true);
            NewCommand = new DelegateCommand(obj => CreateConnection(), () => LoginMonitor.User.IsPrivileged);
            RemoveCommand = new DelegateCommand(obj => RemoveConnection(),
                () => SelectedConnection != null && LoginMonitor.User.IsPrivileged);
            ConnectCommand = new DelegateCommand(obj => Connect(), () => SelectedConnection != null);
            DisconnectCommand = new DelegateCommand(obj => Disconnect(), () => true);
        }

        public ObservableCollection<VpnConnectionInfo> VpnConnections { get; set; }
        public VpnConnectionInfo SelectedConnection { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ConnectCommand { get; set; }
        public ICommand DisconnectCommand { get; set; }

        public bool IsReadOnly
        {
            get { return !LoginMonitor.User.IsPrivileged; }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        public void Load()
        {
            using (TedTechVPNEntities dbContext = new TedTechVPNEntities())
            {
                VpnConnections = new ObservableCollection<VpnConnectionInfo>();
                foreach (VpnConnection connection in dbContext.VpnConnection.Where(x => x.IsActive))
                {
                    VpnConnections.Add(new VpnConnectionInfo(connection));
                }
            }
        }

        private void Connect()
        {
            IsConnected = _vpnProvider.Connect(SelectedConnection.VpnConnection);
            SelectedConnection.HasError = !IsConnected;
            SelectedConnection.IsEstablished = IsConnected;
        }

        private void Disconnect()
        {
            _vpnProvider.Disconnect();
            IsConnected = false;
            if (SelectedConnection != null)
                SelectedConnection.IsEstablished = IsConnected;
        }

        private void CreateConnection()
        {
            VpnConnections.Add(new VpnConnectionInfo());
        }

        private void RemoveConnection()
        {
            VpnConnections.Remove(SelectedConnection);
        }

        private void Logout()
        {
            LoginMonitor.Logout();
            LoginMonitor.User = new User();
            Disconnect();
            RequestSwitch(new SwitchViewEventArgs("Login"));
        }
    }
}