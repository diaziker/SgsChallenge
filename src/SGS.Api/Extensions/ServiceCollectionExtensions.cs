using SGS.Domain.DataSource;
using SGS.Domain.Services;
using SGS.Infrastructure.DataSource;
using SGS.Infrastructure.Settings;

namespace SGS.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton<IDataSource, DataSource>()
                .Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

            return services;
        }

        public static IServiceCollection AddDomainDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProductServices, ProductServices>();

            return services;
        }
    }
}
