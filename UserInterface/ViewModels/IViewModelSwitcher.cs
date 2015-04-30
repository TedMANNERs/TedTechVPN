namespace TedTechVpn.UserInterface.ViewModels
{
    public interface IViewModelSwitcher
    {
        IViewModel CurrentView { get; set; }
    }
}