﻿namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using TimeReport.Data.Context;
using TimeReport.Data.Interface;

using TimeReport.Data.Service;

public static class TimeReportContextExtensions
{
    public static IServiceCollection AddTimeReportPersistance(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ITimeReportContext, TimeReportContext>(options => options.UseSqlServer(connectionString));
        services.AddTransient<ITimeReportService, TimeReportService>();
        
        return services;
    }
}