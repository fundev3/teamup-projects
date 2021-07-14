namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsRepository : IProjectsRepository
    {
        private List<Health> healthList = new List<Health>() { new Health() { Message = "I'm resumes-api and I'm alive and running! ;)" } };

        public Health GetHealth()
        {
            return this.healthList[0];
        }
    }
}