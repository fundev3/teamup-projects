namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using System.Linq;
    using FluentValidation;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Core.Validators;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;

    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<Project> projectsRepository;

        public ProjectsService(IRepository<Project> projectsRepository)
        {
            this.projectsRepository = projectsRepository;
        }

        public Project GetProject(Guid id)
        {
            var project = this.projectsRepository.GetById(id);
            return project;
        }

        public Project PostProject(Project project)
        {
            project.Id = Guid.NewGuid();
            ProjectValidator validator = new ProjectValidator();
            validator.ValidateAndThrow(project);
            var result = this.projectsRepository.Add(project);
            return result;
        }

        public Project[] GetProjects()
        {
                var projects = this.projectsRepository.GetAll().ToArray();
                return projects;
        }
    }
}
