namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetProjectTest
    {
        private readonly Mock<IProjectService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetProject getProject;

        public GetProjectTest()
        {
            this.mockService = new Mock<IProjectService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getProject = new GetProject(this.mockService.Object);
        }

        [Fact]
        public void GetProject_Returns_OkObjectResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;

            // Act
            var response = this.getProject.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Project[]>(okObjectResult.Value);
        }
    }
}
