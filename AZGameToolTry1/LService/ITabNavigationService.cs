using AZGameToolTry1.Model;
using System;

namespace AZGameToolTry1.LService
{
    public delegate void TabNameSentHandler(String s, TabNavigationEventArgs e);

    public interface ITabNavigationService
    {
        event TabNameSentHandler TabNameSent;

        void GoToTab(string tabName, Object parameter);
    }
}