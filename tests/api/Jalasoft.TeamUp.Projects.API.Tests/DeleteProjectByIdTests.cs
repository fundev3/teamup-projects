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
        public void DeleteProject_IdIsValid_NoContentResult()
        {
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.RemoveProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")));
            this.mockService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(new Project());
            Assert.IsType<NoContentResult>(this.deleteProject.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de")));
        }

        [Fact]
        public void DeleteProject_IdIsNotValid_NotFoundResult()
        {
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.RemoveProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Throws(new ProjectsException(ProjectsErrors.NotFound));
            var response = this.deleteProject.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"));
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public void DeleteProject_SystemError_InternalServerError()
        {
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(new Project());
            this.mockService.Setup(service => service.RemoveProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Throws(new Exception());
            var response = this.deleteProject.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"));
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
