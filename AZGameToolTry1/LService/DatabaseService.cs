using AZGameToolTry1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZGameToolTry1.LService
{
    public class DatabaseService : IDatabaseService
    {
        private void ChangeDatabase(string changedCollection)
        {
            DatabaseChange?.Invoke(changedCollection, new EventArgs());
        }

        public event DatabaseChangeHandler DatabaseChange;        
        
        /// <summary>
        /// Sends a change notification to all subscribers, so these can refresh with the new data.
        /// </summary>
        /// <param name="dbactions">The callback of database actions.</param>
        /// <param name="changedCollectionName">the collection that changed.</param>
        /// <returns></returns>
        public RecentProject WriteInDb(DbActions dbactions, string changedCollectionName)
        {
            var res = dbactions?.Invoke();
            // NOTE: It's possible to use a collection with the names of changed collections, if the dbactions performs writes on multiple collections, but let's not go there for now...
            ChangeDatabase(changedCollectionName);
            return res;
        }

        public double DbSize()
        {
            return new FileInfo(App.DbPath).Length;
        }

      
    }
}