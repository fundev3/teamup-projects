namespace Jalasoft.TeamUp.Projects.DAL.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IProjectsRepository
    {
        public Project GetProject(Guid id);
    }
}
