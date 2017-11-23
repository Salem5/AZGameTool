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
using System.Windows;
using System.Windows.Input;

namespace AZGameToolTry1.ViewModel
{
    public class PickPTabViewModel : BaseViewModel, IPickPTabViewModel
    {
        readonly IStatusNotificationService statusNotificationService;
        readonly ITabNavigationService tabNavigationService;
        readonly IDatabaseService databaseService;
        readonly IProjectDataService projectDataService;

        public PickPTabViewModel(IStatusNotificationService statusNotificationServiceParam,
            ITabNavigationService tabNavigationServiceParam,
            IDatabaseService databaseServiceParam,
            IProjectDataService projectDataServiceParam)
        {
            statusNotificationService = statusNotificationServiceParam;
            tabNavigationService = tabNavigationServiceParam;
            databaseService = databaseServiceParam;
            projectDataService = projectDataServiceParam;
            databaseService.DatabaseChange += DatabaseService_DatabaseChange;

            //Initialize commands.
            ProjectCreateCommand = new AnotherCommandImplementation(t =>
            {
            });

            ProjectOpenCommand = new AnotherCommandImplementation(t =>
            {
                if (CommonFileDialog.IsPlatformSupported)
                {
                    CommonOpenFileDialog folderDialog = new CommonOpenFileDialog("Project Picker")
                    {
                        IsFolderPicker = true,
                    };

                    var diagRes = folderDialog.ShowDialog();
                    if (diagRes == CommonFileDialogResult.Ok)
                    {
                        ProjectLoading(folderDialog.FileName);
                    }
                }
                else
                {
                    statusNotificationService.SendMessage(new Notification() { Kind = NotificationKind.Error, Message = "This Application requires Windows Vista(I think) or up, because I simply refuse to also spawn the classic folder picker. Sorry.", Title = "For the love of ***: UPDATE!", Time = DateTime.Now });
                }
            });

            ProjectSelectCommand = new AnotherCommandImplementation(o =>
            {
                if (o is RecentProject project)
                {
                    ProjectLoading(project.Location);
                }
                else
                {
                    statusNotificationService.SendMessage(new Notification() { Kind = NotificationKind.Error, Message = "The picker failed to load the selected project. There might be an issue with the database.", Title = "Loading Failure", Time = DateTime.Now });
                    // TODO: add interaction to remove reference from picker.
                }
            }
            );
        }

        private void ProjectLoading(string path)
        {
            if (projectDataService.LoadProject(path))
            {
                tabNavigationService.GoToTab("ProjectTabControl", null);
            }
            // NOTE: if loading fails when we get here, the service itself should signal what went wrong since it's not specific to the project picker viewmodel.
        }

        private void DatabaseService_DatabaseChange(string s, EventArgs e)
        {
            RaisePropertyChanged(nameof(RecentProjectCollection));
        }

        /// <summary>
        /// Select a valid git repository.
        /// </summary>
        public ICommand ProjectOpenCommand { get; }
        public ICommand ProjectCreateCommand { get; }
        public ICommand ProjectSelectCommand { get; }

        public IEnumerable<RecentProject> RecentProjectCollection
        {
            get
            {
                IEnumerable<RecentProject> res;

                using (var db = new LiteDatabase(App.DbPath))
                {
                    var col = db.GetCollection<RecentProject>();
                    var find = col.Find(Query.All(), 0, 10);
                    res = find.ToList<RecentProject>().OrderByDescending((p) => p.Date);
                }
                return res;
            }
        }

        protected override void Dispose(bool disposing)
        {
            databaseService.DatabaseChange -= DatabaseService_DatabaseChange;
            base.Dispose(disposing);
        }
    }
}