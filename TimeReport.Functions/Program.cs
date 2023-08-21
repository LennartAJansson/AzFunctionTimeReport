using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration(builder => builder
        .AddUserSecrets<Program>())
    .ConfigureOpenApi()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) => services
        .AddTimeReportPersistance(context.Configuration.GetConnectionString("TimeReportDb")
            ?? throw new ArgumentException("ConnectionString \"TimeReportDb\" is missing in configuration"))
        .AddMediators())
    .Build();


    host.UpdateTimeReportPersistance().Run();
