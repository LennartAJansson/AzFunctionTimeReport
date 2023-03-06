namespace TimeReport.Endpoints;

using System.Net;
using System.Text.Json;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using TimeReport.Contract;
using TimeReport.Model;

public sealed class PersonEndpoints
{
    private readonly ILogger<PersonEndpoints> logger;

    public PersonEndpoints(ILogger<PersonEndpoints> logger)
    {
        this.logger = logger;
    }

    [OpenApiOperation(operationId: "ReadPeople", tags: new[] { "People" }, Summary = "ReadPeople", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<PersonResponse>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadPeople")]
    public async Task<IActionResult> ReadPeople(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        IMediator mediator)
    {
        IEnumerable<PersonResponse>? response = await mediator.Send(new ReadPeopleQuery());

        return response is not null && response.Any() ? new OkObjectResult(response.ToArray()) : new NotFoundResult();
    }

    [OpenApiOperation(operationId: "ReadPerson", tags: new[] { "People" }, Summary = "ReadPerson", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(PersonFullResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadPerson")]
    public async Task<IActionResult> ReadPerson(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        IMediator mediator)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return new NotFoundResult();
        }

        PersonFullResponse? response = await mediator.Send(new ReadPersonQuery(id));
        return response is not null ? new OkObjectResult(response) : new NotFoundResult();
    }

    [OpenApiOperation(operationId: "CreatePerson", tags: new[] { "People" }, Summary = "CreatePerson", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(CreatePersonCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(PersonResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("CreatePerson")]
    public async Task<IActionResult> CreatePerson(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        IMediator mediator)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            CreatePersonCommand? request = JsonSerializer.Deserialize<CreatePersonCommand>(req.Body, options);
            if (request is not null)
            {
                PersonResponse response = await mediator.Send(request);
                return new OkObjectResult(response);
            }
        }

        return new BadRequestResult();
    }


    [OpenApiOperation(operationId: "UpdatePerson", tags: new[] { "People" }, Summary = "UpdatePerson", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(UpdatePersonCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(PersonResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("UpdatePerson")]
    public async Task<IActionResult> UpdatePerson(
        [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req,
        IMediator mediator)
    {
        if (req.Body.Length > 0)
        {
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            UpdatePersonCommand? request = JsonSerializer.Deserialize<UpdatePersonCommand>(req.Body, options);
            if (request is not null)
            {
                PersonResponse response = await mediator.Send(request);
                return new OkObjectResult(response);
            }
        }

        return new BadRequestResult();
    }

    [OpenApiOperation(operationId: "DeletePerson", tags: new[] { "People" }, Summary = "DeletePerson", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(PersonResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("DeletePerson")]
    public async Task<IActionResult> DeletePerson(
        [HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req,
        IMediator mediator)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return new NotFoundResult();
        }

        PersonResponse? response = await mediator.Send(new DeletePersonCommand(id));
        return response is not null ? new OkObjectResult(response) : new NotFoundResult();
    }
}
