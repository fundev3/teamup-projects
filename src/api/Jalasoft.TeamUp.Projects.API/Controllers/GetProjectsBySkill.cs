namespace Jalasoft.TeamUp.Projects.API.Controllers
{
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    public class GetProjectsBySkill
    {
        private readonly IProjectsService projectService;

        public GetProjectsBySkill(IProjectsService projectService)
        {
            this.projectService = projectService;
        }

        [FunctionName("GetProjectsBySkill")]
        [OpenApiOperation(operationId: "GetProjectsBySkill", tags: new[] { "Projects" })]
        [OpenApiParameter(name: "skill", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The project skill name.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/{skill}/projects")] HttpRequest req, string skill)
        {
            try
            {
                var projects = this.projectService.GetProjectsBySkill(skill);
                if (projects == null)
                {
                    throw new ProjectsException(ProjectsErrors.NotFound);
                }

                return new OkObjectResult(projects);
            }
            catch (ProjectsException e)
            {
                return e.Error;
            }
            catch (System.Exception e)
            {
                var errorException = new ProjectsException(ProjectsErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}
