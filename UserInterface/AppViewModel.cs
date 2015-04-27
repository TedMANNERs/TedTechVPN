using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Core;
using Model;

namespace UserInterface
{
    public class AppViewModel : ViewModelBase, IViewModel
    {
        private readonly ILoginMonitor _loginMonitor;
        private readonly IVpnConnectionProvider _vpnProvider;
        private bool _isConnected;

        public AppViewModel(IVpnConnectionProvider vpnProvider, ILoginMonitor loginMonitor)
        {
            _vpnProvider = vpnProvider;
            _loginMonitor = loginMonitor;
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


        public void Load()
        {
            using (TedTechVPNEntities dbContext = new TedTechVPNEntities())
            {
                VpnConnections = new ObservableCollection<VpnConnection>(dbContext.VpnConnection.Where(x => x.IsActive));
            }
        }

        private void Connect()
        {
            IsConnected = _vpnProvider.Connect(SelectedConnection);
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
            _loginMonitor.Logout();
            Disconnect();
            RequestSwitch(new SwitchViewEventArgs("Login"));
        }
    }
}