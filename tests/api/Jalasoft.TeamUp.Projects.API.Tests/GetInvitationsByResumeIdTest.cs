namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetInvitationsByResumeIdTest
    {
        private readonly Mock<IInvitationsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetInvitationsByResumeId getInvitations;

        public GetInvitationsByResumeIdTest()
        {
            this.mockService = new Mock<IInvitationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getInvitations = new GetInvitationsByResumeId(this.mockService.Object);
        }

        [Fact]
        public void GetInvitationsByResumeId_Returns_OkObjectResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetInvitationsByResumeId(2)).Returns(new Invitation[2]);

            // Act
            var response = this.getInvitations.Run(request, 2);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Invitation[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetInvitationsByResumeId_Returns_NotFoundResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetInvitationsByResumeId(3)).Throws(new ProjectsException(ProjectsErrors.NotFound));

            // Act
            var response = this.getInvitations.Run(request, 3);

            // Assert
            var notfountObjectResult = Assert.IsType<ObjectResult>(response);
        }
    }
}
