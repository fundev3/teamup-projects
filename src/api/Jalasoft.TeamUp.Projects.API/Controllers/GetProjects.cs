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
    using Microsoft.Extensions.Primitives;
    using Microsoft.OpenApi.Models;

    public class GetProjects
    {
        private readonly IProjectsService projectService;

        public GetProjects(IProjectsService projectService)
        {
            this.projectService = projectService;
        }

        [FunctionName("GetProjects")]
        [OpenApiOperation(operationId: "GetProjects", tags: new[] { "Projects" })]
        [OpenApiParameter(name: "skill", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The project skill name.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project[]), Description = "Successful response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/projects")] HttpRequest req)
        {
            try
            {
                req.Query.TryGetValue("skill", out StringValues skill);
                if (string.IsNullOrEmpty(skill))
                {
                    var projects = this.projectService.GetProjects();
                    if (projects == null)
                    {
                        throw new ProjectsException(ProjectsErrors.NotFound);
                    }

                    return new OkObjectResult(projects);
                }
                else
                {
                    var projects = this.projectService.GetProjectsBySkill(skill);
                    if (projects == null)
                    {
                        throw new ProjectsException(ProjectsErrors.NotFound);
                    }

                    return new OkObjectResult(projects);
                }
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
