namespace Jalasoft.TeamUp.Projects.Core.Tests
{
    using System;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Moq;
    using Xunit;

    public class GetProjectCoreTests
    {
        private readonly Mock<IProjectsRepository> mockRepository;
        private readonly ProjectsService service;

        public GetProjectCoreTests()
        {
            this.mockRepository = new Mock<IProjectsRepository>();
            this.service = new ProjectsService(this.mockRepository.Object);
        }

        [Fact]
        public void GetProject_Returns_Project()
        {
            this.mockRepository.Setup(repository => repository.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(new Project());
            var result = this.service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.NotNull(result);
        }

        [Fact]
        public void GetProject_Returns_Null()
        {
            this.mockRepository.Setup(repository => repository.GetProject(Guid.Parse("4a7939fd-59de-44bd-a092-f5d8434584de"))).Equals(null);
            var result = this.service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.Null(result);
        }
    }
}
