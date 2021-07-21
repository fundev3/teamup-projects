namespace Jalasoft.TeamUp.Projects.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Projects.Models;

    public interface IHealthService
    {
        Health GetHealth();
    }
}