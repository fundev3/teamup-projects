namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<Project> projectsRepository;

        public ProjectsService(IRepository<Project> projectsRepository)
        {
            this.projectsRepository = projectsRepository;
        }

        public Project GetProject(Guid id)
        {
            return this.projectsRepository.GetById(id);
        }

        public Project PostProject(Project project)
        {
            project.Id = Guid.NewGuid();
            var result = this.projectsRepository.Add(project);
            return result;
        }

        public Project[] GetProjects()
        {
            return this.projectsRepository.GetAll().ToArray();
        }
    }
}
