using System.Windows.Controls;
using TedTechVpn.UserInterface.ViewModels;

namespace TedTechVpn.UserInterface
{
    public partial class AppView
    {
        public AppView()
        {
            InitializeComponent();
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            AppViewModel appViewModel = (AppViewModel) DataContext;
            appViewModel.SaveConnectionChanges();
        }
    }
}