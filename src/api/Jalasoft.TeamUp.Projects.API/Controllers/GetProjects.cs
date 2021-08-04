namespace Jalasoft.TeamUp.Projects.API.Controllers
{
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

    public class GetProjects
    {
        private readonly IProjectsService projectService;

        public GetProjects(IProjectsService projectService)
        {
            this.projectService = projectService;
        }

        [FunctionName("GetProjects")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Projects" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project[]), Description = "Successful response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/projects")] HttpRequest req)
        {
            try
            {
                var projects = this.projectService.GetProjects();
                return new OkObjectResult(projects);
            }
            catch (ProjectsException e)
            {
                return new ContentResult
                {
                    StatusCode = e.StatusCode,
                    Content = e.ProjectsErrorMessage,
                };
            }
            catch (System.Exception)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    Content = "Something went wrong, please contact the TeamUp administrator.",
                };
            }
        }
    }
}
