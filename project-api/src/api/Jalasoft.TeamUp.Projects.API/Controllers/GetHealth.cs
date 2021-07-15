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
        private readonly IHealthsService healthsService;

        public GetHealth(IHealthsService healthsService)
        {
            this.healthsService = healthsService;
        }

        [FunctionName("GetHealth")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Health" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Health), Description = "Successful response")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ILogger log)
        {
            var result = this.healthsService.GetHealth();
            return new OkObjectResult(result);
        }
    }
}
