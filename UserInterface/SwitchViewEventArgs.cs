using System;

namespace TedTechVpn.UserInterface
{
    public class SwitchViewEventArgs : EventArgs
    {
        public SwitchViewEventArgs(string viewModel)
        {
            ViewModel = viewModel;
        }

        public string ViewModel { get; set; }
    }
}