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

    public class GetProjectById
    {
        private readonly IProjectsService projectsService;

        public GetProjectById(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        [FunctionName("GetProjectById")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Projects" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The project identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/projects/{id:guid}")] HttpRequest req, Guid id)
        {
            try
            {
                var result = this.projectsService.GetProject(id);
                return new OkObjectResult(result);
            }
            catch (ProjectsException e)
            {
                return new ContentResult
                {
                    StatusCode = e.StatusCode,
                    Content = e.ProjectsErrorMessage,
                };
            }
        }
    }
}
