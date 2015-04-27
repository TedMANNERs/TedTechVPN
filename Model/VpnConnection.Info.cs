using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public partial class VpnConnection : INotifyPropertyChanged
    {
        private bool _hasError;
        private bool _isEstablished;

        public bool HasError
        {
            get { return _hasError; }
            set
            {
                _hasError = value; 
                OnPropertyChanged();
            }
        }

        public bool IsEstablished
        {
            get { return _isEstablished; }
            set
            {
                _isEstablished = value; 
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