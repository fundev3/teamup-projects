namespace Jalasoft.TeamUp.Projects.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IInvitationsRepository : IRepository<Invitation>
    {
        public IEnumerable<Invitation> GetAllInvitationsByResumeId(int resumeId);

        public IEnumerable<Invitation> GetAllInvitationsByProjectId(string projectId);
    }
}
