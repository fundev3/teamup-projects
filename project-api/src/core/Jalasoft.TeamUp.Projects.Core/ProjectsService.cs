namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
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

        public Project PostProject(Project project)
        {
            project.Id = Guid.NewGuid();
            var result = this.projectRepository.PostProject(project);
            return result;
        }
    }
}
