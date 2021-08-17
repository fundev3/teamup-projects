﻿namespace Jalasoft.TeamUp.Projects.API.Controllers
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
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    public class PatchProjectById
    {
        private readonly IProjectsService updateProjectService;

        public PatchProjectById(IProjectsService updateProjectService)
        {
            this.updateProjectService = updateProjectService;
        }

        [FunctionName("UpdateProject")]
        [OpenApiOperation(operationId: "UpdateProject", tags: new[] { "Projects" })]
        [OpenApiRequestBody("application/json", typeof(JsonPatchDocument<Project>), Description = "JSON request body")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public async Task<IActionResult> UpdateProject(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/projects/{id:guid}")] HttpRequest req, Guid id)
        {
            try
            {
                var projectUpd = this.updateProjectService.GetProject(id);
                if (projectUpd == null)
                {
                    throw new ProjectsException(ProjectsErrors.NotFound);
                }

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<JsonPatchDocument<Project>>(requestBody);
                data.ApplyTo(projectUpd);
                var result = this.updateProjectService.UpdateProject(projectUpd);
                return new OkObjectResult(result);
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
