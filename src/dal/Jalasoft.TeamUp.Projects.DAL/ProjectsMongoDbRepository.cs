namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class ProjectsMongoDbRepository : IProjectsMongoDbRepository
    {
        private static MongoClient client;
        private static IMongoDatabase database;
        private static IMongoCollection<Project> collection;

        public ProjectsMongoDbRepository()
        {
            string stringConnection = Environment.GetEnvironmentVariable("MongoSessionServices", EnvironmentVariableTarget.Process);
            client = new MongoClient(stringConnection);
            if (client == null)
            {
                throw new ProjectsException(ProjectsException.ProjectsErros.InternalServerError);
            }

            database = client.GetDatabase("Projects");
            collection = database.GetCollection<Project>("Projects");
        }

        public Project Add(Project project)
        {
            if (collection == null)
            {
                throw new ProjectsException(ProjectsException.ProjectsErros.InternalServerError);
            }

            if (collection.Find(x => x.Name == project.Name).FirstOrDefault() == null)
            {
                throw new ProjectsException(ProjectsException.ProjectsErros.NotFound);
            }

            collection.InsertOne(project);
            return project;
        }

        public IEnumerable<Project> GetAll()
        {
            if (collection == null)
            {
                throw new ProjectsException(ProjectsException.ProjectsErros.InternalServerError);
            }

            return collection.Find(new BsonDocument()).ToEnumerable<Project>();
        }

        public Project GetById(Guid id)
        {
            if (collection == null)
            {
                throw new ProjectsException(ProjectsException.ProjectsErros.InternalServerError);
            }

            Project project = collection.Find(x => x.Id == id).FirstOrDefault();
            if (project == null)
            {
                throw new ProjectsException(ProjectsException.ProjectsErros.NotFound);
            }

            return project;
        }
    }
}
