using AZGameToolTry1.Model;
using System;
using System.Collections.Generic;

namespace AZGameToolTry1.LService
{
    public delegate void ProjectLoadedHandler(RecentProject p, EventArgs e);

    public interface IProjectDataService
    {
        event ProjectLoadedHandler ProjectLoaded;
        string ProjectPath { get; }

        IEnumerable<BaseMd> ProjectFiles { get; }

        bool LoadProject(string projectPath);

        string CreateMarkup(string sourceFilePath);
    }
}