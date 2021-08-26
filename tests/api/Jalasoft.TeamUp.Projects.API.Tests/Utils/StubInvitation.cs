namespace Jalasoft.TeamUp.Projects.API.Tests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.Models;

    public class StubInvitation
    {
        public static Invitation GetStubInvitation()
        {
            Invitation stubInvitation = new Invitation
            {
                Id = new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"),
                ProjectId = "5a7939fd-59de-44bd-a092-f5d8434584df",
                ProjectName = "TeamUp",
                ResumeId = 3,
                ResumeName = "Jose",
                PictureResume = "https://www.example.com/images/dinosaur.jpg",
                Status = "Invited",
                TextInvitation = "You are invited to be part of TeamUp",
                StartDate = DateTime.Today.AddDays(-10),
                ExpireDate = DateTime.Today.AddDays(+10),
            };
            return stubInvitation;
        }

        public static Invitation GetBadStubInvitation()
        {
            Invitation stubInvitation = new Invitation
            {
                Id = new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"),
                ProjectId = "5a7939fd-59de-44bd-a092-f5d8434584df",
                ProjectName = "TeamUp",
                ResumeId = 3,
                ResumeName = "Jose",
                PictureResume = "https://www.example.com/images/dinosaur.jpg",
                Status = "Pedro",
                TextInvitation = "You are invited to be part of TeamUp",
                StartDate = DateTime.Today.AddDays(-10),
                ExpireDate = DateTime.Today.AddDays(+10),
            };
            return stubInvitation;
        }
    }
}