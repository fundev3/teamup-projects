namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using MongoDB.Driver;

    public class ProjectsMongoDbRepository : IProjectsMongoDbRepository
    {
        private static MongoClient client;

        public ProjectsMongoDbRepository()
        {
            client = new MongoClient("mongodb://localhost:27017");
        }

        public Project Add()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAll()
        {
            throw new NotImplementedException();
        }

        public Project GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
