namespace Jalasoft.TeamUp.Projects.Core
{
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

        public Project CreateProject(Project project)
        {
            var result = this.projectRepository.PostProject(project);
            return result;
        }
    }
}
