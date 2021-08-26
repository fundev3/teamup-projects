namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
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
        public void GetProjectById_IdIsValid_OkObjectResult()
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
        public void GetProjectById_IdIsNotValid_NotFoundResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Throws(new ProjectsException(ProjectsErrors.NotFound));

            // Act
            var response = this.getProject.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"));

            // Assert
            var notfountObjectResult = Assert.IsType<ObjectResult>(response);
        }
    }
}
