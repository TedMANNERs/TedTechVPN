using System.ComponentModel;
using System.Runtime.CompilerServices;
using UserInterface.Annotations;

namespace UserInterface
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public delegate void SwitchViewEventHandler(object sender, SwitchViewEventArgs e);

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

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