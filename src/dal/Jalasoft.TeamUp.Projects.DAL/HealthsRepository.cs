namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class HealthsRepository : IHealthsRepository
    {
        public Health GetHealth()
        {
            return new Health() { Message = "I'm projects-api and I'm alive and running! ;)" };
        }
    }
}