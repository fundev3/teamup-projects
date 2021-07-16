namespace Jalasoft.TeamUp.Projects.DAL.Interfaces
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.Models;

    public interface ICreateProjectRepository
    {
        public Project PostProject(Project project);
    }
}
