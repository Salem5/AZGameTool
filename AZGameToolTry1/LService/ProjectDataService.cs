using AZGameToolTry1.Model;
using LibGit2Sharp;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AZGameToolTry1.LService
{
    public class ProjectDataService : IProjectDataService
    {
        readonly IDatabaseService databaseService;
        readonly IStatusNotificationService statusNotificationService;

        public string ProjectPath { get; private set; }

        public IEnumerable<BaseMd> ProjectFiles { get; private set; }

        public ProjectDataService(IDatabaseService databaseServiceParam,
            IStatusNotificationService statusNotificationServiceParam)
        {
            statusNotificationService = statusNotificationServiceParam;
            databaseService = databaseServiceParam;
            ProjectFiles = new List<BaseMd>();
        }

        public bool LoadProject(string projectPath)
        {
            if (Directory.Exists(projectPath))
            {
                if (Repository.IsValid(projectPath))
                {
                    var repo = new Repository(projectPath);
                    // Store new repo in to the recent projects table.
                    RecentProject project = databaseService.WriteInDb(() =>
                    {
                        RecentProject foundProject;

                        using (var db = new LiteDatabase(App.DbPath))
                        {
                            var col = db.GetCollection<RecentProject>();
                            var res = Path.GetFileNameWithoutExtension(projectPath);

                            foundProject = col.FindOne(f => f.Location == projectPath && f.Name == res);
                            foundProject = new RecentProject() { Id = foundProject?.Id ?? 0, Date = DateTime.Now, Name = res, Location = projectPath };

                            // Need to test Upsert a lot more.
                            col.Upsert(foundProject);

                            //if (find == null)
                            //{
                            //    col.Insert(new RecentProject() { Date = DateTime.Now, Name = res, Location = folderDialog.FileName });
                            //}
                            //else
                            //{
                            //    find.Date = DateTime.Now;
                            //    col.Update(find);
                            //}
                        }

                        return foundProject;

                    }, nameof(RecentProject));

                    ProjectPath = projectPath;

                    // Read all relevant md files 

                    var mdFilePaths = Directory.GetFiles(projectPath, "*.md", SearchOption.TopDirectoryOnly);

                    var resTeam = from mdFile in mdFilePaths
                                  where mdFile.ToLower().EndsWith("team.md")
                                  select new Team() { };

                    var resReadMe = from mdFile in mdFilePaths
                                    where mdFile.ToLower().EndsWith("readme.md")
                                    select new ReadMe() { Text = File.ReadAllText(Path.Combine(projectPath, mdFile)) };

                    var castedProjectFiles = ProjectFiles as List<BaseMd>;
                    castedProjectFiles.Clear();
                    castedProjectFiles.AddRange(resTeam);
                    castedProjectFiles.AddRange(resReadMe);

                    ProjectLoaded?.Invoke(project, new EventArgs());
                    return true;
                }
                else
                {
                    statusNotificationService.SendMessage(new Notification()
                    {
                        Kind = NotificationKind.Warning,
                        Message = "The directory is not a valid git repo.",
                        Title = "Invalid Repo",
                        Time = DateTime.Now
                    });
                    return false;
                }
            }
            else
            {
                statusNotificationService.SendMessage(new Notification()
                {
                    Kind = NotificationKind.Warning,
                    Message = "The remembered directory does not exist (anymore).",
                    Title = "Not Found",
                    Time = DateTime.Now
                });
                return false;
            }
        }

        public event ProjectLoadedHandler ProjectLoaded;
    }
}
