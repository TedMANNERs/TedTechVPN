﻿using System.Windows.Input;

namespace UserInterface
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;

        public MainViewModel()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.Switch += SwitchView;
            CurrentView = loginViewModel;
        }

        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand ViewLoginCommand { get; set; }
        public ICommand ViewAppCommand { get; set; }

        private void SwitchView(object sender, SwitchViewEventArgs e)
        {
            CurrentView = e.ViewModel;
        }
    }
}