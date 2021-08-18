namespace Jalasoft.TeamUp.Projects.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Moq;
    using Xunit;

    public class InvitationsServiceTests
    {
        private readonly Mock<IInvitationsRepository> mockRepository;
        private readonly InvitationsService service;

        public InvitationsServiceTests()
        {
            this.mockRepository = new Mock<IInvitationsRepository>();
            this.service = new InvitationsService(this.mockRepository.Object);
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
                    Status = "invited"
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
                    Status = "invited"
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
        public void GetInvitationsByProjectId_ProjectIdIsValid_EnumerableInvitations()
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
    }
}
