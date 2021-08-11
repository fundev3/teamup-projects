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

    public class PostProjectsTest
    {
        private readonly Mock<IProjectsService> mockProjectsService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PostProject postProject;

        public PostProjectsTest()
        {
            this.mockProjectsService = new Mock<IProjectsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.postProject = new PostProject(this.mockProjectsService.Object);
        }

        [Fact]
        public async void PostProject_Returns_CreateProject_Project()
        {
            var request = this.mockHttpContext.Request;
            this.mockProjectsService.Setup(service => service.PostProject(null)).Returns(new Project() { Name = "TeamUp" });
            var response = await this.postProject.CreateProject(request);
            var okObjectResult = Assert.IsType<CreatedResult>(response);
            Assert.IsType<Project>(okObjectResult.Value);
        }

        [Fact]
        public async void PostProject_Returns_BadRequest()
        {
            var request = this.mockHttpContext.Request;
            this.mockProjectsService.Setup(service => service.PostProject(null)).Throws(new ProjectsException(ProjectsErrors.BadRequest, new FluentValidation.ValidationException("BadRequest")));
            var response = await this.postProject.CreateProject(request);
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
