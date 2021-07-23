namespace Jalasoft.TeamUp.Projects.DAL.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IProjectsRepository
    {
        Project PostProject(Project project);

        public Project GetProject(Guid id);
    }
}
