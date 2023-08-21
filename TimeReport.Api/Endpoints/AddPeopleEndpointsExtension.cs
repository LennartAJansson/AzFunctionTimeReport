namespace TimeReport.Api.Endpoints;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

using TimeReport.Contract;

public static class AddPeopleEndpointsExtension
{
    public static IEndpointRouteBuilder AddPeopleEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/People").WithTags("People");

        _ = group.MapPost("/CreatePerson", async Task<Results<Ok<PersonResponse>, NotFound>> (IMediator mediator, CreatePersonCommand request) =>
        {
            PersonResponse? response = await mediator.Send(request);
            
            return response is not null ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("CreatePerson")
            .WithOpenApi();

        _ = group.MapGet("/GetAllPeople", async Task<Results<Ok<IEnumerable<PersonResponse>>, NotFound>> (IMediator mediator) =>
        {
            IEnumerable<PersonResponse>? response = await mediator.Send(new ReadPeopleQuery());
            
            return response is not null && response.Any() ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("GetAllPeople")
            .WithOpenApi();

        _ = group.MapGet("/GetPerson/{id}", async Task<Results<Ok<PersonFullResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            PersonFullResponse? response = await mediator.Send(new ReadPersonQuery(id));
            
            return response is not null ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("GetPerson")
            .WithOpenApi();

        _ = group.MapPut("/UpdatePerson", async Task<Results<Ok<PersonResponse>, NotFound>> (IMediator mediator, UpdatePersonCommand request) =>
        {
            PersonResponse? response = await mediator.Send(request);
            
            return response is not null ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("UpdatePerson")
            .WithOpenApi();

        _ = group.MapDelete("/DeletePerson/{id}", async Task<Results<Ok<PersonResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            PersonResponse? response = await mediator.Send(new DeletePersonCommand(id));
            
            return response is not null ? 
                TypedResults.Ok(response) : 
                TypedResults.NotFound();
        })
            .WithName("DeletePerson")
            .WithOpenApi();

        return app;
    }
}


