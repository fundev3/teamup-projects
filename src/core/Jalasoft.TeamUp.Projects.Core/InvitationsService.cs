namespace Jalasoft.TeamUp.Projects.Core
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
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
    }
}
