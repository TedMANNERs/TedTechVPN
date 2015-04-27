﻿using Core;
using Ninject.Modules;

namespace UserInterface
{
    public class VpnModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IViewModelSwitcher>().To<ViewModelSwitcher>().InSingletonScope();
            Bind<IViewModel>().To<LoginViewModel>().InSingletonScope();
            Bind<IViewModel>().To<AppViewModel>().InSingletonScope();
            Bind<ViewModelBase>().To<MainViewModel>().InSingletonScope();
            Bind<ViewModelBase>().To<LoginViewModel>().InSingletonScope();
            Bind<ViewModelBase>().To<AppViewModel>().InSingletonScope();
            Bind<IPasswordProvider>().To<PasswordProvider>();
            Bind<IVpnConnectionProvider>().To<VpnConnectionProvider>();
        }
    }
}