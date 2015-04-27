using System.ComponentModel;
using System.Runtime.CompilerServices;
using Core;
using UserInterface.Annotations;

namespace UserInterface
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public delegate void SwitchViewEventHandler(object sender, SwitchViewEventArgs e);

        public ILoginMonitor LoginMonitor { get; set; }

        public string Name { get; set; }

        protected virtual void RequestSwitch(SwitchViewEventArgs e)
        {
            SwitchViewEventHandler eventHandler = Switch;
            if (eventHandler != null) eventHandler(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event SwitchViewEventHandler Switch;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}