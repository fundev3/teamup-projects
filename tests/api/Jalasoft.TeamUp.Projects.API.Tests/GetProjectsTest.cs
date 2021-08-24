namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetProjectsTest
    {
        private readonly Mock<IProjectsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetProjects getProjects;

        public GetProjectsTest()
        {
            this.mockService = new Mock<IProjectsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getProjects = new GetProjects(this.mockService.Object);
        }

        [Fact]
        public void GetProjects_ProjectsExist_OkObjectResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;

            // Act
            var response = this.getProjects.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Project[]>(okObjectResult.Value);
        }
    }
}
