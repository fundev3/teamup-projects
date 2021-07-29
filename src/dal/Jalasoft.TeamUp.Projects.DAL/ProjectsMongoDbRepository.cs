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
        private static IMongoDatabase database;
        private static IMongoCollection<Project> collection;

        public ProjectsMongoDbRepository()
        {
            string stringConnection = ConfigurationManager.ConnectionStrings["MongoSessionServices"].ConnectionString;
            client = new MongoClient(stringConnection);
            database = client.GetDatabase("Projects");
            collection = database.GetCollection<Project>("Projects");
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
            Project project = (Project)collection.Find(x => x.Id == id);
            return project;
        }
    }
}
