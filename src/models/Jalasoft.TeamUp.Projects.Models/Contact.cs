namespace Jalasoft.TeamUp.Projects.Models
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Contact
    {
        public string Name { get; set; }

        public Guid IdResume { get; set; }
    }
}
