namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.Collections.Generic;
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
        private readonly Mock<HttpRequest> mockRequest;
        private readonly GetProjects getResumes;

        public GetProjectsTest()
        {
            this.mockService = new Mock<IProjectsService>();
            this.mockRequest = new Mock<HttpRequest>();
            this.getResumes = new GetProjects(this.mockService.Object);
        }

        [Fact]
        public void GetProjects_Returns_OkObjectResult()
        {
            Assert.IsType<OkObjectResult>(this.getResumes.Run(this.mockRequest.Object));
        }

        [Fact]
        public void GetProjects_Returns_NotOkResult()
        {
            Assert.IsNotType<CreatedResult>(this.getResumes.Run(this.mockRequest.Object));
        }

        [Fact]
        public void GetProjects_Returns_EmptyResult()
        {
            this.mockService.Setup(service => service.GetProjects()).Returns(new Project[0]);

            var result = this.getResumes.Run(this.mockRequest.Object);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resumes = Assert.IsType<Project[]>(okObjectResult.Value);
            Assert.Empty(resumes);
        }

        [Fact]
        public void GetResumes_Returns_AllItemsResult()
        {
            this.mockService.Setup(service => service.GetProjects()).Returns(this.MockProjects());

            var result = this.getResumes.Run(this.mockRequest.Object);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resumes = Assert.IsType<Project[]>(okObjectResult.Value);
            Assert.Equal(2, resumes.Length);
        }

        public Project[] MockProjects()
        {
            var projects = new Project[]
        {
            new Project()
            {
                Contact = new Contact()
                {
                    Id = Guid.NewGuid(),
                    IdResume = Guid.NewGuid(),
                    Name = "Nelson Harris"
                },
                CreationDate = new DateTimeOffset(DateTime.Now.Year, 4, 14, 10, 00, 00, new TimeSpan(7, 0, 0)),
                Description = "Id reiciendis voluptatum.Quia nulla ullam occaecati quasi vitae.",
                Id = Guid.NewGuid(),
                Logo = "http://placeimg.com/640/480/technics",
                MemberList = new List<Contact>(),
                Name = "Daugherty - Witting",
                State = true,
                TextInvitation = "Et quia temporibus labore. Laudantium enim ut assumenda. Deserunt dolores deserunt nobis."
            },
            new Project()
            {
                Contact = new Contact()
                {
                    Id = Guid.NewGuid(),
                    IdResume = Guid.NewGuid(),
                    Name = "Nelson Harris"
                },
                CreationDate = new DateTimeOffset(DateTime.Now.Year, 5, 14, 10, 00, 00, new TimeSpan(7, 0, 0)),
                Description = "laboriosam ut consectetur",
                Id = Guid.NewGuid(),
                Logo = "http://placeimg.com/640/480/technics",
                MemberList = new List<Contact>(),
                Name = "Leticia Wiza",
                State = true,
                TextInvitation = "Id reiciendis voluptatum.Quia nulla ullam occaecati quasi vitae."
            }
        };
            return projects;
        }
    }
}
