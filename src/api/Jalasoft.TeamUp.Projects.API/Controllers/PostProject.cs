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
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Resource bad request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Resource internal server error")]
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
            catch (ValidationException exVal)
            {
                var errorException = new ProjectsException(ProjectsErrors.BadRequest, exVal);
                return errorException.Error;
            }
            catch (ProjectsException e)
            {
                return e.Error;
            }
            catch (Exception e)
            {
                var errorException = new ProjectsException(ProjectsErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}
