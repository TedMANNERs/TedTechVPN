using System.Windows.Input;

namespace UserInterface
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(obj => Login(), () => !string.IsNullOrEmpty(Username));
        }

        public ICommand LoginCommand { get; set; }

        private void Login()
        {
            AppViewModel appViewModel = VpnKernel.Get<AppViewModel>();
            appViewModel.Username = Username;
            OnSwitch(new SwitchViewEventArgs(appViewModel));
        }
    }
}