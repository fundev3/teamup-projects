namespace Jalasoft.TeamUp.Projects.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IProjectsService
    {
        Project PostProject(Project project);

        public Project GetProject(Guid id);

        Project[] GetProjects(string skill);

        public void RemoveProject(Guid id);

        Project UpdateProject(Project project);
    }
}
