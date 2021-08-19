namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System.IO;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class PostInvitationTest
    {
        private readonly Mock<IInvitationsService> mockInvitationsService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PostInvitation postInvitation;

        public PostInvitationTest()
        {
            this.mockInvitationsService = new Mock<IInvitationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.postInvitation = new PostInvitation(this.mockInvitationsService.Object);
        }

        [Fact]
        public async void PostInvitation_ValidInvitation_CreatedResult()
        {
            var request = this.mockHttpContext.Request;
            this.mockInvitationsService.Setup(service => service.PostInvitation(null)).Returns(new Invitation() { ProjectName = "TeamUp" });
            var response = await this.postInvitation.RunAsync(request);
            var createdResult = Assert.IsType<CreatedResult>(response);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void PostInvitation_InvalidInvitation_BadRequestResult()
        {
            var request = this.mockHttpContext.Request;
            this.mockInvitationsService.Setup(service => service.PostInvitation(null)).Throws(new FluentValidation.ValidationException("BadRequest"));
            var response = await this.postInvitation.RunAsync(request);
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
