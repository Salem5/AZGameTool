using AZGameToolTry1.LService;
using AZGameToolTry1.ViewModel;
using AZGameToolTry1.Controls;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace AZGameToolTry1.InjectionModules
{
    public class NinjectModuleWindowsX64 : NinjectModule
    {
        public override void Load()
        {
            Bind < IProjectDataService>().To<ProjectDataService>().InSingletonScope();
            Bind<IDatabaseService>().To<DatabaseService>().InSingletonScope();
            Bind<ITabNavigationService>().To<TabNavigationService>().InSingletonScope();
            Bind<IStatusNotificationService>().To<StatusNotificationService>().InSingletonScope();
            Bind<IPickPTabViewModel>().To<PickPTabViewModel>().InSingletonScope();
            Bind<ISnackbarMessageQueue>().To<SnackbarMessageQueue>().InSingletonScope();
            Bind<IProjectViewModel>().To<ProjectViewModel>().InSingletonScope();
            
            Bind<MainWindow>().ToSelf();
        }
    }
}
