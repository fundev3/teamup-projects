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

    public class GetProjectsBySkillTests
    {
        private readonly Mock<IProjectsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetProjectsBySkill getProjects;

        public GetProjectsBySkillTests()
        {
            this.mockService = new Mock<IProjectsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getProjects = new GetProjectsBySkill(this.mockService.Object);
        }

        [Fact]
        public void GetProjectsBySkill_Returns_OkObjectResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetProjectsBySkill("C#")).Returns(new Project[10]);

            // Act
            var response = this.getProjects.Run(request, "C#");

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Project[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetProjectsBySkill_Returns_NotFoundResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetProjectsBySkill("C#")).Throws(new ProjectsException(ProjectsErrors.NotFound));

            // Act
            var response = this.getProjects.Run(request, "C#");

            // Assert
            var notfountObjectResult = Assert.IsType<ObjectResult>(response);
        }
    }
}
