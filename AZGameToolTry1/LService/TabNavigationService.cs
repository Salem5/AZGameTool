using AZGameToolTry1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZGameToolTry1.LService
{
    public class TabNavigationService : ITabNavigationService
    {
        public void NavigateToTab(string tabName, Object parameter)
        => TabNavigatedTo?.Invoke(tabName, new TabNavigationEventArgs(parameter));

        public void CreateTab(string tabName, object parameter)
         => TabCreated?.Invoke(tabName, new TabNavigationEventArgs(parameter));

        public event TabNavigationHandler TabNavigatedTo;
        public event TabNavigationHandler TabCreated;
    }
}
