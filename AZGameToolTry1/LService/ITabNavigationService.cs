using AZGameToolTry1.Model;
using System;

namespace AZGameToolTry1.LService
{
    public delegate void TabNavigationHandler(String s, TabNavigationEventArgs e);

    public interface ITabNavigationService
    {
        event TabNavigationHandler TabNavigatedTo;

        event TabNavigationHandler TabCreated;

        void NavigateToTab(string tabName, Object parameter);

        void CreateTab(string tabName, Object parameter);
    }
}