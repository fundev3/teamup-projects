namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using System.Linq;
    using FluentValidation;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Core.Validators;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class InvitationsService : IInvitationsService
    {
        private readonly IInvitationsRepository invitationsRepository;

        public InvitationsService(IInvitationsRepository invitationsRepository)
        {
            this.invitationsRepository = invitationsRepository;
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
