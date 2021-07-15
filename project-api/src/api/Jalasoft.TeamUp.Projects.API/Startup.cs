using Jalasoft.TeamUp.Projects.API;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Jalasoft.TeamUp.Projects.API
{
    using Jalasoft.TeamUp.Projects.Core;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.DAL;
    using Jalasoft.TeamUp.Projects.DAL.Interfaces;
    using Microsoft.Azure.Functions.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
              builder.Services.AddTransient<IHealthsService, HealthsService>();
              builder.Services.AddTransient<IHealthsRepository, HealthsRepository>();
        }
    }
}
