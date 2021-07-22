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
        public void PostProject_Returns_OkObjectResult_Project()
        {
            Project project1 = new Project()
            {
                Name = "Project Name example",
                Description = "This is a Description",
                State = true,
                TextInvitation = "You are invited",
                Logo = "This is a logo",
                CreationDate = new DateTimeOffset(DateTime.Now.Year, 4, 14, 10, 00, 00, new TimeSpan(7, 0, 0)),
                Contact = new Contact()
                {
                    Id = Guid.NewGuid(),
                    IdResume = Guid.NewGuid(),
                    Name = "Julio"
                }
            };
            var request = this.mockHttpContext.Request;
            this.mockProjectsService.Setup(service => service.PostProject(project1)).Returns(new Project());
            var response = this.postProject.Run(request);
        }
    }
}
