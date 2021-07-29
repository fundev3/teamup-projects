namespace Jalasoft.TeamUp.Projects.Models
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Contact
    {
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid IdResume { get; set; }
    }
}
