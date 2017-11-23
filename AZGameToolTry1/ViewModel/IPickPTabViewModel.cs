using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using AZGameToolTry1.Model;

namespace AZGameToolTry1.ViewModel
{
    public interface IPickPTabViewModel : INotifyPropertyChanged
    {
        ICommand ProjectSelectCommand { get; }
        IEnumerable<RecentProject> RecentProjectCollection { get; }
    }
}