using System.Reflection;

namespace XCore.Common.Data.Command;

/// <summary>
///     The service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandData(this IServiceCollection services)
    {
        var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => !string.IsNullOrWhiteSpace(x.FullName) &&
                        !x.FullName.Contains("Microsoft", StringComparison.CurrentCultureIgnoreCase))
            .SelectMany(x => x.GetTypes()).Where(type =>
                type is { IsAbstract: false, IsClass: true, IsInterface: false }).ToList();

        entityTypes = entityTypes.Where(x =>
            x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))).ToList();

        services.AddMediatR(mediatRServiceConfiguration => mediatRServiceConfiguration.RegisterServicesFromAssemblies(entityTypes.Select(type => type.Assembly).ToArray()));

        return services;
    }
}