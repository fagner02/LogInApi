using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace LogInApi.StartupConfig {
    public static class ControllerConfig {
        public static void AddControllerService(this IServiceCollection services) {
            services
                .AddControllers()
                .AddJsonOptions(
                    options => options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter()
                    ));
        }
    }
}