using System.Windows;
using System.Windows.Controls;

namespace UserInterface
{
    /// <summary>
    ///     Interaktionslogik für LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void VpnPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginViewModel loginViewModel = (LoginViewModel) DataContext;
            loginViewModel.Password = VpnPasswordBox.SecurePassword;
        }
    }
}