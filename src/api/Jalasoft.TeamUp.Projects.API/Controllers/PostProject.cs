namespace Jalasoft.TeamUp.Projects.API.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using FluentValidation;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    public class PostProject
    {
        private readonly IProjectsService postProjectService;

        public PostProject(IProjectsService postProjectService)
        {
            this.postProjectService = postProjectService;
        }

        [FunctionName("PostProject")]
        [OpenApiOperation(operationId: "PostProject", tags: new[] { "Projects" })]
        [OpenApiRequestBody("application/json", typeof(Project), Description = "JSON request body")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Project), Description = "Successful response")]
        public async Task<IActionResult> CreateProject(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/projects")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Project>(requestBody);
                var result = this.postProjectService.PostProject(data);
                return new CreatedResult("v1/projects/:id", result);
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
