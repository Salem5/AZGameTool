using AZGameToolTry1.LService;
using AZGameToolTry1.Model;
using LibGit2Sharp;
using LiteDB;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.WindowsAPICodePack.Dialogs;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AZGameToolTry1.ViewModel
{
    public class DesignTimeProjectViewModel : BaseViewModel, IProjectViewModel
    {
      
        public ICommand ViewTeamCommand { get; }

        public DesignTimeProjectViewModel()
        {
         
        }

        
        private void ProjectDataService_ProjectLoaded(RecentProject p, EventArgs e)
        {
           
        }

        ObservableCollection<BaseMd> projectFiles = new ObservableCollection<BaseMd>() { new Team(), new ReadMe() { Text = "Loadsa Text." } };

        public ObservableCollection<BaseMd> ProjectFiles
        {
            get { return projectFiles; }
            set { SetValue<ObservableCollection<BaseMd>>(value, ref projectFiles); }
        } 

        protected override void Dispose(bool disposing)
        {
           
            base.Dispose(disposing);
        }
    }
}