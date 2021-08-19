namespace Jalasoft.TeamUp.Projects.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IInvitationsService
    {
        Invitation[] GetInvitationsByResumeId(int resumeId);

        Invitation[] GetInvitationsByProjectId(string projectId);

        Invitation GetInvitation(Guid invitationId);

        Invitation UpdateInvitation(Invitation invitation);
    }
}
