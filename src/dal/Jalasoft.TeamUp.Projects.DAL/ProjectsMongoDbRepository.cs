namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using MongoDB.Driver;

    public class ProjectsMongoDbRepository : IProjectsMongoDbRepository
    {
        private static MongoClient client;

        public ProjectsMongoDbRepository()
        {
            string stringConnection = ConfigurationManager.ConnectionStrings["MongoSessionServices"].ConnectionString;
            client = new MongoClient(stringConnection);
        }

        public Project Add(Project project)
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
