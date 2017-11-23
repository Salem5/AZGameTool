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
        public void GoToTab(string tabName, Object parameter)
        {
            TabNameSent?.Invoke(tabName, new TabNavigationEventArgs(parameter)  );
        }

        public event TabNameSentHandler TabNameSent;
        
    }
}
