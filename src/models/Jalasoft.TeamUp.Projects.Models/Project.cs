namespace Jalasoft.TeamUp.Projects.Models
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public Contact Contact { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        public bool State { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string TextInvitation { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Logo { get; set; }

        [BsonRepresentation(BsonType.Array)]
        public Contact[] MemberList { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreationDate { get; set; }
    }
}
