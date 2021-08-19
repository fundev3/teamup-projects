namespace Jalasoft.TeamUp.Projects.Core.Tests.Validators
{
    using System;
    using System.Collections.Generic;
    using FluentValidation.TestHelper;
    using Jalasoft.TeamUp.Projects.Core.Validators;
    using Jalasoft.TeamUp.Projects.Models;
    using Xunit;

    public class InvitationValidatorTests
    {
        private readonly InvitationValidator validator;

        public InvitationValidatorTests()
        {
            this.validator = new InvitationValidator();
        }

        public static IEnumerable<object[]> Invitations =>
        new List<object[]>
        {
            new object[] { new Invitation { Status = "Invited" } },
            new object[] { new Invitation { Status = "Accepted" } },
            new object[] { new Invitation { Status = "Rejected" } }
        };

        [Fact]
        public void InvitationValidator_Valid_Id_Success()
        {
            // Arrange
            var invitation = new Invitation { Id = Guid.NewGuid() };

            // Act
            var result = this.validator.TestValidate(invitation);

            // Assert
            result.ShouldNotHaveValidationErrorFor(invitation => invitation.Id);
        }

        [Fact]
        public void InvitationValidator_Valid_ProjectId_ThrowsError()
        {
            // Arrange
            var invitation = new Invitation { ProjectId = " " };

            // Act
            var result = this.validator.TestValidate(invitation);

            // Assert
            result.ShouldHaveValidationErrorFor(invitation => invitation.ProjectId);
        }

        [Fact]
        public void InvitationValidator_Valid_ResumeId_ThrowsError()
        {
            // Arrange
            var invitation = new Invitation { ResumeId = 0 };

            // Act
            var result = this.validator.TestValidate(invitation);

            // Assert
            result.ShouldHaveValidationErrorFor(invitation => invitation.ResumeId);
        }

        [Fact]
        public void InvitationValidator_Valid_ProjectName_ThrowsError()
        {
            // Arrange
            var invitation = new Invitation { ProjectName = "@123^" };

            // Act
            var result = this.validator.TestValidate(invitation);

            // Assert
            result.ShouldHaveValidationErrorFor(invitation => invitation.ProjectName);
        }

        [Fact]
        public void InvitationValidator_Valid_ResumeName_ThrowsError()
        {
            // Arrange
            var invitation = new Invitation { ResumeName = "@123^" };

            // Act
            var result = this.validator.TestValidate(invitation);

            // Assert
            result.ShouldHaveValidationErrorFor(invitation => invitation.ResumeName);
        }

        [Theory]
        [MemberData(nameof(Invitations))]
        public void InvitationValidator_Validate_Status_Without_Errors_Success(Invitation invitation)
        {
            // Arrange
            var invitationToTest = invitation;

            // Act
            var result = this.validator.TestValidate(invitationToTest);

            // Assert
            result.ShouldNotHaveValidationErrorFor("Status");
        }

        [Fact]
        public void InvitationValidator_Text_Invitation_More_Than_160_characters_ThrowsError()
        {
            // Arrange
            var invitation = new Invitation { TextInvitation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam faucibus in eros eu hendrerit. Nulla a nunc eget est tempor placerat at nec diam. Donec at tincidunt." };

            // Act
            var result = this.validator.TestValidate(invitation);

            // Assert
            result.ShouldHaveValidationErrorFor(invitation => invitation.TextInvitation);
        }

        [Fact]
        public void InvitationValidator_Picture_Extension_Invalid_ThrowsError()
        {
            // Arrange
            var invitation = new Invitation { PictureResume = "noextensionvalid.com" };

            // Act
            var result = this.validator.TestValidate(invitation);

            // Assert
            result.ShouldHaveValidationErrorFor("PictureResume");
        }
    }
}