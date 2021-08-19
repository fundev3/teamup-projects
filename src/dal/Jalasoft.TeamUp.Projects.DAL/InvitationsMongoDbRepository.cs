namespace Jalasoft.TeamUp.Projects.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class InvitationsMongoDbRepository : IInvitationsRepository
    {
        private static MongoClient client;
        private static IMongoDatabase database;
        private static IMongoCollection<Invitation> collection;

        public InvitationsMongoDbRepository()
        {
            string stringConnection = Environment.GetEnvironmentVariable("MongoSessionServices", EnvironmentVariableTarget.Process);
            client = new MongoClient(stringConnection);

            database = client.GetDatabase("Projects");
            collection = database.GetCollection<Invitation>("Invitations");
        }

        public Invitation Add(Invitation invitation)
        {
            collection.InsertOne(invitation);
            return invitation;
        }

        public IEnumerable<Invitation> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invitation> GetAllInvitationsByProjectId(string projectId)
        {
            return collection.Find(x => x.ProjectId == projectId).ToEnumerable<Invitation>();
        }

        public IEnumerable<Invitation> GetAllInvitationsByResumeId(int resumeId)
        {
            return collection.Find(x => x.ResumeId == resumeId).ToEnumerable<Invitation>();
        }

        public Invitation GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Invitation UpdateById(Invitation project)
        {
            throw new NotImplementedException();
        }
    }
}
