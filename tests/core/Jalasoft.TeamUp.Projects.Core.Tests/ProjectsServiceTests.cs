﻿namespace Jalasoft.TeamUp.Projects.Core.Tests
{
    using System;
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

        [Fact]
        public void GetProject_Returns_SingleProject()
        {
            // Arrange
            var stubProject = new Project { Id = Guid.NewGuid() };
            this.mockRepository.Setup(repository => repository.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(stubProject);

            // Act
            var result = this.service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"));

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetProject_Returns_Null()
        {
            // Arrange
            Project stubProject = null;
            this.mockRepository.Setup(repository => repository.GetProject(Guid.Parse("4a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(stubProject);

            // Act
            var result = this.service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"));

            // Assert
            Assert.Null(result);
        }
    }
}