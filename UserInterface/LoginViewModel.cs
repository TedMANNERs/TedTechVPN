using System.Data.Entity.Core;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Input;
using Model;

namespace UserInterface
{
    public class LoginViewModel : ViewModelBase, IViewModel
    {
        private string _errorMessage;

        public LoginViewModel()
        {
            Name = "Login";
            Password = new SecureString();
            LoginCommand = new DelegateCommand(obj => Login(),
                () => !string.IsNullOrEmpty(Username) && Password.Length > 0);
        }

        public ICommand LoginCommand { get; set; }
        public SecureString Password { get; set; }

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
            IPasswordProvider provider = new PasswordProvider();
            UnicodeEncoding encoding = new UnicodeEncoding();

            try
            {
                using (TedTechVPNEntities dbContext = new TedTechVPNEntities())
                {
                    User user = dbContext.User.FirstOrDefault(u => u.Name == Username);

                    if (user != null &&
                        user.Password == encoding.GetString(provider.Hash(Password, encoding.GetBytes(user.Salt))))
                    {
                        ErrorMessage = string.Empty;
                        RequestSwitch(new SwitchViewEventArgs("App"));
                    }
                    else
                        ErrorMessage = "User does not exist";
                }
            }
            catch (EntityException e)
            {
                ErrorMessage = e.InnerException.Message;
            }
        }
    }
}