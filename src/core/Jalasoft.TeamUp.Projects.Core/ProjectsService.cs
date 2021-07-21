namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository projectsRepository;

        public ProjectsService(IProjectsRepository projectsRepository)
        {
            this.projectsRepository = projectsRepository;
        }

        public Project GetProject(Guid id)
        {
            return this.projectsRepository.GetProject(id);
        }
    }
}
