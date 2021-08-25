namespace Jalasoft.TeamUp.Projects.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.API.Tests.Utils;
    using Jalasoft.TeamUp.Projects.DAL;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Moq;
    using Xunit;

    public class ProjectsServiceTests
    {
        private readonly Mock<IProjectsRepository> mockRepository;
        private readonly ProjectsService service;

        public ProjectsServiceTests()
        {
            this.mockRepository = new Mock<IProjectsRepository>();
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
                    Skills = new Skill[2]
                    {
                        new Skill
                        {
                            SkillId = "KS125LS6N7WP4S6SFTCK",
                            Name = "Python (Programming Language)"
                        },
                        new Skill
                        {
                            SkillId = "KSDJCA4E89LB98JAZ7LZ",
                            Name = "React.js"
                        }
                    },
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
                    Skills = new Skill[1]
                    {
                        new Skill
                        {
                            SkillId = "KS125LS6N7WP4S6SFTAM",
                            Name = "C#"
                        }
                    },
                    TextInvitation = "laboriosam cumque consequatur",
                    CreationDate = DateTime.Today.AddDays(-5),
                },
                new Project
                {
                    Id = new Guid("5a7939fd-59de-33bd-a092-f5d8434584db"),
                    Name = "UnitTest",
                    Contact = new Contact()
                    {
                        Name = "Jose Ecos",
                        IdResume = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584da")
                    },
                    Description = "Molestiae numquam possimus sit delectus. Sit ut consequatur est magni. Dolorem voluptatum et distinctio omnis et sit et. Ea soluta optio saepe ea voluptatem pariatur voluptas qui nihil.",
                    Logo = "https://www.example.com/images/dinosaur.jpg",
                    MemberList = new Contact[1]
                    {
                        new Contact
                        {
                            Name = "Paulo",
                            IdResume = new Guid("536316e6-f8f6-41ea-b1ce-455b92be9304")
                        }
                    },
                    State = true,
                    Skills = new Skill[1]
                    {
                        new Skill
                        {
                            SkillId = "AS125LS6N7WP4S6SFTAM",
                            Name = "C#"
                        }
                    },
                    TextInvitation = "We invite you",
                    CreationDate = DateTime.Today.AddDays(-5),
                }
            };
            return projects;
        }

        [Fact]
        public void GetProject_IdIsValid_SingleProject()
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
        public void GetProjects_ProjectsDontExist_EmptyArray()
        {
            // Arrange
            var stubEmptyProjectList = new List<Project>();
            this.mockRepository.Setup(repository => repository.GetAll()).Returns(stubEmptyProjectList);

            // Act
            var result = this.service.GetProjects(null);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetProjects_ProjectsExist_ProjectsArray()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetAll()).Returns(MockProjects());

            // Act
            var result = this.service.GetProjects(null);

            // Assert
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void DeleteProjectById_IdIsValid_ProjectRemoved()
        {
            // Arrange
            var stubProject = new Project { Id = Guid.NewGuid() };

            var memoryRepository = new ProjectsInMemoryRepository();
            var projectService = new ProjectsService(memoryRepository);
            var list = projectService.GetProjects(null);
            var countBeforeDelete = list.Length;

            // Act
            projectService.RemoveProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"));
            var countAfterDelete = projectService.GetProjects(null).Length;

            // Assert
            Assert.NotEqual(countBeforeDelete, countAfterDelete);
        }

        [Fact]
        public void UpdateProjectById_ProjectIsValid_ProjectUpdated()
        {
            // Arrange
            Project stubProject = StubProject.GetStubProject();

            this.mockRepository.Setup(repository => repository.GetById(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(stubProject);

            // Act
            var result = this.service.UpdateProject(stubProject);

            // Assert
            Assert.IsType<Project>(result);
        }

        [Fact]
        public void GetProjectsBySkill_SkillIsInProject_ProjectsArray()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetAllBySkill("C#")).Returns(MockProjects());

            // Act
            var result = this.service.GetProjects("C#");

            // Assert
            Assert.IsType<Project[]>(result);
        }

        [Fact]
        public void GetProjectsBySkill_SkillDoesntExist_EmptyArray()
        {
            // Arrange
            var stubEmptyProjectList = new List<Project>();
            this.mockRepository.Setup(repository => repository.GetAllBySkill("Pedro#")).Returns(stubEmptyProjectList);

            // Act
            var result = this.service.GetProjects("Pedro#");

            // Assert
            Assert.Empty(result);
        }
    }
}