namespace Jalasoft.TeamUp.Projects.API.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    public class GetInvitationsByResumeId
    {
        private readonly IInvitationsService invitationsService;

        public GetInvitationsByResumeId(IInvitationsService invitationsService)
        {
            this.invitationsService = invitationsService;
        }

        [FunctionName("GetInvitationsByResumeId")]
        [OpenApiOperation(operationId: "GetInvitationsByResumeId", tags: new[] { "Invitations" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The invitation identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Invitation[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Resource internal server error")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/resumes/{id:int}/invitations/")] HttpRequest req, int id)
        {
            try
            {
                var result = this.invitationsService.GetInvitationsByResumeId(id);
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
