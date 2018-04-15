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
    public class MainViewModel : BaseViewModel
    {
        readonly IStatusNotificationService statusNotificationService;
        readonly ITabNavigationService tabNavigationService;
        readonly IProjectDataService projectDataService;

        ISnackbarMessageQueue messageQueue;

        public ICommand OpenAboutMeCommand { get; }

        public ISnackbarMessageQueue MessageQueue
        {
            get => messageQueue;
            private set => SetValue<ISnackbarMessageQueue>(value, ref messageQueue);
        }

        public bool DebugVisible
        {
            get
            {
                //#if DEBUG
                //                return true;
                //#else
                //                return false;
                //#endif
                return App.DebugVisible;
            }
            set
            {
                App.DebugVisible = value;
                RaisePropertyChanged(nameof(DebugVisible));
            }

        }

        public int? badgeCount;
        public int? BadgeCount
        {
            get => badgeCount;
            set => SetValue<int?>(value, ref badgeCount);
        }

        public String loadedProject;
        public String LoadedProject
        {
            get => loadedProject;
            set => SetValue<String>(value, ref loadedProject);
        }


        public bool pickerTabControl = true;
        public bool PickerTabControl
        {
            get
            {
                return pickerTabControl;
            }
            set
            {
                SetValue<bool>(value, ref pickerTabControl);
            }
        }

        public bool projectTabControl;
        public bool ProjectTabControl
        {
            get
            {
                return projectTabControl;
            }
            set
            {
                SetValue<bool>(value, ref projectTabControl);
            }
        }

        public bool settingsTabControl;
        public bool SettingsTabControl
        {
            get
            {
                return settingsTabControl;
            }
            set
            {
                SetValue<bool>(value, ref settingsTabControl);
            }
        }

        public bool notificationTabControl;
        public bool NotificationTabControl
        {
            get
            {
                return notificationTabControl;
            }
            set
            {
                SetValue<bool>(value, ref notificationTabControl);
                if (value)
                {
                    statusNotificationService.ClearBadgeCount();
                }
            }
        }

        public MainViewModel(IStatusNotificationService statusNotificationServiceParam,
            ITabNavigationService tabNavigationServiceParam,
            IProjectDataService projectDataServiceParam,
            ISnackbarMessageQueue snackbarMessageQueueParam)
        {
            MessageQueue = snackbarMessageQueueParam;
            statusNotificationService = statusNotificationServiceParam;
            tabNavigationService = tabNavigationServiceParam;
            projectDataService = projectDataServiceParam;

            OpenAboutMeCommand = new AnotherCommandImplementation(t =>
            {
                string aboutMeFileName = "AboutMe.md";
                string aboutMeFullPath = Path.Combine(Environment.CurrentDirectory, aboutMeFileName);
                tabNavigationService.CreateTab("AboutMe",
                    new ReadMe()
                    {
                        FileName = aboutMeFileName,
                        FullFileName = aboutMeFullPath,
                        Text = projectDataService.CreateMarkup(aboutMeFullPath),
                        LastUpdate = File.GetLastWriteTime(aboutMeFullPath)
                    });
            });

            statusNotificationService.MessageSent += StatusNotificationService_MessageSent;
            statusNotificationService.BadgeCountCleared += StatusNotificationService_BadgeCountCleared;
            tabNavigationService.TabNavigatedTo += TabNavigationService_TabNavigatedTo;
            projectDataService.ProjectLoaded += ProjectDataService_ProjectLoaded;
        }

        private void StatusNotificationService_BadgeCountCleared(EventArgs e)
        {
            BadgeCount = null;
        }

        private void ProjectDataService_ProjectLoaded(RecentProject p, EventArgs e)
        {
            LoadedProject = p.Name;
        }

        private void TabNavigationService_TabNavigatedTo(string s, TabNavigationEventArgs e)
        {
            switch (s)
            {
                case nameof(SettingsTabControl):
                    {
                        SettingsTabControl = true;
                    }
                    break;
                case nameof(PickerTabControl):
                    {
                        PickerTabControl = true;
                    }
                    break;
                case nameof(ProjectTabControl):
                    {
                        ProjectTabControl = true;
                    }
                    break;
                case nameof(NotificationTabControl):
                    {
                        NotificationTabControl = true;
                        statusNotificationService.ClearBadgeCount();
                    }
                    break;
                default:
                    break;
            }
        }

        private void StatusNotificationService_MessageSent(Notification n, EventArgs e)
        {
            bool promote = (n.Kind == NotificationKind.Error);

            MessageQueue.Enqueue(n.Title, "Details", () =>
            {
                tabNavigationService.NavigateToTab(nameof(NotificationTabControl), n);
            }, promote);

            BadgeCount = BadgeCount + 1 ?? 1;
        }

        protected override void Dispose(bool disposing)
        {
            statusNotificationService.MessageSent -= StatusNotificationService_MessageSent;
            statusNotificationService.BadgeCountCleared -= StatusNotificationService_BadgeCountCleared;
            tabNavigationService.TabNavigatedTo -= TabNavigationService_TabNavigatedTo;
            projectDataService.ProjectLoaded -= ProjectDataService_ProjectLoaded;
            base.Dispose(disposing);
        }
    }
}