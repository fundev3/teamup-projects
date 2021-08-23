namespace Jalasoft.TeamUp.Projects.API.Tests.Utils
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
                Id = new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"),
                Name = "TeamUp",
                Contact = new Contact()
                {
                    Name = "Jose Ecos",
                    IdResume = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")
                },
                Description = "Centralize resumes and project",
                Logo = "https://www.example.com/images/dinosaur.jpg",
                MemberList = new Contact[1]
        {
                        new Contact
                        {
                            Name = "Paola Quintanilla",
                            IdResume = new Guid("536316e6-f8f6-41ea-b1ce-455b92be9303")
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
                            Name = "React.js"
                        }
                    },
                TextInvitation = "You are invited to be part of TeamUp",
                CreationDate = DateTime.Today.AddDays(-10),
            };
            return stubProject;
        }
    }
}
