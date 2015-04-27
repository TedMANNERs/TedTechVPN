using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public partial class VpnConnection : INotifyPropertyChanged
    {
        private bool _hasError;

        public bool HasError
        {
            get { return _hasError; }
            set
            {
                _hasError = value; 
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}