namespace Jalasoft.TeamUp.Projects.DAL
{
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsRepository : IProjectsRepository
    {
        private static IEnumerable<Project> projects = new List<Project>();

        public Project PostProject(Project project)
        {
            projects = projects.Append(project);
            return project;
        }
    }
}
