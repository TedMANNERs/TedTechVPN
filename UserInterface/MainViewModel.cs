using System.Windows.Input;

namespace UserInterface
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            CurrentView = new LoginViewModel();
        }

        public ViewModelBase CurrentView { get; set; }
        public ICommand ViewLoginCommand { get; set; }
        public ICommand ViewAppCommand { get; set; }
    }
}