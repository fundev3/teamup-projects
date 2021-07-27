namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsRepository : IProjectsRepository
    {
        private static readonly Project[] Projects = new Project[]
            {
                new Project
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
                    TextInvitation = "You are invited to be part of TeamUp",
                    CreationDate = DateTime.Today.AddDays(-10),
                }
            };

        public Project Create()
        {
            throw new NotImplementedException();
        }

        public List<Project> GetAll()
        {
            throw new NotImplementedException();
        }

        public Project GetById(Guid id)
        {
            Project result = Projects.FirstOrDefault(p => Equals(p.Id, id));
            return result;
        }
    }
}
