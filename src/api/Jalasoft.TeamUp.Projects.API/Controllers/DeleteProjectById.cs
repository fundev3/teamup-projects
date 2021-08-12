namespace Jalasoft.TeamUp.Projects.API.Controllers
{
    using System;
    using System.Net;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;

    public class DeleteProjectById
    {
        private readonly IProjectsService projectsService;

        public DeleteProjectById(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        [FunctionName("DeleteProjectById")]
        [OpenApiOperation(operationId: "DeleteProjectById", tags: new[] { "Projects" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The project identifier.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "There's no content to be returned")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/projects/{id:guid}")] HttpRequest req, Guid id)
        {
            try
            {
                var result = this.projectsService.DeleteProject(id);
                return new NoContentResult();
            }
            catch (ProjectsException e)
            {
                return e.Error;
            }
            catch (Exception ex)
            {
                var errorException = new ProjectsException(ProjectsErrors.InternalServerError, ex);
                return errorException.Error;
            }
        }
    }
}
