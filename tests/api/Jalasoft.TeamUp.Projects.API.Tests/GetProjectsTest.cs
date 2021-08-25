namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.API.Tests.Utils;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
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
            Project[] projects = new Project[2];
            projects[0] = StubProject.GetStubProject();
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetProjects(null)).Returns(projects);

            // Act
            var response = this.getProjects.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Project[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetProjectsBySkill_SkillNameIsValid_OkObjectResult()
        {
            // Arrange
            Project[] projects = new Project[1];
            projects[0] = StubProject.GetStubProject();
            var request = this.mockHttpContext.Request;
            request.QueryString = new QueryString("?skill=C#");
            this.mockService.Setup(service => service.GetProjects("C#")).Returns(projects);

            // Act
            var response = this.getProjects.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Project[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetProjectsBySkill_SkillNameIsValid_NotFoundResult()
        {
            // Arrange
            Project[] projects = new Project[1];
            projects[0] = StubProject.GetStubProject();
            var request = this.mockHttpContext.Request;
            request.QueryString = new QueryString("?skill=Python");
            this.mockService.Setup(service => service.GetProjects("none")).Returns(projects);

            // Act
            var response = this.getProjects.Run(request);

            // Assert
            var notfoundObjectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, notfoundObjectResult.StatusCode);
        }
    }
}
