using System;

namespace UserInterface
{
    public class SwitchViewEventArgs : EventArgs
    {
        public SwitchViewEventArgs(ViewModelBase viewModel)
        {
            ViewModel = viewModel;
        }

        public ViewModelBase ViewModel { get; set; }
    }
}