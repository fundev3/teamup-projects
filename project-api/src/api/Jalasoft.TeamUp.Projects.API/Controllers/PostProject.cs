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

    public class PostProject
    {
        private readonly ICreateProjectService createProjectService;

        public PostProject(ICreateProjectService createProjectService)
        {
            this.createProjectService = createProjectService;
        }

        [FunctionName("PostProject")]
        [OpenApiOperation(operationId: "Post", tags: new[] { "CreateProject" })]
        [OpenApiRequestBody("application/json", typeof(Project), Description = "JSON request body containing { ID, Name, Description, Contact }")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project), Description = "Successful response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Project>(requestBody);
            var result = this.createProjectService.CreateProject(data);

            return new OkObjectResult(result);
        }
    }
}
