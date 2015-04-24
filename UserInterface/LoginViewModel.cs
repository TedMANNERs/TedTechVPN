using System.Windows.Input;

namespace UserInterface
{
    public class LoginViewModel : ViewModelBase, IViewModel
    {
        public LoginViewModel()
        {
            Name = "Login";
            LoginCommand = new DelegateCommand(obj => Login(), () => !string.IsNullOrEmpty(Username));
        }

        public ICommand LoginCommand { get; set; }

        private void Login()
        {
            RequestSwitch(new SwitchViewEventArgs("App"));
        }
    }
}