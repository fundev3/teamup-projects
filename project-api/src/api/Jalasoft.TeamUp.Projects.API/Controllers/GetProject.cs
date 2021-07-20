namespace Jalasoft.TeamUp.Projects.API.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;

    public class GetProject
    {
        private readonly IProjectsService projectsService;

        public GetProject(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        [FunctionName("GetProject")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Project" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "The **Id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project), Description = "Successful response")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            Guid id = Guid.Parse(req.Query["id"]);
            var result = this.projectsService.GetProject(id);
            return new OkObjectResult(result);
        }
    }
}
