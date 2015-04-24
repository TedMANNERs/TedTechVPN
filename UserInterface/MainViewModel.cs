namespace UserInterface
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Switcher = VpnKernel.Get<IViewModelSwitcher>();
        }

        public IViewModelSwitcher Switcher { get; private set; }
    }
}