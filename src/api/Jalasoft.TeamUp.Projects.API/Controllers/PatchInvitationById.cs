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
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    public class PatchInvitationById
    {
        private readonly IInvitationsService updateInvitationService;

        public PatchInvitationById(IInvitationsService updateInvitationService)
        {
            this.updateInvitationService = updateInvitationService;
        }

        [FunctionName("UpdateInvitation")]
        [OpenApiOperation(operationId: "UpdateInvitation", tags: new[] { "Invitations" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The invitation identifier.")]
        [OpenApiRequestBody("application/json", typeof(JsonPatchDocument<Invitation>), Description = "JSON request body")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Invitation), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public async Task<IActionResult> UpdateInvitation(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/invitations/{id:guid}")] HttpRequest req, Guid id)
        {
            try
            {
                var invitation = this.updateInvitationService.GetInvitation(id);
                if (invitation == null)
                {
                    throw new ProjectsException(ProjectsErrors.NotFound);
                }

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<JsonPatchDocument<Invitation>>(requestBody);
                data.ApplyTo(invitation);
                var result = this.updateInvitationService.UpdateInvitation(invitation);
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