namespace Jalasoft.TeamUp.Projects.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IRepository<T>
    {
        public T GetById(Guid id);

        public IEnumerable<T> GetAll();

        public T Add(T project);

        public T UpdateProject(Project project);
    }
}
