using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Model;

namespace UserInterface
{
    public class AppViewModel : ViewModelBase, IViewModel
    {
        public AppViewModel()
        {
            Name = "App";
            VpnConnections = new ObservableCollection<VpnConnection>();
            LogoutCommand = new DelegateCommand(obj => Logout(), () => true);
            NewCommand = new DelegateCommand(obj => CreateConnection(), () => true);
            RemoveCommand = new DelegateCommand(obj => RemoveConnection(), () => SelectedConnection != null);
            ConnectCommand = new DelegateCommand(obj => Connect(), () => SelectedConnection != null);
        }

        public ObservableCollection<VpnConnection> VpnConnections { get; set; }
        public VpnConnection SelectedConnection { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ConnectCommand { get; set; }

        private void Connect()
        {
            throw new NotImplementedException();
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