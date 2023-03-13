using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using TimeReport.Data.Extensions;
using TimeReport.Mediators.Extensions;

var host = new HostBuilder()
    .ConfigureAppConfiguration(builder=>builder.AddUserSecrets<Program>())
    .ConfigureOpenApi()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context,services) =>
    {
        //We rely on the fact that the connection string is stored in either:
        //the local user secrets as "ConnectionStrings:TimeReportDb"
        //or environment variable as "ConnectionStrings__TimeReportDb"
        services
            .AddTimeReportPersistance(context.Configuration.GetConnectionString("TimeReportDb")
            ?? throw new ArgumentException("ConnectionString \"TimeReportDb\" is missing in configuration"));
        services.AddMediators();
    })
    .Build();

host.Run();
