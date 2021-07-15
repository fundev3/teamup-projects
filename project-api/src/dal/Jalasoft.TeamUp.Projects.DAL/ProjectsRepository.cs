﻿namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;

    public class ProjectsRepository : IProjectsRepository
    {
        private static IEnumerable<Project> projects = new List<Project>() { new Project() { Id = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"), Name = "TeamUp", Contact = "TeamUp@fundacion-jala.org", Description = "Centralize resumes and project" } };

        public Project GetProject(Guid id)
        {
            Project result = projects.Where(p => Guid.Equals(p.Id, id)).SingleOrDefault();
            return result;
        }
    }
}
