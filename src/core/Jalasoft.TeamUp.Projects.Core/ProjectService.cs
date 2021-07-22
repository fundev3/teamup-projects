namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectsRepository;

        public ProjectService(IProjectRepository projectsRepository)
        {
            this.projectsRepository = projectsRepository;
        }

        public Project GetProject(Guid id)
        {
            return this.projectsRepository.GetProject(id);
        }

        public Project[] GetProjects()
        {
            return this.projectsRepository.GetProjects().ToArray();
        }
    }
}
