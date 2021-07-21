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

    public class GetProjectAPITests
    {
        private readonly Mock<IProjectsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetProject getProject;

        public GetProjectAPITests()
        {
            this.mockService = new Mock<IProjectsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getProject = new GetProject(this.mockService.Object);
        }

        [Fact]
        public void GetProject_Returns_OkObjectResult()
        {
            var request = this.mockHttpContext.Request;
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.IsType<OkObjectResult>(this.getProject.Run(request));
        }

        [Fact]
        public void GetProject_Returns_NoOkResult()
        {
            var request = this.mockHttpContext.Request;
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.IsNotType<CreatedResult>(this.getProject.Run(request));
        }

        [Fact]
        public void GetProject_Returns_Project()
        {
            this.mockService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(new Project());
            var request = this.mockHttpContext.Request;
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584de"));
            var result = this.getProject.Run(request);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var project = Assert.IsType<Project>(okObjectResult.Value);
            Assert.NotNull(project);
        }

        [Fact]
        public void GetProject_Returns_Null()
        {
            this.mockService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Equals(null);
            var request = this.mockHttpContext.Request;
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584dd"));
            var result = this.getProject.Run(request);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okObjectResult.Value);
        }

        private static Dictionary<string, StringValues> CreateDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                { key, value }
            };
            return qs;
        }
    }
}
