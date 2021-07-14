namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsRepository : IProjectsRepository
    {
        private Health health = new Health() { Message = "projects-api" };

        public Health GetHealth()
        {
            return this.health;
        }
    }
}