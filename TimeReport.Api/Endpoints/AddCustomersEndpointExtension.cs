namespace TimeReport.Api.Endpoints;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

using TimeReport.Contract;

public static class AddCustomersEndpointExtension
{
    public static IEndpointRouteBuilder AddCustomersEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/Customers")
            .WithTags("Customers");

        _ = group.MapPost("/CreateCustomer", async Task<Results<Ok<CustomerResponse>, NotFound>> (IMediator mediator, CreateCustomerCommand request) =>
        {
            CustomerResponse? response = await mediator.Send(request);
            
            return response is not null ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("CreateCustomer")
            .WithOpenApi();

        _ = group.MapGet("/GetAllCustomers", async Task<Results<Ok<IEnumerable<CustomerResponse>>, NotFound>> (IMediator mediator) =>
        {
            IEnumerable<CustomerResponse>? response = await mediator.Send(new ReadCustomersQuery());
            
            return response is not null && response.Any() ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("GetAllCustomers")
            .WithOpenApi();

        _ = group.MapGet("/GetCustomer/{id}", async Task<Results<Ok<CustomerFullResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            CustomerFullResponse? response = await mediator.Send(new ReadCustomerQuery(id));
            
            return response is not null ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("GetCustomer")
            .WithOpenApi();

        _ = group.MapPut("/UpdateCustomer", async Task<Results<Ok<CustomerResponse>, NotFound>> (IMediator mediator, UpdateCustomerCommand request) =>
        {
            CustomerResponse? response = await mediator.Send(request);
            
            return response is not null ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("UpdateCustomer")
            .WithOpenApi();

        _ = group.MapDelete("/DeleteCustomer/{id}", async Task<Results<Ok<CustomerResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            CustomerResponse? response = await mediator.Send(new DeleteCustomerCommand(id));
            
            return response is not null ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("DeleteCustomer")
            .WithOpenApi();

        return app;
    }
}


