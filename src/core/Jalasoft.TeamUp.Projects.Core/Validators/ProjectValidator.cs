namespace Jalasoft.TeamUp.Projects.Core.Validators
{
    using System;
    using FluentValidation;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            this.RuleFor(project => project.Id)
                .NotEmpty().NotNull();

            this.RuleFor(project => project.Name)
                .Length(3, 15)
                .Matches("^[a-zñ A-ZÑ]+$")
                .NotEmpty().NotNull();

            this.RuleFor(project => project.Description)
                .MaximumLength(160);

            this.RuleFor(project => project.TextInvitation)
                .MaximumLength(160);

            this.RuleFor(project => project.Logo)
                .Matches("[^\\s]+(.*?)\\.(jpg|jpeg|png|JPG|JPEG|PNG)$");
        }
    }
}
