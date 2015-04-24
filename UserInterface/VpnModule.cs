using Ninject.Modules;

namespace UserInterface
{
    public class VpnModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IViewModelSwitcher>().To<ViewModelSwitcher>().InSingletonScope();
            Bind<IViewModel>().To<LoginViewModel>();
            Bind<IViewModel>().To<AppViewModel>();
            Bind<ViewModelBase>().To<MainViewModel>().InSingletonScope();
            Bind<ViewModelBase>().To<LoginViewModel>().InSingletonScope();
            Bind<ViewModelBase>().To<AppViewModel>().InSingletonScope();
        }
    }
}