namespace Jalasoft.TeamUp.Projects.Models
{
    using System;

    public class Invitation
    {
        public Guid Id { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int ResumeId { get; set; }

        public string ResumeName { get; set; }

        public string PictureResume { get; set; }

        public string TextInvitation { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public string Status { get; set; }
    }
}
