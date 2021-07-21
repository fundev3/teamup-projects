namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class HealthService : IHealthService
    {
        private readonly IHealthRepository healthRepository;

        public HealthService(IHealthRepository healthRepository)
        {
            this.healthRepository = healthRepository;
        }

        public Health GetHealth()
        {
            return this.healthRepository.GetHealth();
        }
    }
}
