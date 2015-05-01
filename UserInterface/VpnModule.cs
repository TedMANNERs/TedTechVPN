﻿using Core;
using Ninject.Modules;

namespace UserInterface
{
    public class VpnModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoginMonitor>().To<LoginMonitor>().InSingletonScope();
            Bind<IVpnConnectionProvider>().To<VpnConnectionProvider>().InSingletonScope();
            Bind<IPasswordProvider>().To<PasswordProvider>();
            Bind<IPermissionManager>().To<PermissionManager>();
            Bind<IViewModel>().To<LoginViewModel>().InSingletonScope();
            Bind<IViewModel>().To<AppViewModel>().InSingletonScope().OnActivation(x => x.Load());
            Bind<IViewModelSwitcher>().To<ViewModelSwitcher>().InSingletonScope();
            Bind<ViewModelBase>().To<MainViewModel>().InSingletonScope();
            Bind<ViewModelBase>().To<LoginViewModel>().InSingletonScope();
            Bind<ViewModelBase>().To<AppViewModel>().InSingletonScope();
        }
    }
}