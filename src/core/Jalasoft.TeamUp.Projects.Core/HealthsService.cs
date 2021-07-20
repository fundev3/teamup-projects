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
        private readonly IHealthsRepository healthsRepository;

        public HealthsService(IHealthsRepository projectsRepository)
        {
            this.healthsRepository = projectsRepository;
        }

        public Health GetHealth()
        {
            return this.healthsRepository.GetHealth();
        }
    }
}
