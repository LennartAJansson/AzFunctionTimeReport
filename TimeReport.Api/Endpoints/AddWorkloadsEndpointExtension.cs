namespace TimeReport.Api.Endpoints;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

using TimeReport.Contract;

public static class AddWorkloadsEndpointExtension
{
    public static IEndpointRouteBuilder AddWorkloadsEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/Workloads").WithTags("Workloads");

        _ = group.MapPost("/CreateWorkload", async Task<Results<Ok<WorkloadResponse>, NotFound>> (IMediator mediator, CreateWorkloadCommand request) =>
        {
            WorkloadResponse? response = await mediator.Send(request);

            return response is not null ?
                TypedResults.Ok(response) :
                TypedResults.NotFound();
        })
            .WithName("CreateWorkload")
            .WithOpenApi();

        _ = group.MapGet("/GetAllWorkloads", async Task<Results<Ok<IEnumerable<WorkloadResponse>>, NotFound>> (IMediator mediator) =>
        {
            IEnumerable<WorkloadResponse>? response = await mediator.Send(new ReadWorkloadsQuery());

            return response is not null && response.Any() ?
                TypedResults.Ok(response) :
                TypedResults.NotFound();
        })
            .WithName("GetAllWorkloads")
            .WithOpenApi();

        _ = group.MapGet("/GetWorkload/{id}", async Task<Results<Ok<WorkloadFullResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            WorkloadFullResponse? response = await mediator.Send(new ReadWorkloadQuery(id));

            return response is not null ?
                TypedResults.Ok(response) :
                TypedResults.NotFound();
        })
            .WithName("GetWorkload")
            .WithOpenApi();

        _ = group.MapGet("/GetWorkloadsByPerson/{id}", async Task<Results<Ok<IEnumerable<WorkloadFullResponse>>, NotFound>> (IMediator mediator, int id) =>
        {
            IEnumerable<WorkloadFullResponse>? response = await mediator.Send(new ReadWorkloadsByPersonQuery(id));

            return response is not null ?
                TypedResults.Ok(response) :
                TypedResults.NotFound();
        })
            .WithName("GetWorkloadsByPerson")
            .WithOpenApi();

        _ = group.MapGet("/GetWorkloadsByCustomer/{id}", async Task<Results<Ok<IEnumerable<WorkloadFullResponse>>, NotFound>> (IMediator mediator, int id) =>
        {
            IEnumerable<WorkloadFullResponse>? response = await mediator.Send(new ReadWorkloadsByCustomerQuery(id));

            return response is not null ?
                TypedResults.Ok(response) :
                TypedResults.NotFound();
        })
            .WithName("GetWorkloadsByCustomer")
            .WithOpenApi();

        _ = group.MapPut("/UpdateWorkload", async Task<Results<Ok<WorkloadResponse>, NotFound>> (IMediator mediator, UpdateWorkloadCommand request) =>
        {
            WorkloadResponse? response = await mediator.Send(request);

            return response is not null ?
                TypedResults.Ok(response) :
                TypedResults.NotFound();
        })
            .WithName("UpdateWorkload")
            .WithOpenApi();

        _ = group.MapDelete("/DeleteWorkload/{id}", async Task<Results<Ok<WorkloadResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            WorkloadResponse? response = await mediator.Send(new DeleteWorkloadCommand(id));

            return response is not null ?
                TypedResults.Ok(response) :
                TypedResults.NotFound();
        })
            .WithName("DeleteWorkload")
            .WithOpenApi();

        return app;
    }
}


