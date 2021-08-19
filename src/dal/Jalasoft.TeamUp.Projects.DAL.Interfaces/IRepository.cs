namespace Jalasoft.TeamUp.Projects.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IRepository<T>
    {
        public T GetById(Guid id);

        public IEnumerable<T> GetAll();

        public T Add(T project);

        public void Remove(Guid id);

        public T UpdateById(T project);
    }
}
