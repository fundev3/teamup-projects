namespace Jalasoft.TeamUp.Projects.Models
{
    using System;
    using System.Collections.Generic;

    public class Project
    {

        public Contact Contact { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; }

        public string Logo { get; set; }

        public List<Contact> MemberList { get; set; }

        public string Name { get; set; }

        public bool State { get; set; }

        public string TextInvitation { get; set; }
    }
}
