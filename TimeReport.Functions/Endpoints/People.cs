namespace TimeReport.Functions.Endpoints;
using System.Net;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using TimeReport.Contract;

public sealed class People : BaseHttpFunction
{
    private readonly ILogger logger;
    private readonly IMediator mediator;

    public People(ILoggerFactory loggerFactory, IMediator mediator)
    {
        logger = loggerFactory.CreateLogger<People>();
        this.mediator = mediator;
    }

    [OpenApiOperation(operationId: "CreatePerson", tags: new[] { "People" })]
    [OpenApiRequestBody("application/json", typeof(CreatePersonCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(PersonResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [Function("CreatePerson")]
    public async Task<IActionResult> CreatePerson([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        CreatePersonCommand? request = await GetFromBody<CreatePersonCommand>(req.Body);
        if (request is null)
        {
            return new BadRequestResult();
        }

        PersonResponse response = await mediator.Send(request);

        return response is not null ?
            new OkObjectResult(response) :
            new BadRequestResult();
    }

    [OpenApiOperation(operationId: "ReadPeople", tags: new[] { "People" })]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<PersonResponse>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadPeople")]
    public async Task<IActionResult> ReadPeople([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        IEnumerable<PersonResponse>? response = await mediator.Send(new ReadPeopleQuery());

        return response is not null && response.Any() ?
            new OkObjectResult(response.ToArray()) :
            new NotFoundResult();
    }

    [OpenApiOperation(operationId: "ReadPerson", tags: new[] { "People" })]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(PersonFullResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("ReadPerson")]
    public async Task<IActionResult> ReadPerson([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return new NotFoundResult();
        }

        PersonFullResponse? response = await mediator.Send(new ReadPersonQuery(id));

        return response is not null ?
            new OkObjectResult(response) :
            new NotFoundResult();
    }

    [OpenApiOperation(operationId: "UpdatePerson", tags: new[] { "People" })]
    [OpenApiRequestBody("application/json", typeof(UpdatePersonCommand))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(PersonResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("UpdatePerson")]
    public async Task<IActionResult> UpdatePerson([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
    {
        UpdatePersonCommand? request = await GetFromBody<UpdatePersonCommand>(req.Body);

        if (request is null)
        {
            return new NotFoundResult();
        }

        PersonResponse? response = await mediator.Send(request);

        return response is not null ?
            new OkObjectResult(response) :
            new NotFoundResult();
    }

    [OpenApiOperation(operationId: "DeletePerson", tags: new[] { "People" })]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(PersonResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [Function("DeletePerson")]
    public async Task<IActionResult> DeletePerson([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
    {
        if (!int.TryParse(req.Query("id"), out int id))
        {
            return new NotFoundResult();
        }

        PersonResponse? response = await mediator.Send(new DeletePersonCommand(id));

        return response is not null ?
            new OkObjectResult(response) :
            new NotFoundResult();
    }
}
