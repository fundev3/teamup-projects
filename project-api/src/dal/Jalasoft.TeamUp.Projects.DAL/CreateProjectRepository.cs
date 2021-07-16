namespace Jalasoft.TeamUp.Projects.DAL
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class CreateProjectRepository : ICreateProjectRepository
    {
        private static IList<Project> projects = new List<Project>();

        public Project PostProject(Project myProject)
        {
            projects.Add(myProject);
            return myProject;
        }
    }
}
