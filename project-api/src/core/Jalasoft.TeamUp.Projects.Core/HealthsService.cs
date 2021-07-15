namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class HealthsService : IHealthsService
    {
        private readonly IHealthsRepository projectsRepository;

        public HealthsService(IHealthsRepository projectsRepository)
        {
            this.projectsRepository = projectsRepository;
        }

        public Health GetHealth()
        {
            return this.projectsRepository.GetHealth();
        }
    }
}
