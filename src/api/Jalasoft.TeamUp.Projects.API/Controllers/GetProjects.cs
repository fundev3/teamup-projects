namespace Jalasoft.TeamUp.Projects.API.Controllers
{
    using System.Net;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;

    public class GetProjects
    {
        private readonly IProjectsService projectService;

        public GetProjects(IProjectsService projectService)
        {
            this.projectService = projectService;
        }

        [FunctionName("projects")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Projects" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project), Description = "Successful response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var projects = this.projectService.GetProjects();
            return new OkObjectResult(projects);
        }
    }
}
