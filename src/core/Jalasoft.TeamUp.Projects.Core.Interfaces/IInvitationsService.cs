namespace Jalasoft.TeamUp.Projects.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IInvitationsService
    {
        Invitation[] GetInvitationsByResumeId(int resumeId);

        Invitation[] GetInvitationsByProjectId(string projectId);

        Invitation PostInvitation(Invitation invitation);
    }
}
