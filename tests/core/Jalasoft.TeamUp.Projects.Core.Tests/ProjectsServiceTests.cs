namespace Jalasoft.TeamUp.Projects.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Moq;
    using Xunit;

    public class ProjectsServiceTests
    {
        private readonly Mock<IRepository<Project>> mockRepository;
        private readonly ProjectsService service;

        public ProjectsServiceTests()
        {
            this.mockRepository = new Mock<IRepository<Project>>();
            this.service = new ProjectsService(this.mockRepository.Object);
        }

        public static IEnumerable<Project> MockProjects()
        {
            var projects = new List<Project>
            {
                new Project
                {
                    Id = new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"),
                    Name = "TeamUp",
                    Contact = new Contact()
                    {
                        Name = "Jose Ecos",
                        IdResume = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")
                    },
                    Description = "Centralize resumes and project",
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
                    CreationDate = DateTime.Today.AddDays(-10),
                },
                new Project
                {
                    Id = new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"),
                    Name = "Lueilwitz Group",
                    Contact = new Contact()
                    {
                        Name = "Jose Ecos",
                        IdResume = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")
                    },
                    Description = "Molestiae numquam possimus sit delectus. Sit ut consequatur est magni. Dolorem voluptatum et distinctio omnis et sit et. Ea soluta optio saepe ea voluptatem pariatur voluptas qui nihil.",
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
                    TextInvitation = "laboriosam cumque consequatur",
                    CreationDate = DateTime.Today.AddDays(-5),
                }
            };
            return projects;
        }

        [Fact]
        public void GetProject_Returns_SingleProject()
        {
            // Arrange
            var stubProject = new Project { Id = Guid.NewGuid() };
            this.mockRepository.Setup(repository => repository.GetById(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(stubProject);

            // Act
            var result = this.service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"));

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetProject_Throws_ProjectsException()
        {
            // Arrange
            Project stubProject = null;
            this.mockRepository.Setup(repository => repository.GetById(Guid.Parse("4a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(stubProject);

            // Assert
            Assert.Throws<ProjectsException>(() => this.service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")));
        }

        [Fact]
        public void GetProjects_Returns_EmptyResult()
        {
            // Arrange
            var stubEmptyProjectList = new List<Project>();
            this.mockRepository.Setup(repository => repository.GetAll()).Returns(stubEmptyProjectList);

            // Act
            var result = this.service.GetProjects();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetProjects_Returns_AllItemsResult()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetAll()).Returns(MockProjects());

            // Act
            var result = this.service.GetProjects();

            // Assert
            Assert.Equal(2, result.Length);
        }
    }
}
