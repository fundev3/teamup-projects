﻿namespace Jalasoft.TeamUp.Projects.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Contact { get; set; }
    }
}
