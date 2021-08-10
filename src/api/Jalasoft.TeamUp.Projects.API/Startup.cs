using System;
using Jalasoft.TeamUp.Projects.API;
using Jalasoft.TeamUp.Projects.Core;
using Jalasoft.TeamUp.Projects.Core.Interfaces;
using Jalasoft.TeamUp.Projects.DAL;
using Jalasoft.TeamUp.Projects.DAL.Interfaces;
using Jalasoft.TeamUp.Projects.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Jalasoft.TeamUp.Projects.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IHealthService, HealthService>();
            builder.Services.AddScoped<IHealthRepository, HealthRepository>();
            builder.Services.AddScoped<IProjectsService, ProjectsService>();
#if DEBUG
            builder.Services.AddScoped<IRepository<Project>, ProjectsMongoDbRepository>();
#else
            builder.Services.AddScoped<IRepository<Project>, ProjectsInMemoryRepository>(); 
#endif
        }
    }
}
