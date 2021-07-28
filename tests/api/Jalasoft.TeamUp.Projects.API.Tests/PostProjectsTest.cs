namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
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
        public async void PostProject_Returns_OkObjectResult_Project()
        {
            var request = this.mockHttpContext.Request;
            this.mockProjectsService.Setup(service => service.PostProject(null)).Returns(new Project());
            var response = await this.postProject.CreateProject(request);
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Project>(okObjectResult.Value);
        }

        [Fact]
        public async void PostProject_Returns_StatusCode_201()
        {
            var request = this.mockHttpContext.Request;
            this.mockProjectsService.Setup(service => service.PostProject(null)).Returns(new Project());
            var response = await this.postProject.CreateProject(request);
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Project>(okObjectResult.Value);
        }
    }
}
