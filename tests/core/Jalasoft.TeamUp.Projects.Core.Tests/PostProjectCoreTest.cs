namespace Jalasoft.TeamUp.Projects.Core.Tests
{
    using System;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Moq;
    using Xunit;

    public class PostProjectCoreTest
    {
        private readonly Mock<IProjectsRepository> mockRepository;
        private readonly ProjectsService projectsService;

        public PostProjectCoreTest()
        {
            this.mockRepository = new Mock<IProjectsRepository>();
            this.projectsService = new ProjectsService(this.mockRepository.Object);
        }

        [Fact]
        public void PostProject_Return_Project()
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
                    Name = "Nelson Harris"
                }
            };
            this.mockRepository.Setup(repository => repository.PostProject(project1)).Returns(new Project());
            var result = this.projectsService.PostProject(project1);
            Assert.IsType<Project>(result);
        }

        [Fact]
        public void PostProject_Return_Null()
        {
            Project project1 = new Project();
            this.mockRepository.Setup(repository => repository.PostProject(project1)).Equals(null);
            var result = this.projectsService.PostProject(project1);
            Assert.Null(result);
        }
    }
}
