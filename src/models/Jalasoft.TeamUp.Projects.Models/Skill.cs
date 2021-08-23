namespace Jalasoft.TeamUp.Projects.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Skill
    {
        public string SkillId { get; set; }

        public string Name { get; set; }
    }
}
