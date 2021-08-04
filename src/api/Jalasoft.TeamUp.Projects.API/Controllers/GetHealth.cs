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
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;

    public class GetHealth
    {
        private readonly IHealthService healthService;

        public GetHealth(IHealthService healthService)
        {
            this.healthService = healthService;
        }

        [FunctionName("GetHealth")]
        [OpenApiOperation(operationId: "GetHealth", tags: new[] { "Health" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Health), Description = "Successful response")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/health")] HttpRequest req, ILogger log)
        {
            var result = this.healthService.GetHealth();
            return new OkObjectResult(result);
        }
    }
}
