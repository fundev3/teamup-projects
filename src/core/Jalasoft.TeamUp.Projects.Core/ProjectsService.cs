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
            try
            {
                var project = this.projectsRepository.GetById(id);
                if (project == null)
                {
                    throw new ProjectsException(ProjectsException.ProjectsErrors.NotFound);
                }

                return project;
            }
            catch (Exception)
            {
                throw new ProjectsException(ProjectsException.ProjectsErrors.InternalServerError);
            }
        }

        public Project PostProject(Project project)
        {
            try
            {
                ProjectValidator validator = new ProjectValidator();
                validator.ValidateAndThrow(project);
                project.Id = Guid.NewGuid();
                var result = this.projectsRepository.Add(project);
                return result;
            }
            catch (ValidationException exVal)
            {
                throw new ProjectsException(ProjectsException.ProjectsErrors.BadRequest, exVal);
            }
            catch (Exception)
            {
                throw new ProjectsException(ProjectsException.ProjectsErrors.InternalServerError);
            }
        }

        public Project[] GetProjects()
        {
            try
            {
                var projects = this.projectsRepository.GetAll().ToArray();
                if (projects == null)
                {
                    throw new ProjectsException(ProjectsException.ProjectsErrors.NotFound);
                }

                return projects;
            }
            catch (Exception)
            {
                throw new ProjectsException(ProjectsException.ProjectsErrors.InternalServerError);
            }
        }
    }
}
