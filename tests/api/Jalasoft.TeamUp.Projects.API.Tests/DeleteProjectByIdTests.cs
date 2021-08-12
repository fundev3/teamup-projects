namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class DeleteProjectByIdTests
    {
        private readonly Mock<IProjectsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly DeleteProjectById deleteProject;

        public DeleteProjectByIdTests()
        {
            this.mockService = new Mock<IProjectsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.deleteProject = new DeleteProjectById(this.mockService.Object);
        }

        [Fact]
        public void DeleteProject_Returns_NoContentResult()
        {
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.DeleteProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")));
            var response = this.deleteProject.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"));
            var noContentResult = response;
            Assert.IsType<NoContentResult>(noContentResult);
        }

        [Fact]
        public void DeleteProject_Returns__NotFoundResult()
        {
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.DeleteProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Throws(new ProjectsException(ProjectsErrors.NotFound));
            var response = this.deleteProject.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.IsType<ObjectResult>(response);
        }

        [Fact]
        public void DeleteProject_Returns__InternalServerError()
        {
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.DeleteProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Throws(new Exception());
            var response = this.deleteProject.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.IsType<ObjectResult>(response);
        }
    }
}
