namespace Jalasoft.TeamUp.Projects.API.Controllers
{
    using System.IO;
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
    using Newtonsoft.Json;

    public class GetHealth
    {
        private readonly IProjectsService projectsService;
        public GetHealth(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        [FunctionName("GetHealth")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Health" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(Health), Description = "The OK response")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var result = this.projectsService.GetHealth();
            return new OkObjectResult(result);
        }
    }
}
