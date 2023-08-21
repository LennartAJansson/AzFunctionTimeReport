using TimeReport.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddTimeReportPersistance(builder.Configuration.GetConnectionString("TimeReportDb")
        ?? throw new ArgumentException("ConnectionString \"TimeReportDb\" is missing in configuration"))
    .AddMediators()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

WebApplication app = builder.Build();

app.UpdateTimeReportPersistance();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app
    .AddPeopleEndpoints()
    .AddCustomersEndpoints()
    .AddWorkloadsEndpoints();

app.Run();
