﻿namespace Jalasoft.TeamUp.Projects.Models
{
    using System;
    using System.Collections.Generic;

    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Contact Contact { get; set; }

        public bool State { get; set; }

        public string TextInvitation { get; set; }

        public string Logo { get; set; }

        public List<Contact> MemberList { get; set; }

        public Skill[] Skills { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
