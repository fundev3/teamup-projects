namespace Jalasoft.TeamUp.Projects.Models
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Project
    {
        [BsonRequired]
        public Guid Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Description { get; set; }

        [BsonRequired]
        public Contact Contact { get; set; }

        public bool State { get; set; }

        public string TextInvitation { get; set; }

        public string Logo { get; set; }

        public Contact[] MemberList { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
