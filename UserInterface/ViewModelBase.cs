using System.ComponentModel;
using System.Runtime.CompilerServices;
using UserInterface.Annotations;

namespace UserInterface
{
    public class ViewModelBase : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event SwitchViewEventHandler Switch;

        protected virtual void OnSwitch(SwitchViewEventArgs e)
        {
            SwitchViewEventHandler eventHandler = Switch;
            if (eventHandler != null) eventHandler(this, e);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}