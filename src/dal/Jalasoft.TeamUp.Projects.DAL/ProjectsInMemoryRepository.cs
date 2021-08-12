namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsInMemoryRepository : IProjectsInMemoryRepository
    {
        private static Project[] projects = new Project[]
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
                },
                new Project
                {
                    Id = new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"),
                    Name = "Lueilwitz Group",
                    Contact = new Contact()
                    {
                        Name = "Jose Ecos",
                        IdResume = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")
                    },
                    Description = "Molestiae numquam possimus sit delectus. Sit ut consequatur est magni. Dolorem voluptatum et distinctio omnis et sit et. Ea soluta optio saepe ea voluptatem pariatur voluptas qui nihil.",
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
                    TextInvitation = "laboriosam cumque consequatur",
                    CreationDate = DateTime.Today.AddDays(-5),
                }
            };

        public Project GetById(Guid id)
        {
            Project result = projects.FirstOrDefault(p => Equals(p.Id, id));
            return result;
        }

        public IEnumerable<Project> GetAll()
        {
            return projects;
        }

        public Project Add(Project project)
        {
            projects = new List<Project>(projects) { project }.ToArray();
            return project;
        }

        public void Remove(Guid id)
        {
            int projectIndex = Array.IndexOf(projects, this.GetById(id));
            projects = projects.Where((val, idx) => idx != projectIndex).ToArray();
        }
    }
}
