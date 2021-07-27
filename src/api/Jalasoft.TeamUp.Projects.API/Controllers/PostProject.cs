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
        private readonly IProjectsService postProjectService;

        public PostProject(IProjectsService postProjectService)
        {
            this.postProjectService = postProjectService;
        }

        [FunctionName("project")]
        [OpenApiOperation(operationId: "createProject", tags: new[] { "CreateProject" })]
        [OpenApiRequestBody("application/json", typeof(Project), Description = "JSON request body")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project), Description = "Successful response")]
        public IActionResult CreateProject(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/projects")] HttpRequest req)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<Project>(requestBody);
            var result = this.postProjectService.PostProject(data);
            return new OkObjectResult(result);
        }
    }
}
