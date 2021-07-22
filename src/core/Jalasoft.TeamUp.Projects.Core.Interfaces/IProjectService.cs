namespace Jalasoft.TeamUp.Projects.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IProjectService
    {
        public Project GetProject(Guid id);

        Project[] GetProjects();
    }
}
