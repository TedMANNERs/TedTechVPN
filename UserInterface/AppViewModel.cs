using System.Windows.Input;

namespace UserInterface
{
    public class AppViewModel : ViewModelBase
    {
        public ICommand LogoutCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
    }
}