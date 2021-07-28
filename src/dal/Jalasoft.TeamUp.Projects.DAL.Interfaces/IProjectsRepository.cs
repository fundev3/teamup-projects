namespace Jalasoft.TeamUp.Projects.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IProjectsRepository
    {
        Project PostProject(Project project);

        public Project GetProject(Guid id);

        IEnumerable<Project> GetProjects();
    }
}
