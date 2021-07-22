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
        public void GetProject_Returns_OkObjectResult_Project()
        {
            this.mockService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(new Project());
            var request = this.mockHttpContext.Request;
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584de"));
            var okObjectResult = Assert.IsType<OkObjectResult>(this.getProject.Run(request));
            Assert.IsType<Project>(okObjectResult.Value);
        }

        [Fact]
        public void GetProject_Returns_NotFoundResult()
        {
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Equals(null);
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584de"));
            var notfountObjectResult = Assert.IsType<NotFoundObjectResult>(this.getProject.Run(request));
            Assert.Null(notfountObjectResult.Value);
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
