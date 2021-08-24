namespace Jalasoft.TeamUp.Projects.Core.Tests.Validators
{
    using System;
    using System.Collections.Generic;
    using FluentValidation.TestHelper;
    using Jalasoft.TeamUp.Projects.Core.Validators;
    using Jalasoft.TeamUp.Projects.Models;
    using Xunit;

    public class ProjectValidatorTests
    {
        private readonly ProjectValidator validator;

        public ProjectValidatorTests()
        {
            this.validator = new ProjectValidator();
        }

        public static IEnumerable<object[]> Projects =>
        new List<object[]>
        {
            new object[] { new Project { Name = " " } },
            new object[] { new Project { Name = null } },
            new object[] { new Project { Name = "TeamUp 78484" } }
        };

        [Fact]
        public void ProjectValidator_IdIsValid_ValidProject()
        {
            // Arrange
            var project = new Project { Name = "TeamUp", Id = Guid.NewGuid() };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldNotHaveValidationErrorFor(project => project.Id);
        }

        [Theory]
        [MemberData(nameof(Projects))]
        public void ProjectValidator_NameIsNotValid_ThrowsError(Project project)
        {
            // Arrange
            var projectToTest = project;

            // Act
            var result = this.validator.TestValidate(projectToTest);

            // Assert
            result.ShouldHaveValidationErrorFor("Name");
        }

        [Fact]
        public void ProjectValidator_DescriptionHasMoreThan160Characters_ThrowsError()
        {
            // Arrange
            var project = new Project { Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam faucibus in eros eu hendrerit. Nulla a nunc eget est tempor placerat at nec diam. Donec at tincidunt." };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor(project => project.Description);
        }

        [Fact]
        public void ProjectValidator_TextInvitationHasMoreThan160Characters_ThrowsError()
        {
            // Arrange
            var project = new Project { TextInvitation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam faucibus in eros eu hendrerit. Nulla a nunc eget est tempor placerat at nec diam. Donec at tincidunt." };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor(project => project.TextInvitation);
        }

        [Fact]
        public void ProjectValidator_LogoExtensionNotValid_ThrowsError()
        {
            // Arrange
            var project = new Project { Logo = "noextensionvalid.com" };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor("Logo");
        }

        [Fact]
        public void ProjectValidator_CreatingNewValidProject_ValidProject()
        {
            // Arrange
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = "TeamUp",
                Logo = "TeamUp.jpg",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus turpis sem, eleifend eu nulla a, ultricies convallis lectus. Duis volutpat mi at ante tortor.",
                State = true,
                TextInvitation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus turpis sem, eleifend eu nulla a, ultricies convallis lectus. Duis volutpat mi at ante tortor.",
                CreationDate = DateTime.Today.AddDays(-10)
            };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
