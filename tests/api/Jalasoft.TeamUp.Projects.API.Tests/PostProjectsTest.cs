namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.IO;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Microsoft.AspNetCore.Http;
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
            var obj = new Project()
            {
                Name = "Project Name",
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
            string stream = JsonConvert.SerializeObject(obj);
            var result = SetStream(stream);
            var request = this.mockHttpContext.Request;
            this.mockProjectsService.Setup(service => service.PostProject(obj)).Returns(new Project());
            var response = this.postProject.Run(request);
        }
    }
}
