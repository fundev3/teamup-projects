namespace Jalasoft.TeamUp.Projects.Models
{
    using System;

    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Contact Contact { get; set; }

        public bool State { get; set; }

        public string TextInvitation { get; set; }

        public string Logo { get; set; }

        public Contact[] Contacts { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
