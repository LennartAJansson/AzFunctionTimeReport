namespace TimeReport.Api.Endpoints;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

using TimeReport.Contract;

public static class AddPeopleEndpointsExtension
{
    public static void AddPeopleEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/people").WithTags("people");

        _ = group.MapPost("/", async Task<Results<Ok<PersonResponse>, NotFound>> (IMediator mediator, CreatePersonCommand request) =>
        {
            PersonResponse? response = await mediator.Send(request);
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("CreatePerson")
            .WithOpenApi();

        _ = group.MapGet("/", async Task<Results<Ok<IEnumerable<PersonResponse>>, NotFound>> (IMediator mediator) =>
        {
            IEnumerable<PersonResponse>? response = await mediator.Send(new ReadPeopleQuery());
            return response is not null && response.Any() ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("GetAllPeople")
            .WithOpenApi();

        _ = group.MapGet("/{id}", async Task<Results<Ok<PersonFullResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            PersonFullResponse? response = await mediator.Send(new ReadPersonQuery(id));
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("GetPerson")
            .WithOpenApi();

        _ = group.MapPut("/", async Task<Results<Ok<PersonResponse>, NotFound>> (IMediator mediator, UpdatePersonCommand request) =>
        {
            PersonResponse? response = await mediator.Send(request);
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("UpdatePerson")
            .WithOpenApi();

        _ = group.MapDelete("/{id}", async Task<Results<Ok<PersonResponse>, NotFound>> (IMediator mediator, int id) =>
        {
            PersonResponse? response = await mediator.Send(new DeletePersonCommand(id));
            return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
        })
            .WithName("DeletePerson")
            .WithOpenApi();
    }
}


