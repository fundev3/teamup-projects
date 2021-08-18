namespace Jalasoft.TeamUp.Projects.API.Controllers
{
    using System;
    using System.Net;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;

    public class GetInvitationByProjectId
    {
        private readonly IInvitationsService invitationService;

        public GetInvitationByProjectId(IInvitationsService invitationService)
        {
            this.invitationService = invitationService;
        }

        [FunctionName("GetInvitationByProject")]
        [OpenApiOperation(operationId: "GetProjectById", tags: new[] { "Invitations" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The project identifier.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/projects/{id}/invitations")] HttpRequest req, string id)
        {
            try
            {
                var result = this.invitationService.GetInvitationsByProjectId(id);
                if (result == null)
                {
                    throw new ProjectsException(ProjectsErrors.NotFound);
                }

                return new OkObjectResult(result);
            }
            catch (ProjectsException e)
            {
                return e.Error;
            }
            catch (Exception ex)
            {
                var errorException = new ProjectsException(ProjectsErrors.InternalServerError, ex);
                return errorException.Error;
            }
        }
    }
}
