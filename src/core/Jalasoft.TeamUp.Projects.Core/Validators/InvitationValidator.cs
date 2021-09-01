namespace Jalasoft.TeamUp.Projects.Core.Validators
{
    using System;
    using System.Collections.Generic;
    using FluentValidation;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.Models.Enums;

    public class InvitationValidator : AbstractValidator<Invitation>
    {
        public InvitationValidator()
        {
            this.RuleFor(invitation => invitation.ResumeName)
                .Length(3, 30)
                .Matches("^[a-zñ A-ZÑ]+$")
                .NotEmpty();

            this.RuleFor(invitation => invitation.Id)
                .NotEmpty();

            this.RuleFor(invitation => invitation.ProjectId)
                .NotEmpty();

            this.RuleFor(invitation => invitation.ResumeId)
                .NotEmpty();

            this.RuleFor(invitation => invitation.ProjectName)
                .Length(3, 15)
                .Matches("^[a-zñ A-ZÑ]+$")
                .NotEmpty();

            this.RuleFor(invitation => invitation.TextInvitation)
                .MaximumLength(160);

            this.RuleFor(invitation => invitation.Status)
                .IsEnumName(typeof(InvitationStatus));

            this.RuleFor(invitation => invitation.PictureResume)
                .Matches("[^\\s]+(.*?)\\.(jpg|jpeg|png|JPG|JPEG|PNG)$");
        }
    }
}