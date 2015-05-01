using System;
using System.Data.Entity.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using TedTechVpn.Core;

namespace TedTechVpn.UserInterface.ViewModels
{
    public class LoginViewModel : ViewModelBase, IViewModel
    {
        private string _errorMessage;

        public LoginViewModel(ILoginMonitor loginMonitor)
        {
            LoginMonitor = loginMonitor;
            Name = "Login";
            LoginCommand = new DelegateCommand(obj => Login(),
                () => !string.IsNullOrEmpty(LoginMonitor.User.Name) && LoginMonitor.SecurePassword.Length > 0);
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
                Task<bool> loginTask = Task<bool>.Factory.StartNew(() => LoginMonitor.Login());
                bool loginSuccessful = loginTask.Result;
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