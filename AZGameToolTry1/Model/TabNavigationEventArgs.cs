using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZGameToolTry1.Model
{
    public class TabNavigationEventArgs : EventArgs
    {
        public Object Parameter { get; }

        public TabNavigationEventArgs(Object parameter)
        {
            Parameter = parameter;
        }
    }
}
