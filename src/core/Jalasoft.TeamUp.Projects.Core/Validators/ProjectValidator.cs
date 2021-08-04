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
                .Length(3, 70)
                .Matches("^[a-zñ A-ZÑ]+$")
                .NotEmpty().NotNull();

            this.RuleFor(project => project.Description)
                .MaximumLength(160)
                .WithMessage("It must not have more than 160 characters");

            this.RuleFor(project => project.State)
                .NotNull()
                .Must(x => x == false || x == true);

            this.RuleFor(project => project.TextInvitation)
                .MaximumLength(160)
                .WithMessage("It must not have more than 160 characters");

            this.RuleFor(project => project.Logo)
                .Matches("[^\\s]+(.*?)\\.(jpg|jpeg|png|JPG|JPEG|PNG)$")
                .WithMessage("The logo extension is not valid");

            this.RuleFor(project => project.CreationDate)
                .Must(BeAValidDate)
                .WithMessage("Creation date is required");
        }

        private static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
