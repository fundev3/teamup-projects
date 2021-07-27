namespace Jalasoft.TeamUp.Projects.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IRepository<T>
    {
        public T GetById(Guid id);

        public List<T> GetAll();

        public T Create();
    }
}
