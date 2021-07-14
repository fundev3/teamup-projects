namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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

        public Health GetHealth()
        {
            return this.projectsRepository.GetHealth();
        }
    }
}
