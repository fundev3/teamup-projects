namespace Jalasoft.TeamUp.Projects.Core.Tests
{
    using System;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Moq;
    using Xunit;

    public class PostProjectCoreTests
    {
        private readonly Mock<IProjectsRepository> mockRepository;
        private readonly ProjectsService projectsService;

        public PostProjectCoreTests()
        {
            this.mockRepository = new Mock<IProjectsRepository>();
            this.projectsService = new ProjectsService(this.mockRepository.Object);
        }

        [Fact]
        public void PostProject_Return_Project()
        {
            var stubProject = new Project()
            {
                Name = "Name Example",
                Description = "Description Example",
                Contact = new Contact()
                {
                    Name = "Jose Ecos",
                    IdResume = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")
                },
                Logo = "https://www.example.com/images/dinosaur.jpg",
                MemberList = new Contact[1]
                {
                    new Contact
                    {
                        Name = "Paola Quintanilla",
                        IdResume = new Guid("536316e6-f8f6-41ea-b1ce-455b92be9303")
                    }
                },
                State = true,
                TextInvitation = "You are invited to be part of TeamUp",
                CreationDate = DateTime.Today.AddDays(-10)
            };
            this.mockRepository.Setup(repository => repository.Add(stubProject)).Returns(new Project());
            var result = this.projectsService.PostProject(stubProject);
            Assert.IsType<Project>(result);
        }
    }
}
