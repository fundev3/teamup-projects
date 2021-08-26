namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsInMemoryRepository : IProjectsRepository
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
                        IdResume = 1
                    },
                    Description = "Centralize resumes and project",
                    Logo = "https://www.example.com/images/dinosaur.jpg",
                    MemberList = new Contact[1]
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
                            Name = "React.js"
                        }
                    },
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
                        IdResume = 1
                    },
                    Description = "Molestiae numquam possimus sit delectus. Sit ut consequatur est magni. Dolorem voluptatum et distinctio omnis et sit et. Ea soluta optio saepe ea voluptatem pariatur voluptas qui nihil.",
                    Logo = "https://www.example.com/images/dinosaur.jpg",
                    MemberList = new Contact[1]
                    {
                        new Contact
                        {
                            Name = "Paola Quintanilla",
                            IdResume = 2
                        }
                    },
                    Skills = new Skill[1]
                    {
                        new Skill
                        {
                            SkillId = "KS125LS6N7WP4S6SFTAM",
                            Name = "C#"
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
            projects = projects.Where(x => x.Id != id).ToArray();
        }

        public Project UpdateById(Project project)
        {
            var projectsList = new List<Project>(projects);
            Project result = projectsList.FirstOrDefault(p => Equals(p.Id, project.Id));
            projectsList.Remove(result);
            projectsList.Add(project);
            projects = new List<Project>(projectsList).ToArray();
            return project;
        }

        public IEnumerable<Project> GetAllBySkill(string skill)
        {
            return projects.Where(project => project.Skills.Any(item => item.Name == skill));
        }
    }
}
