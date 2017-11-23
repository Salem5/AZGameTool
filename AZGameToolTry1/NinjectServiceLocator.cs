using AZGameToolTry1.InjectionModules;
using AZGameToolTry1.LService;
using AZGameToolTry1.ViewModel;
using MaterialDesignColors.WpfExample;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZGameToolTry1
{
    public sealed class NinjectServiceLocator
    {
        private static readonly IKernel Kernel = new StandardKernel(new NinjectModuleWindowsX64());

        private static NinjectServiceLocator instance = new NinjectServiceLocator();
        public static NinjectServiceLocator Instance
        {
            get
            { return instance; }
        }

        public static T GetStuff<T>()
        {
            return Kernel.Get<T>();
        }

        // Properties of ViewModels, so that XAML code can retrieve these directly.        

        // Interfaces for things that might be a pain during design time.
        static public IPickPTabViewModel IPickPTabViewModel => GetStuff<IPickPTabViewModel>();
        static public IProjectViewModel ProjectViewModel => GetStuff<IProjectViewModel>();
        static public PaletteSelectorViewModel PaletteSelectorViewModel => GetStuff<PaletteSelectorViewModel>();
        static public SettingsViewModel SettingsViewModel => GetStuff<SettingsViewModel>();
        static public MainViewModel MainViewModel => GetStuff<MainViewModel>();        
        static public NotificationViewModel NotificationViewModel => GetStuff<NotificationViewModel>();
    }
}