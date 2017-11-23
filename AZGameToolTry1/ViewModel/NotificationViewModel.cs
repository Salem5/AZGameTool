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
    public class NotificationViewModel : BaseViewModel
    {
        readonly IStatusNotificationService statusNotificationService;
        readonly ITabNavigationService tabNavigationService;
        //readonly IProjectDataService projectDataService;

        public NotificationViewModel(IStatusNotificationService statusNotificationServiceParam,
            ITabNavigationService tabNavigationServiceParam
            //            ,IProjectDataService projectDataServiceParam
            )
        {
            NotificationCollection = new ObservableCollection<NotificationItemViewModel>();

            statusNotificationService = statusNotificationServiceParam;
            tabNavigationService = tabNavigationServiceParam;
            //   projectDataService = projectDataServiceParam;

            statusNotificationService.MessageSent += StatusNotificationService_MessageSent;
            tabNavigationService.TabNameSent += TabNavigationService_TabNameSent;
            //projectDataService.ProjectLoaded += ProjectDataService_ProjectLoaded;
        }

        ObservableCollection<NotificationItemViewModel> notificationCollection;

        public ObservableCollection<NotificationItemViewModel> NotificationCollection
        {
            get { return notificationCollection; }
            set { SetValue<ObservableCollection<NotificationItemViewModel>>(value, ref notificationCollection); }
        }

        /// <summary>
        /// Handles up to 300 Notifications.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="e"></param>
        private void StatusNotificationService_MessageSent(Notification n, EventArgs e)
        {
            NotificationCollection.Insert(0, new NotificationItemViewModel(){ Model = n });
            // TODO: Maybe but in advanced settings?
            while (NotificationCollection.Count > 300)
            {
                NotificationCollection.Remove(NotificationCollection.Last());
            }
        }

        private void TabNavigationService_TabNameSent(string s, TabNavigationEventArgs e)
        {
            var res = NotificationCollection.FirstOrDefault( (n) => n.Model.Equals(e.Parameter)  );
            if (res != null)
            {
                res.ExpandNotification = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            statusNotificationService.MessageSent -= StatusNotificationService_MessageSent;
            tabNavigationService.TabNameSent -= TabNavigationService_TabNameSent;
            //projectDataService.ProjectLoaded -= ProjectDataService_ProjectLoaded;
            base.Dispose(disposing);
        }
    }
}