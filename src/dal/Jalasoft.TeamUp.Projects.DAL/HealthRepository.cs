namespace Jalasoft.TeamUp.Projects.DAL
{
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class HealthRepository : IHealthRepository
    {
        public Health GetHealth()
        {
            return new Health() { Message = "I'm projects-api and I'm alive and running! ;)" };
        }
    }
}