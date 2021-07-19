namespace Jalasoft.TeamUp.Projects.DAL
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsRepository : IProjectsRepository
    {
        private static IList<Project> projects = new List<Project>();

        public Project PostProject(Project project)
        {
            projects.Add(project);
            return project;
        }
    }
}
