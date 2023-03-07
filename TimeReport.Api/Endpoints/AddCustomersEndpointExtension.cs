namespace TimeReport.Api.Endpoints;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

using TimeReport.Contract;

public static class AddCustomersEndpointExtension
{
    public static void AddCustomersEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/customers").WithTags("customers");

        _ = group.MapPost("/", async Task<Results<Ok<CustomerResponse>, NotFound>> (IMediator mediator, CreateCustomerCommand request) =>
        {
            CustomerResponse? response = await mediator.Send(request);
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("CreateCustomer")
            .WithOpenApi();

        _ = group.MapGet("/", async Task<Results<Ok<IEnumerable<CustomerResponse>>, NotFound>> (IMediator mediator) =>
        {
            IEnumerable<CustomerResponse>? response = await mediator.Send(new ReadCustomersQuery());
            return response is not null && response.Any() ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("GetAllCustomers")
            .WithOpenApi();

        _ = group.MapGet("/{id}", async Task<Results<Ok<CustomerFullResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            CustomerFullResponse? response = await mediator.Send(new ReadCustomerQuery(id));
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("GetCustomer")
            .WithOpenApi();

        _ = group.MapPut("/", async Task<Results<Ok<CustomerResponse>, NotFound>> (IMediator mediator, UpdateCustomerCommand request) =>
        {
            CustomerResponse? response = await mediator.Send(request);
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("UpdateCustomer")
            .WithOpenApi();

        _ = group.MapDelete("/{id}", async Task<Results<Ok<CustomerResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            CustomerResponse? response = await mediator.Send(new DeleteCustomerCommand(id));
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("DeleteCustomer")
            .WithOpenApi();
    }
}


