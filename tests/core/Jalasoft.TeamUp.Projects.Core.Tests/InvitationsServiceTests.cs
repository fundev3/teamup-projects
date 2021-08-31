namespace Jalasoft.TeamUp.Projects.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.API.Tests.Utils;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Moq;
    using Xunit;

    public class InvitationsServiceTests
    {
        private readonly Mock<IInvitationsRepository> mockRepository;
        private readonly Mock<IProjectsRepository> mockProjectsRepository;
        private readonly InvitationsService service;

        public InvitationsServiceTests()
        {
            this.mockRepository = new Mock<IInvitationsRepository>();
            this.mockProjectsRepository = new Mock<IProjectsRepository>();

            this.service = new InvitationsService(this.mockRepository.Object, this.mockProjectsRepository.Object);
        }

        public static IEnumerable<Invitation> MockInvitations()
        {
            var invitations = new List<Invitation>
            {
                new Invitation
                {
                    Id = new Guid("777939fd-59de-44bd-a092-f5d8434584df"),
                    ProjectId = "f2879e14-fd99-4364-8e18-7a1a07f3ea55",
                    ProjectName = "TeamUp",
                    ResumeId = 1,
                    ResumeName = "Jose Ecos",
                    PictureResume = "photo.png",
                    TextInvitation = "We invite you to collaborate with the development team",
                    StartDate = DateTime.Today.AddDays(-10),
                    ExpireDate = DateTime.Today.AddDays(+10),
                    Status = "Invited"
                },
                new Invitation
                {
                    Id = new Guid("777939fd-7777-44bd-a092-f5d8434584df"),
                    ProjectId = "f2879e14-fd99-4364-8e18-7a1a07f3ea55",
                    ProjectName = "TeamUp",
                    ResumeId = 2,
                    ResumeName = "Pedro",
                    PictureResume = "photo.png",
                    TextInvitation = "We invite you to collaborate with the development team",
                    StartDate = DateTime.Today.AddDays(-10),
                    ExpireDate = DateTime.Today.AddDays(+10),
                    Status = "Invited"
                }
            };
            return invitations;
        }

        [Fact]
        public void GetInvitationsByResumeId_ResumeIdIsValid_EnumerableInvitations()
        {
            // Arrange
            var stubInvitation = new Invitation { Id = Guid.NewGuid() };
            this.mockRepository.Setup(repository => repository.GetAllInvitationsByResumeId(2)).Returns(MockInvitations);

            // Act
            var result = this.service.GetInvitationsByResumeId(2);

            // Assert
            Assert.IsType<Invitation[]>(result);
        }

        [Fact]
        public void GetInvitationsByResumeId_InvitationsDontExist_EmptyArray()
        {
            // Arrange
            var stubEmptyInvitationList = new List<Invitation>();
            this.mockRepository.Setup(repository => repository.GetAllInvitationsByResumeId(3)).Returns(stubEmptyInvitationList);

            // Act
            var result = this.service.GetInvitationsByResumeId(3);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetInvitationsByProjectId_ProjectIdIsValid_InvitationsArray()
        {
            // Arrange
            var stubInvitation = new Invitation { Id = Guid.NewGuid() };
            this.mockRepository.Setup(repository => repository.GetAllInvitationsByProjectId("f2879e14-fd99-4364-8e18-7a1a07f3ea55")).Returns(MockInvitations);

            // Act
            var result = this.service.GetInvitationsByProjectId("f2879e14-fd99-4364-8e18-7a1a07f3ea55");

            // Assert
            Assert.IsType<Invitation[]>(result);
        }

        [Fact]
        public void GetInvitationsByProjectId_InvitationsDontExist_EmptyArray()
        {
            // Arrange
            var stubEmptyInvitationList = new List<Invitation>();
            this.mockRepository.Setup(repository => repository.GetAllInvitationsByProjectId("f2879e14-fd99-4364-8e18-7a1a07f3ea77")).Returns(stubEmptyInvitationList);

            // Act
            var result = this.service.GetInvitationsByProjectId("f2879e14-fd99-4364-8e18-7a1a07f3ea77");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void PatchInvitationByProjectId_InvitationIsValid_InvitationUpdated()
        {
            // Arrange
            Invitation stubInvitation = StubInvitation.GetStubInvitation();

            this.mockRepository.Setup(repository => repository.UpdateById(stubInvitation)).Returns(stubInvitation);

            // Act
            var result = this.service.UpdateInvitation(stubInvitation);

            // Assert
            Assert.IsType<Invitation>(result);
        }

        [Fact]
        public void PostInvitation_InvitationIsValid_SingleInvitation()
        {
            var stubInvitation = StubInvitation.GetStubInvitation();
            this.mockRepository.Setup(repository => repository.Add(stubInvitation)).Returns(new Invitation());
            var result = this.service.PostInvitation(stubInvitation);
            Assert.IsType<Invitation>(result);
        }

        [Fact]
        public void PostInvitation_InvitationIsNotValid_ValidationException()
        {
            var badInvitation = StubInvitation.GetBadStubInvitation();
            this.mockRepository.Setup(repository => repository.Add(badInvitation)).Returns(new Invitation());

            Assert.Throws<FluentValidation.ValidationException>(() => this.service.PostInvitation(badInvitation));
        }
    }
}
