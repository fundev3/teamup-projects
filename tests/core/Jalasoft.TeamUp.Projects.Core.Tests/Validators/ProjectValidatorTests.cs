namespace Jalasoft.TeamUp.Projects.Core.Tests.Validators
{
    using System;
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

        [Fact]
        public void Should_Have_Pass_Id_is_Valid()
        {
            // Arrange
            var project = new Project { Name = "TeamUp", Id = Guid.NewGuid() };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldNotHaveValidationErrorFor(project => project.Id);
        }

        [Fact]
        public void Should_Have_Error_When_Name_is_Empty()
        {
            // Arrange
            var project = new Project { Name = " " };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor("Name");
        }

        [Fact]
        public void Should_Have_Error_When_Name_is_Null()
        {
            // Arrange
            var project = new Project { Name = null };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor("Name");
        }

        [Fact]
        public void Should_Have_Error_When_Name_Contain_Numbers()
        {
            // Arrange
            var project = new Project { Name = "TeamUp 78484" };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor("Name");
        }

        [Fact]
        public void Should_Have_Error_When_Description_is_More_Than_160_Characters()
        {
            // Arrange
            var project = new Project { Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam faucibus in eros eu hendrerit. Nulla a nunc eget est tempor placerat at nec diam. Donec at tincidunt." };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor(project => project.Description);
            result.ShouldHaveValidationErrorFor(project => project.Description).WithErrorMessage("It must not have more than 160 characters");
        }

        [Fact]
        public void Should_Have_Error_When_Text_Invitation_is_More_Than_160_Characters()
        {
            // Arrange
            var project = new Project { TextInvitation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam faucibus in eros eu hendrerit. Nulla a nunc eget est tempor placerat at nec diam. Donec at tincidunt." };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor(project => project.TextInvitation);
            result.ShouldHaveValidationErrorFor(project => project.TextInvitation).WithErrorMessage("It must not have more than 160 characters");
        }

        [Fact]
        public void Should_Have_Error_When_Extesion_logo_is_Invalid()
        {
            // Arrange
            var project = new Project { Logo = "noextensionvalid.com" };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor("Logo");
        }

        [Fact]
        public void Should_Have_Error_When_Date_is_Invalid()
        {
            // Arrange
            var project = new Project { CreationDate = default(DateTime) };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldHaveValidationErrorFor(project => project.CreationDate);
        }

        [Fact]
        public void Should_Not_Have_Pass_When_State_is_Bool()
        {
            // Arrange
            var project = new Project { State = true };

            // Act
            var result = this.validator.TestValidate(project);

            // Assert
            result.ShouldNotHaveValidationErrorFor(project => project.State);
        }

        [Fact]
        public void Validation_Project_Success()
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
