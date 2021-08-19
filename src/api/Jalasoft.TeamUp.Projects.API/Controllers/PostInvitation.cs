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

    public class PostInvitation
    {
        private readonly IInvitationsService invitationService;

        public PostInvitation(IInvitationsService invitationService)
        {
            this.invitationService = invitationService;
        }

        [FunctionName("PostInvitationByProjectId")]
        [OpenApiOperation(operationId: "PostInvitationByProjectId", tags: new[] { "Invitations" })]
        [OpenApiRequestBody("application/json", typeof(Invitation), Description = "JSON request body")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Invitation[]), Description = "Successful response")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/invitations")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Invitation>(requestBody);
                var result = this.invitationService.PostInvitation(data);
                return new CreatedResult("v1/invitations/:id", result);
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
