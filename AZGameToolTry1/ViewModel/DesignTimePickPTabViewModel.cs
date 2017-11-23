using AZGameToolTry1.Model;
using LiteDB;
using MaterialDesignColors.WpfExample.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AZGameToolTry1.ViewModel
{
    public class DesignTimePickPTabViewModel : IPickPTabViewModel
    {
        public DesignTimePickPTabViewModel()
        {// TODO: Load Projects from SqLite here.

        }

        public ICommand ProjectSelectCommand { get; } = new AnotherCommandImplementation(o =>
        { System.Windows.MessageBox.Show(o.ToString()); }

        //,(x) =>
        //{
        // //   return (RecentProjectCollection.Count() >= 0);
        //}
        );

        public IEnumerable<RecentProject> RecentProjectCollection
        {
            get
            {
                return new List<RecentProject>()
                    {
                    new RecentProject() { Date = DateTime.Now, Name = "TestName1", Location = "TestLocation1" } ,
                new RecentProject() { Date = DateTime.Now.AddDays(-1), Name = "TestName2", Location = "TestLocation2" },
                new RecentProject() { Date = DateTime.Now.AddDays(-2), Name = "TestName3", Location = "TestLocation3" }
                };
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
