namespace Jalasoft.TeamUp.Projects.API.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Jalasoft.TeamUp.Projects.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    public class GetInvitationByProjectId
    {
        private static readonly Invitation[] Invitations = new Invitation[]
            {
                new Invitation
                        {
                            Id = "5a7939fd-59de-44bd-a092-f5d8434584de",
                            ProjectId = "724d912-59de-44bd-a092-f5d8434584de",
                            ProjectName = "Sony",
                            ResumeId = Guid.Parse("fb14f3f4-7d2e-4c4e-9c50-9997c83a77c9"),
                            ResumeName = "Jose Ecos",
                            PictureResume = "photo.png",
                            TextInvitation = "We invite you to collaborate with the development team",
                            StartDate = DateTime.Now.AddDays(-10),
                            ExpireDate = DateTime.Now,
                            Status = "invited"
                        },
                new Invitation
                        {
                            Id = "5a7939fd-59de-44bd-a092-111111111111",
                            ProjectId = "724d912-59de-44bd-a092-222222222222",
                            ProjectName = "Samsung",
                            ResumeId = Guid.Parse("fb14f3f4-7d2e-4c4e-9c50-9997c83a77c9"),
                            ResumeName = "Freddy",
                            PictureResume = "photo.png",
                            TextInvitation = "We invite you to collaborate with the development team",
                            StartDate = DateTime.Now.AddDays(-10),
                            ExpireDate = DateTime.Now,
                            Status = "invited"
                        },
                new Invitation
                        {
                            Id = "5a7939fd-59de-44bd-a092-3333333333",
                            ProjectId = "724d912-59de-44bd-a092-4444444444",
                            ProjectName = "Huamei",
                            ResumeId = Guid.Parse("fb14f3f4-7d2e-4c4e-9c50-9997c83a7711"),
                            ResumeName = "Gustavo",
                            PictureResume = "photo.png",
                            TextInvitation = "We invite you to collaborate with the development team",
                            StartDate = DateTime.Now.AddDays(-10),
                            ExpireDate = DateTime.Now,
                            Status = "invited"
                        }
            };

        private readonly InvitationService invitationService;

        public GetInvitationByProjectId(InvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        [FunctionName("GetInvitationByProject")]
        [OpenApiOperation(operationId: "GetProjectById", tags: new[] { "Invitations" })]
        [OpenApiParameter(name: "idProject", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The project identifier.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/invitations/{idProject:guid}")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(Invitations);
        }

        /**
         * TODO: Delete and use model of Pedro
         * */
        public class Invitation
        {
            public string Id { get; set; }

            public string ProjectId { get; set; }

            public string ProjectName { get; set; }

            public Guid ResumeId { get; set; }

            public string ResumeName { get; set; }

            public string PictureResume { get; set; }

            public string TextInvitation { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime ExpireDate { get; set; }

            public string Status { get; set; }
        }
    }
}
