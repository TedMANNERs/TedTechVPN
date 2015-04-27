using System.Collections.ObjectModel;
using System.Windows.Input;
using Core;
using Model;

namespace UserInterface
{
    public class AppViewModel : ViewModelBase, IViewModel
    {
        private readonly IVpnConnectionProvider _vpnProvider;
        private bool _isConnected;

        public AppViewModel(IVpnConnectionProvider vpnProvider)
        {
            _vpnProvider = vpnProvider;
            Name = "App";
            VpnConnections = new ObservableCollection<VpnConnection>();
            LogoutCommand = new DelegateCommand(obj => Logout(), () => true);
            NewCommand = new DelegateCommand(obj => CreateConnection(), () => true);
            RemoveCommand = new DelegateCommand(obj => RemoveConnection(), () => SelectedConnection != null);
            ConnectCommand = new DelegateCommand(obj => Connect(), () => SelectedConnection != null);
            DisconnectCommand = new DelegateCommand(obj => Disconnect(), () => true);
        }

        public ObservableCollection<VpnConnection> VpnConnections { get; set; }
        public VpnConnection SelectedConnection { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ConnectCommand { get; set; }
        public ICommand DisconnectCommand { get; set; }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        private void Connect()
        {
            _vpnProvider.Connect(SelectedConnection);
            IsConnected = true;
        }

        private void Disconnect()
        {
            _vpnProvider.Disconnect();
            IsConnected = false;
        }

        private void CreateConnection()
        {
            VpnConnections.Add(new VpnConnection());
        }

        private void RemoveConnection()
        {
            VpnConnections.Remove(SelectedConnection);
        }

        private void Logout()
        {
            RequestSwitch(new SwitchViewEventArgs("Login"));
        }
    }
}