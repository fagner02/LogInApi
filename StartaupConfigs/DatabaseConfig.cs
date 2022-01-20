using LogInApi.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogInApi.StartupConfig {
    public static class DatabaseConfig {
        public static void AddDatabaseService(this IServiceCollection services, IConfiguration Configuration) {
            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContextPool<DatabaseContext>(
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}