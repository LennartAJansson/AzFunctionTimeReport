using Microsoft.Extensions.DependencyInjection;

namespace TimeReport.Data.Extensions;

using Microsoft.EntityFrameworkCore;

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
}