namespace XCore.Common.Data.Repository;

/// <summary>
///     The service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the repository.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="setEntityReadyToExport">If true, set entity ready to export.</param>
    /// <param name="ignoreExportTracking">If true, ignore export tracking.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddRepository<TDbContext, TEntity, TIRepository, TRepository>(
        this IServiceCollection services, bool? setEntityReadyToExport = null, bool? ignoreExportTracking = null)
        where TDbContext : BaseDbContext
        where TEntity : class, IEntityId
        where TIRepository : class, IRepository<TEntity>
        where TRepository : class, TIRepository
    {
        services.AddScoped<TIRepository, TRepository>(provider =>
        {
            var context = provider.GetRequiredService<TDbContext>();
            return (TRepository)Activator.CreateInstance(typeof(TRepository), context, setEntityReadyToExport,
                ignoreExportTracking)!;
        });

        return services;
    }

    /// <summary>
    ///     Adds the repository.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="setEntityReadyToExport">If true, set entity ready to export.</param>
    /// <param name="ignoreExportTracking">If true, ignore export tracking.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddRepository<TDbContext>(
        this IServiceCollection services, bool? setEntityReadyToExport = null, bool? ignoreExportTracking = null)
        where TDbContext : BaseDbContext
    {
        var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => !string.IsNullOrWhiteSpace(x.FullName) &&
                        !x.FullName.Contains("Microsoft", StringComparison.CurrentCultureIgnoreCase))
            .SelectMany(x => x.GetTypes()).Where(type =>
                type is { IsAbstract: false, IsClass: true, IsInterface: false }).ToList();

        entityTypes = entityTypes.Where(x =>
                       x.GetInterfaces().Any(y => y.IsGenericType && (y.GetGenericTypeDefinition() == typeof(IRepository<>) || y.GetGenericTypeDefinition() == typeof(IRepositoryAsync<>))) &&
                       x.BaseType is { IsGenericType: true } &&
                       (x.BaseType.GetGenericTypeDefinition() == typeof(Repository<,>) || x.BaseType.GetGenericTypeDefinition() == typeof(RepositoryAsync<,>))).ToList();

        foreach (var type in entityTypes)
        {
            var repositoryInterface = type.GetInterfaces().First(x => !x.IsGenericType);
            var repositoryGenericInterface = type.GetInterfaces().Where(x => x != repositoryInterface)
                .OrderByDescending(x => x.Name)
                .First(x => x.IsGenericType);

            services.AddScoped(repositoryInterface, provider =>
            {
                var context = provider.GetRequiredService<TDbContext>();
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                return Activator.CreateInstance(type, context, loggerFactory, setEntityReadyToExport,
                    ignoreExportTracking)!;
            });

            services.AddScoped(repositoryGenericInterface, provider =>
            {
                var context = provider.GetRequiredService<TDbContext>();
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                return Activator.CreateInstance(type, context, loggerFactory, setEntityReadyToExport,
                    ignoreExportTracking)!;
            });
        }

        return services;
    }
}