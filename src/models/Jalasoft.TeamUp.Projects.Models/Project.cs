namespace Jalasoft.TeamUp.Projects.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool State { get; set; }

        public string TextInvitation { get; set; }

        public string Logo { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public List<Contact> Contacts { get; set; }

        public Contact Contact { get; set; }
    }
}
