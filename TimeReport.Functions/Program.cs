using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration(builder => builder.AddUserSecrets<Program>())
    .ConfigureOpenApi()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
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


    host.UpdateTimeReportPersistance().Run();
