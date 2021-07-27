namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Moq;
    using Xunit;

    public class GetProjectByIdTests
    {
        private readonly Mock<IProjectsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetProjectById getProject;

        public GetProjectByIdTests()
        {
            this.mockService = new Mock<IProjectsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getProject = new GetProjectById(this.mockService.Object);
        }

        [Fact]
        public void GetProject_Returns_OkObjectResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(new Project());

            // Act
            var response = this.getProject.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"));

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Project>(okObjectResult.Value);
        }

        [Fact]
        public void GetProject_Returns_NotFoundResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Equals(null);

            // Act
            var response = this.getProject.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"));

            // Assert
            var notfountObjectResult = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Null(notfountObjectResult.Value);
        }
    }
}
