namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class InvitationsInMemoryRepository : IInvitationsRepository
    {
        private static Invitation[] invitations = new Invitation[]
        {
            new Invitation()
            {
                Id = new Guid("777939fd-59de-44bd-a092-f5d8434584df"),
                ProjectId = "f2879e14-fd99-4364-8e18-7a1a07f3ea55",
                ProjectName = "TeamUp",
                ResumeId = 1,
                ResumeName = "Jose Ecos",
                PictureResume = "photo.png",
                TextInvitation = "We invite you to collaborate with the development team",
                StartDate = DateTime.Today.AddDays(-10),
                ExpireDate = DateTime.Today.AddDays(+10),
                Status = "invited"
            },
            new Invitation()
            {
                Id = new Guid("777939fd-7777-44bd-a092-f5d8434584df"),
                ProjectId = "f2879e14-fd99-4364-8e18-7a1a07f3ea55",
                ProjectName = "TeamUp",
                ResumeId = 2,
                ResumeName = "Pedro",
                PictureResume = "photo.png",
                TextInvitation = "We invite you to collaborate with the development team",
                StartDate = DateTime.Today.AddDays(-10),
                ExpireDate = DateTime.Today.AddDays(+10),
                Status = "invited"
            }
        };

        public Invitation Add(Invitation project)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invitation> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invitation> GetAllInvitationsByProjectId(string projectId)
        {
            return invitations.Where(x => Equals(x.ProjectId, projectId));
        }

        public IEnumerable<Invitation> GetAllInvitationsByResumeId(int resumeId)
        {
            return invitations.Where(x => Equals(x.ResumeId, resumeId));
        }

        public Invitation GetById(Guid id)
        {
            return invitations.FirstOrDefault(x => Equals(x.Id, id));
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Invitation UpdateById(Invitation invitation)
        {
            var invitationsList = new List<Invitation>(invitations);
            Invitation result = invitationsList.FirstOrDefault(x => Equals(x.Id, invitation.Id));
            invitationsList.Remove(result);
            invitationsList.Add(invitation);
            invitations = new List<Invitation>(invitationsList).ToArray();
            return invitation;
        }
    }
}
