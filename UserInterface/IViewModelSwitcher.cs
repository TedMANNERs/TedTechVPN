namespace UserInterface
{
    public interface IViewModelSwitcher
    {
        IViewModel CurrentView { get; set; }
    }
}