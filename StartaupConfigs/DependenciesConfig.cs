using LogInApi.Repositories;
using LogInApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LogInApi.StartupConfig {
    public static class DependenciesConfig {
        public static void AddDependenciesService(this IServiceCollection services) {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICollaboratorService, CollaboratorService>();
        }
    }
}