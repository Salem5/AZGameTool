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
    public class SettingsViewModel : BaseViewModel
    {
        readonly IStatusNotificationService statusNotificationService;
        readonly IDatabaseService databaseService;

      
        public ICommand ResetDatabaseCommand { get; }
        public ICommand TestNotificationCommand { get; }
      
        public double DbSize { get => databaseService.DbSize(); }

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

        public SettingsViewModel(IStatusNotificationService statusNotificationServiceParam, IDatabaseService databaseServiceParam)
        {
            statusNotificationService = statusNotificationServiceParam;
            databaseService = databaseServiceParam;

            ResetDatabaseCommand = new AnotherCommandImplementation(t =>
            {
                databaseService.WriteInDb(() =>
                {
                    using (var db = new LiteDatabase(App.DbPath))
                    {
                        db.DropCollection(nameof(RecentProject));
                    }
                    return null;
                }, nameof(RecentProject));
            });

            TestNotificationCommand = new AnotherCommandImplementation(ne =>
            {
                NotificationKind kind = (NotificationKind)Convert.ToInt32(ne);
                statusNotificationService.SendMessage(new Notification()
                {
                    Kind = kind,
                    Message = kind.ToString(),
                    Title = kind.ToString(),
                    Time = DateTime.Now
                });
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}