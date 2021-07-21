namespace Jalasoft.TeamUp.Projects.Core
{
    using System.Linq;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository projectRepository;

        public ProjectsService(IProjectsRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public Project[] GetProjects()
        {
            return this.projectRepository.GetProjects().ToArray();
        }
    }
}
