namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using TimeReport.Data.Context;
using TimeReport.Data.Interfaces;
using TimeReport.Data.Services;

public static class TimeReportContextExtensions
{
    public static IServiceCollection AddTimeReportPersistance(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ITimeReportContext, TimeReportContext>(options => options.UseSqlServer(connectionString));
        services.AddTransient<ITimeReportService, TimeReportService>();

        return services;
    }

    public static IHost UpdateTimeReportPersistance(this IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();
        
        scope.ServiceProvider
            .GetRequiredService<TimeReportContext>()
            .UpdateDb();

        return host;
    }
}