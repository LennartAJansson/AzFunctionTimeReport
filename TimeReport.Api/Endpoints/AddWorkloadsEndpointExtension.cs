namespace TimeReport.Api.Endpoints;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

using TimeReport.Contract;

public static class AddWorkloadsEndpointExtension
{
    public static void AddWorkloadsEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/workloads").WithTags("workloads");

        _ = group.MapPost("/", async Task<Results<Ok<WorkloadResponse>, NotFound>> (IMediator mediator, CreateWorkloadCommand request) =>
        {
            WorkloadResponse? response = await mediator.Send(request);
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("CreateWorkload")
            .WithOpenApi();

        _ = group.MapGet("/", async Task<Results<Ok<IEnumerable<WorkloadResponse>>, NotFound>> (IMediator mediator) =>
        {
            IEnumerable<WorkloadResponse>? response = await mediator.Send(new ReadWorkloadsQuery());
            return response is not null && response.Any() ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("GetAllWorkloads")
            .WithOpenApi();

        _ = group.MapGet("/{id}", async Task<Results<Ok<WorkloadFullResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            WorkloadFullResponse? response = await mediator.Send(new ReadWorkloadQuery(id));
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("GetWorkload")
            .WithOpenApi();

        _ = group.MapPut("/", async Task<Results<Ok<WorkloadResponse>, NotFound>> (IMediator mediator, UpdateWorkloadCommand request) =>
        {
            WorkloadResponse? response = await mediator.Send(request);
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("UpdateWorkload")
            .WithOpenApi();

        _ = group.MapDelete("/{id}", async Task<Results<Ok<WorkloadResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            WorkloadResponse? response = await mediator.Send(new DeleteWorkloadCommand(id));
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("DeleteWorkload")
            .WithOpenApi();
    }
}


