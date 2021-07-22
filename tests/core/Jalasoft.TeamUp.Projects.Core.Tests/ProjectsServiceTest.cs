namespace Jalasoft.TeamUp.Projects.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Moq;
    using Xunit;

    public class ProjectsServiceTest
    {
        private readonly Mock<IProjectRepository> mockRepository;
        private readonly ProjectService projectService;

        public ProjectsServiceTest()
        {
            this.mockRepository = new Mock<IProjectRepository>();
            this.projectService = new ProjectService(this.mockRepository.Object);
        }

        [Fact]
        public void GetProjects_EmptyResult()
        {
            this.mockRepository.Setup(repository => repository.GetProjects()).Returns(new List<Project>());
            var result = this.projectService.GetProjects();
            Assert.Empty(result);
        }

        [Fact]
        public void GetProjects_AllItemsResult()
        {
            this.mockRepository.Setup(respository => respository.GetProjects()).Returns(this.MockProjects);
            var result = this.projectService.GetProjects();
            Assert.Equal(2, result.Length);
        }

        public IEnumerable<Project> MockProjects()
        {
            var projects = new List<Project>
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
