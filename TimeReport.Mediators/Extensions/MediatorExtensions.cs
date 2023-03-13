namespace TimeReport.Mediators.Extensions;
using Microsoft.Extensions.DependencyInjection;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediators(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(MediatorExtensions).Assembly);
        });

        services.AddAutoMapper(typeof(MediatorExtensions).Assembly);

        return services;
    }
}
