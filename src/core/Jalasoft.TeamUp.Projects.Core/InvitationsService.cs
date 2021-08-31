namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using System.Linq;
    using FluentValidation;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Core.Validators;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.Models.Enums;

    public class InvitationsService : IInvitationsService
    {
        private readonly IInvitationsRepository invitationsRepository;
        private readonly IProjectsRepository projectsRepository;

        public InvitationsService(IInvitationsRepository invitationsRepository, IProjectsRepository projectsRepository)
        {
            this.invitationsRepository = invitationsRepository;
            this.projectsRepository = projectsRepository;
        }

        public Invitation[] GetInvitationsByProjectId(string projectId)
        {
            var result = this.invitationsRepository.GetAllInvitationsByProjectId(projectId).ToArray();
            return result;
        }

        public Invitation[] GetInvitationsByResumeId(int resumeId)
        {
            return this.invitationsRepository.GetAllInvitationsByResumeId(resumeId).ToArray();
        }

        public Invitation GetInvitation(Guid invitationId)
        {
            return this.invitationsRepository.GetById(invitationId);
        }

        public Invitation UpdateInvitation(Invitation invitation)
        {
            InvitationValidator validator = new InvitationValidator();
            validator.ValidateAndThrow(invitation);
            var invitationStatus = InvitationStatus.Accepted;
            if (invitation.Status == invitationStatus.ToString())
            {
                Contact contact = new Contact
                {
                    IdResume = invitation.ResumeId,
                    Name = invitation.ResumeName
                };

                var project = this.projectsRepository.GetById(Guid.Parse(invitation.ProjectId));
                project.MemberList.Add(contact);
                this.projectsRepository.UpdateById(project);
            }

            var result = this.invitationsRepository.UpdateById(invitation);
            return result;
        }

        public Invitation PostInvitation(Invitation invitation)
        {
            invitation.Id = Guid.NewGuid();
            InvitationValidator validator = new InvitationValidator();
            validator.ValidateAndThrow(invitation);
            var result = this.invitationsRepository.Add(invitation);
            return result;
        }
    }
}
