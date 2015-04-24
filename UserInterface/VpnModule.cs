using Ninject.Modules;

namespace UserInterface
{
    public class VpnModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ViewModelBase>().To<MainViewModel>().InSingletonScope().OnActivation(x => x.Load());
            Bind<ViewModelBase>().To<LoginViewModel>().InSingletonScope();
            Bind<ViewModelBase>().To<AppViewModel>().InSingletonScope();
        }
    }
}