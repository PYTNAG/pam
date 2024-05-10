using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PAM.Core.App.Validation;

namespace PAM.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["DbConnection"];

            services.AddDbContext<PersonalAccountsDbContext>(
                options => options.UseSqlite(connectionString)
            );

            services.AddScoped<IPersonalAccountsDbContext>(
                provider => provider.GetService<PersonalAccountsDbContext>()
            );

            return services;
        }
    }
}