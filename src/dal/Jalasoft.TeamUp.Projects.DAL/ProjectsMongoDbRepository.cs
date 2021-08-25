namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class ProjectsMongoDbRepository : IProjectsRepository
    {
        private static MongoClient client;
        private static IMongoDatabase database;
        private static IMongoCollection<Project> collection;

        public ProjectsMongoDbRepository()
        {
            string stringConnection = Environment.GetEnvironmentVariable("MongoSessionServices", EnvironmentVariableTarget.Process);
            client = new MongoClient(stringConnection);

            database = client.GetDatabase("Projects");
            collection = database.GetCollection<Project>("Projects");
        }

        public Project Add(Project project)
        {
            collection.InsertOne(project);
            return project;
        }

        public IEnumerable<Project> GetAll()
        {
            return collection.Find(new BsonDocument()).ToEnumerable<Project>();
        }

        public Project GetById(Guid id)
        {
            Project project = collection.Find(x => x.Id == id).FirstOrDefault();

            return project;
        }

        public void Remove(Guid id)
        {
           collection.FindOneAndDelete(x => x.Id == id);
        }

        public Project UpdateById(Project project)
        {
            var filter = Builders<Project>.Filter.Eq(s => s.Id, project.Id);
            var proj = collection.ReplaceOne(filter, project);

            return project;
        }

        public IEnumerable<Project> GetAllBySkill(string skill)
        {
            var filter = Builders<Project>.Filter.ElemMatch(x => x.Skills, x => x.Name == skill);
            return collection.Find(filter).ToEnumerable<Project>();
        }
    }
}
