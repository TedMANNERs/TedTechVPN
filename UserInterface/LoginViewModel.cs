using System.Data.Entity.Core;
using System.Security;
using System.Windows.Input;
using Core;

namespace UserInterface
{
    public class LoginViewModel : ViewModelBase, IViewModel
    {
        private string _errorMessage;

        public LoginViewModel(ILoginMonitor loginMonitor)
        {
            LoginMonitor = loginMonitor;
            Name = "Login";
            LoginCommand = new DelegateCommand(obj => Login(),
                () => !string.IsNullOrEmpty(LoginMonitor.Username) && LoginMonitor.Password.Length > 0);
        }

        public ICommand LoginCommand { get; set; }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        private void Login()
        {
            try
            {
                bool loginSuccessful = LoginMonitor.Login();
                if (loginSuccessful)
                {
                    ErrorMessage = string.Empty;
                    RequestSwitch(new SwitchViewEventArgs("App"));
                }
                else
                {
                    ErrorMessage = "Login incorrect";
                }
            }
            catch (EntityException e)
            {
                ErrorMessage = e.InnerException.Message;
            }
        }
    }
}