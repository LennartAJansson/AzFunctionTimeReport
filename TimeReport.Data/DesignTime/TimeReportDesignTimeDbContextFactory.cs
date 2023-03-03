namespace TimeReport.Data.DesignTime;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TimeReport.Data.Context;

/*
Add-Migration Initial -Context TimeReportContext -Project TimeReport.Data -StartupProject TimeReport.Data 
Update-Database -Context TimeReportContext -Project TimeReport.Data -StartupProject TimeReport.Data
*/

public sealed class TimeReportDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TimeReportContext>
{
    public TimeReportContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<TimeReportContext>()
            .Build();

        DbContextOptionsBuilder<TimeReportContext> optionsBuilder = new();
        _ = optionsBuilder.UseSqlServer(configuration.GetConnectionString("TimeReportDb"))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        return new TimeReportContext(optionsBuilder.Options);
    }
}
