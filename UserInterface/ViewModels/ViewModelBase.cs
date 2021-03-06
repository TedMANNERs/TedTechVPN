﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using TedTechVpn.Core;
using TedTechVpn.UserInterface.Properties;

namespace TedTechVpn.UserInterface.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public delegate void SwitchViewEventHandler(object sender, SwitchViewEventArgs e);

        public ILoginMonitor LoginMonitor { get; set; }
        public string Name { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RequestSwitch(SwitchViewEventArgs e)
        {
            SwitchViewEventHandler eventHandler = Switch;
            if (eventHandler != null) eventHandler(this, e);
        }

        public event SwitchViewEventHandler Switch;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}