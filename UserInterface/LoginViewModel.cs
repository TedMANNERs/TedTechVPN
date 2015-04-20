using System;
using System.Windows.Input;
using Model;

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
            throw new NotImplementedException();
        }
    }
}