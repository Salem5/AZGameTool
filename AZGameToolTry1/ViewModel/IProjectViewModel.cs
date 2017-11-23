using System.Collections.ObjectModel;
using System.Windows.Input;
using AZGameToolTry1.Model;

namespace AZGameToolTry1.ViewModel
{
    public interface IProjectViewModel
    {
        ObservableCollection<BaseMd> ProjectFiles { get; set; }
        ICommand ViewTeamCommand { get; }
    }
}