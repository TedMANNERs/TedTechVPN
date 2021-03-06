﻿using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TedTechVpn.UserInterface.Properties;

namespace TedTechVpn.UserInterface.ViewModels
{
    public class ViewModelSwitcher : IViewModelSwitcher, INotifyPropertyChanged
    {
        private readonly IViewModel[] _viewModels;
        private IViewModel _currentView;

        public ViewModelSwitcher(IViewModel[] viewModels)
        {
            _viewModels = viewModels;
            foreach (IViewModel viewModel in viewModels)
            {
                viewModel.Switch += Switch;
            }

            CurrentView = viewModels.First(x => x.Name == "Login");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IViewModel CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public void Switch(object sender, SwitchViewEventArgs e)
        {
            CurrentView = _viewModels.FirstOrDefault(x => x.Name == e.ViewModel);
            if (CurrentView != null && CurrentView.GetType() == typeof (AppViewModel))
                ((AppViewModel) CurrentView).Load();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}