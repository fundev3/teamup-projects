﻿namespace Jalasoft.TeamUp.Projects.API.Tests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.Models;

    public class StubProject
    {
        public static Project GetStubProject()
        {
            Project stubProject = new Project
            {
                Id = new Guid("5a7939fd-59de-44bd-a092-f5d8434584df"),
                Name = "TeamUp",
                Contact = new Contact()
                {
                    Name = "Jose Ecos",
                    IdResume = 1
                },
                Description = "Centralize resumes and project",
                Logo = "https://www.example.com/images/dinosaur.jpg",
                MemberList = new List<Contact>
        {
                        new Contact
                        {
                            Name = "Paola Quintanilla",
                            IdResume = 2
                        }
        },
                State = true,
                Skills = new Skill[2]
                    {
                        new Skill
                        {
                            SkillId = "KS125LS6N7WP4S6SFTCK",
                            Name = "Python (Programming Language)"
                        },
                        new Skill
                        {
                            SkillId = "KSDJCA4E89LB98JAZ7LZ",
                            Name = "C#"
                        }
                    },
                TextInvitation = "You are invited to be part of TeamUp",
                CreationDate = DateTime.Today.AddDays(-10),
            };
            return stubProject;
        }
    }
}
