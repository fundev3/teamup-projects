namespace Jalasoft.TeamUp.Projects.Core
{
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class CreateProjectService : ICreateProjectService
    {
        private readonly ICreateProjectRepository projectRepository;

        public CreateProjectService(ICreateProjectRepository projectRepository)
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
