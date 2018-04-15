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
    public class ProjectViewModel : BaseViewModel, IProjectViewModel
    {
        private readonly IStatusNotificationService statusNotificationService;
        private readonly IProjectDataService projectDataService;
        private readonly ITabNavigationService tabNavigationService;

        public ICommand ViewTeamCommand { get; }
        public ICommand ViewReadmeCommand { get; }

        public ProjectViewModel(IStatusNotificationService statusNotificationServiceParam, IProjectDataService projectDataServiceParam, ITabNavigationService tabNavigationServiceParam)
        {
            ProjectFiles = new ObservableCollection<BaseMd>();

            tabNavigationService = tabNavigationServiceParam;
            statusNotificationService = statusNotificationServiceParam;
            projectDataService = projectDataServiceParam;
            projectDataService.ProjectLoaded += ProjectDataService_ProjectLoaded;

            ViewTeamCommand = new AnotherCommandImplementation(t =>
            {
                statusNotificationServiceParam.SendMessage(new Notification() { Kind = NotificationKind.Other, Message = t.ToString(), Title = "DEBUG Team", Time = DateTime.Now });
            });

            ViewReadmeCommand = new AnotherCommandImplementation(r =>
            {
                if (r is ReadMe readMe)
                {                    
                    tabNavigationService.CreateTab("ReadMeTab", r);
                }
            });
        }

        private void ProjectDataService_ProjectLoaded(RecentProject p, EventArgs e)
        {
            ProjectFiles.Clear();
            foreach (BaseMd fileMd in projectDataService.ProjectFiles)
            {
                // Scan for a tag that describes the type.
                // TODO: create the proper model classes to assign them in here.
                //File.OpenRead(filePath);                
                ProjectFiles.Add(fileMd);
            }
        }

        ObservableCollection<BaseMd> projectFiles;

        public ObservableCollection<BaseMd> ProjectFiles
        {
            get { return projectFiles; }
            set { SetValue<ObservableCollection<BaseMd>>(value, ref projectFiles); }
        }
        
        protected override void Dispose(bool disposing)
        {
            projectDataService.ProjectLoaded -= ProjectDataService_ProjectLoaded;
            base.Dispose(disposing);
        }
    }
}