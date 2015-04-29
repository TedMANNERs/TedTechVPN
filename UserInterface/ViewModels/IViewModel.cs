using System.Runtime.CompilerServices;

namespace UserInterface.ViewModels
{
    public interface IViewModel
    {
        string Name { get; set; }
        event ViewModelBase.SwitchViewEventHandler Switch;
        void OnPropertyChanged([CallerMemberName] string propertyName = null);
    }
}