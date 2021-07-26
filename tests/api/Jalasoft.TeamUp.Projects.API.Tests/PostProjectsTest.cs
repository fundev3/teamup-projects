namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.IO;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Newtonsoft.Json;
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

        public static Stream SetStream(string body)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [Fact]
        public void PostProject_Returns_OkObjectResult_Project()
        {
            var project = new Project()
            {
                Name = "TeamUp",
                Contact = new Contact()
                {
                    Name = "Jose Ecos",
                    IdResume = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")
                },
                Description = "Centralize resumes and project",
                Logo = "It's a logo",
                MemberList = new Contact[1]
                {
                    new Contact
                    {
                        Name = "Luis",
                        IdResume = new Guid("536316e6-f8f6-41ea-b1ce-455b92be9303")
                    }
                },
                State = true,
                TextInvitation = "You are invited to be part of TeamUp",
                CreationDate = DateTime.Today.AddDays(-10)
            };
            string body = JsonConvert.SerializeObject(project);
            this.mockHttpContext.Request.Headers.Add("post", "application/json");
            this.mockHttpContext.Request.Body = SetStream(body);
            this.mockProjectsService.Setup(service => service.PostProject(new Project())).Returns(new Project());
            var response = this.postProject.Run();
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Project>(okObjectResult.Value);
        }
    }
}
