using AZGameToolTry1.Controls;
using AZGameToolTry1.InjectionModules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AZGameToolTry1
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public static string DbPath { get; } = @"MyData.db";
        public static bool DebugVisible { get; internal set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NinjectServiceLocator.GetStuff<MainWindow>().Show();
        }
    }
}
