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

    public class GetInvitationsByProjectIdTests
    {
        private readonly Mock<IInvitationsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetInvitationsByProjectId getInvitations;

        public GetInvitationsByProjectIdTests()
        {
            this.mockService = new Mock<IInvitationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getInvitations = new GetInvitationsByProjectId(this.mockService.Object);
        }

        [Fact]
        public void GetInvitationsByProjectId_IdIsValid_OkObjectResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetInvitationsByProjectId("f2879e14-fd99-4364-8e18-7a1a07f3ea55")).Returns(new Invitation[2]);

            // Act
            var response = this.getInvitations.Run(request, "f2879e14-fd99-4364-8e18-7a1a07f3ea55");

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Invitation[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetInvitationsByProjectId_IdIsNotValid_NotFoundResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetInvitationsByProjectId("f2879e14-fd99-4364-8e18-7a1a07f3ea77")).Returns((Invitation[])null);

            // Act
            var response = this.getInvitations.Run(request, "f2879e14-fd99-4364-8e18-7a1a07f3ea77");

            // Assert
            var notfountObjectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, notfountObjectResult.StatusCode);
        }
    }
}
