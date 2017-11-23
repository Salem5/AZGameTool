using AZGameToolTry1.Model;
using System;

namespace AZGameToolTry1.LService
{
    public delegate void DatabaseChangeHandler(String s, EventArgs e);

    public delegate RecentProject DbActions();

    public interface IDatabaseService
    {
        event DatabaseChangeHandler DatabaseChange;
        RecentProject WriteInDb(DbActions dbactions, string changedCollection);
    }
}